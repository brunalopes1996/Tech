using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tech.Models;
using Tech.Data;
using Microsoft.EntityFrameworkCore; 
using System.Threading.Tasks; 

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

    
    public IActionResult Questoes()
    {
        return RedirectToAction("ExibirQuestionario");
    }
    
    
    public async Task<IActionResult> ExibirQuestionario()
    {
       
        var questionario = await _db.Questionarios
                                    .Include(q => q.Questoes) 
                                    .Where(q => q.Questoes.Any()) 
                                    .FirstOrDefaultAsync();

        if (questionario == null)
        {
           
            TempData["MensagemErro"] = "Nenhum questionario disponivel no momento.";
            return RedirectToAction(nameof(Index)); 
        }

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
public async Task<IActionResult> SubmeterRespostas(int questionarioId, Dictionary<int, string> respostas)
{
    if (respostas == null || !respostas.Any())
    {
        TempData["MensagemErro"] = "Nenhuma resposta foi enviada.";
        return RedirectToAction("ExibirQuestionario");
    }
    
    // Buscar o questionário e as questões do banco de dados
    var questionario = await _db.Questionarios
                              .Include(q => q.Questoes)
                              .FirstOrDefaultAsync(q => q.Id == questionarioId);
                              
    if (questionario == null)
    {
        TempData["MensagemErro"] = "Questionário não encontrado.";
        return RedirectToAction("Index");
    }
    
    // Calcular resultados
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
            
            // Verificar se a resposta está correta
            // Substitua "RespostaCorreta" pelo nome da propriedade que armazena a resposta correta no seu modelo
            if (resposta == questao.AlternativaCorreta) 
            {
                acertos++;
                acertou = true;
            }
        }
        
        // Adicionar detalhes da resposta (opcional)
        resultado.DetalhesRespostas.Add(new RespostaDetalhe
        {
            QuestaoId = questao.Id,
            QuestaoTexto = questao.Questao,
            RespostaUsuario = respostaUsuario,
            RespostaCorreta = questao.AlternativaCorreta,
            Acertou = acertou
        });
    }
    
    // Calcular percentual
    resultado.Acertos = acertos;
    resultado.Percentual = totalQuestoes > 0 ? (acertos * 100.0 / totalQuestoes) : 0;
    
    // Retornar para a view de resultado
    return View("Resultado", resultado);
}

}

