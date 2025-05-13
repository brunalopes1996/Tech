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
}

