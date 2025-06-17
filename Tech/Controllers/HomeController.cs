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

    public HomeController(ILogger<HomeController> logger,  AppDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public IActionResult Index()
    {
        return View();
    }

    // Action to display the education level selection page
    public IActionResult SelecionarNivel()
    {
        return View();
    }
    
    // This action is now just a redirect to the selection page
    public IActionResult Questoes()
    {
        return RedirectToAction("SelecionarNivel");
    }
    
    
    // Modified action to display questionnaire based on selected education level
    public async Task<IActionResult> ExibirQuestionario(string nivelEnsino) // Added nivelEnsino parameter
    {
       if (string.IsNullOrEmpty(nivelEnsino))
        {
            // If no level is provided, redirect back to selection
            TempData["MensagemErro"] = "Por favor, selecione um nível de ensino.";
            return RedirectToAction(nameof(SelecionarNivel));
        }

        // Map the route parameter to the expected database value (adjust casing if necessary based on DB data)
        string tipoBancaFiltro = nivelEnsino.ToLower() == "medio" ? "medio" :
                                 nivelEnsino.ToLower() == "superior" ? "superior" :
                                 null; // Handle potential unexpected values

        if (tipoBancaFiltro == null)
        {
             TempData["MensagemErro"] = "Nível de ensino inválido.";
             return RedirectToAction(nameof(SelecionarNivel));
        }

        // Query the database, filtering by Banca.Tipo
        var questionario = await _db.Questionarios
                                    .Include(q => q.Questoes) 
                                    .Include(q => q.Banca) // Include Banca for filtering
                                    .Where(q => q.Banca.Tipo == tipoBancaFiltro && q.Questoes.Any()) // Filter by Banca.Tipo
                                    .FirstOrDefaultAsync(); // Using FirstOrDefault, adjust if multiple questionnaires per type are expected

        if (questionario == null)
        {
            // Use TempData to show a message on the redirected page
            TempData["MensagemErro"] = $"Nenhum questionário disponível para o nível '{nivelEnsino}' no momento.";
            return RedirectToAction(nameof(SelecionarNivel)); // Redirect back to selection if no questionnaire found
        }

        // Pass the selected level to the view, might be useful for display
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
            // Redirect back to the specific questionnaire level if possible
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
        
        // Pass the level back to the result view if needed
        ViewData["NivelEnsino"] = nivelEnsino;

        return View("Resultado", resultado);
    }

}

