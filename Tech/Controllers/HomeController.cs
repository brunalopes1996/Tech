using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tech.Models;
using Tech.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace Tech.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _db;

    public HomeController(ILogger<HomeController> logger, AppDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult SelecionarNivel()
    {
        return View();
    }

    public IActionResult Questoes()
    {
        return RedirectToAction("SelecionarNivel");
    }



    public async Task<IActionResult> ExibirQuestionario(string nivelEnsino)
    {
        if (string.IsNullOrEmpty(nivelEnsino))
        {

            TempData["MensagemErro"] = "Por favor, selecione um nível de ensino.";
            return RedirectToAction(nameof(SelecionarNivel));
        }


        string tipoBancaFiltro = nivelEnsino.ToLower() == "medio" ? "medio" :
                                 nivelEnsino.ToLower() == "superior" ? "superior" :
                                 null;

        if (tipoBancaFiltro == null)
        {
            TempData["MensagemErro"] = "Nível de ensino inválido.";
            return RedirectToAction(nameof(SelecionarNivel));
        }


        var questionario = await _db.Questionarios
                                    .Include(q => q.Questoes)
                                    .Include(q => q.Banca)
                                    .Where(q => q.Banca.Tipo == tipoBancaFiltro && q.Questoes.Any())
                                    .FirstOrDefaultAsync();

        if (questionario == null)
        {

            TempData["MensagemErro"] = $"Nenhum questionário disponível para o nível '{nivelEnsino}' no momento.";
            return RedirectToAction(nameof(SelecionarNivel));
        }


        ViewData["NivelEnsinoSelecionado"] = nivelEnsino;
        ViewData["TituloQuestionario"] = $"Questionário - {questionario.Nome} ({nivelEnsino.Substring(0, 1).ToUpper() + nivelEnsino.Substring(1)})";

        return View(questionario);
    }

    public IActionResult Concursos()
    {
        return View();
    }

    public IActionResult Planos()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public async Task<IActionResult> SubmeterRespostas(int questionarioId, Dictionary<int, string> respostas, string nivelEnsino) // Added nivelEnsino to potentially redirect back correctly
    {
        if (respostas == null || !respostas.Any())
        {
            TempData["MensagemErro"] = "Nenhuma resposta foi enviada.";

            return RedirectToAction("ExibirQuestionario", new { nivelEnsino = nivelEnsino });
        }

        var questionario = await _db.Questionarios
                                  .Include(q => q.Questoes)
                                  .FirstOrDefaultAsync(q => q.Id == questionarioId);

        if (questionario == null)
        {
            TempData["MensagemErro"] = "Questionário não encontrado.";
            return RedirectToAction("Index");
        }

        int totalQuestoes = questionario.Questoes.Count;
        int acertos = 0;

        var resultado = new ResultadoViewModel
        {
            QuestionarioNome = questionario.Nome,
            TotalQuestoes = totalQuestoes,
            DetalhesRespostas = new List<RespostaDetalhe>()
        };

        foreach (var questao in questionario.Questoes)
        {
            bool acertou = false;
            string respostaUsuario = "Não respondida";

            if (respostas.TryGetValue(questao.Id, out string resposta))
            {
                respostaUsuario = resposta;
                if (resposta == questao.AlternativaCorreta)
                {
                    acertos++;
                    acertou = true;
                }
            }

            resultado.DetalhesRespostas.Add(new RespostaDetalhe
            {
                QuestaoId = questao.Id,
                QuestaoTexto = questao.Questao,
                RespostaUsuario = respostaUsuario,
                RespostaCorreta = questao.AlternativaCorreta,
                Acertou = acertou
            });
        }

        resultado.Acertos = acertos;
        resultado.Percentual = totalQuestoes > 0 ? (acertos * 100.0 / totalQuestoes) : 0;


        ViewData["NivelEnsino"] = nivelEnsino;

        return View("Resultado", resultado);
    }

}

