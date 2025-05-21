using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Tech.Data;
using Tech.Models;

namespace Tech.Controllers
{
    public class HistoricoController : Controller
    {
        private readonly AppDbContext _context;

        public HistoricoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Historico
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var historico = await _context.Historicos
                .Include(h => h.Usuario)
                .Include(h => h.Questionario)
                .Include(h => h.Questao)
                .Where(h => h.UsuarioId == userId)
                .OrderByDescending(h => h.DataResposta)
                .ToListAsync();

            return View(historico);
        }

        // GET: Historico/Estatisticas
        public async Task<IActionResult> Estatisticas()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            // Obter dados para o gráfico
            var historico = await _context.Historicos
                .Where(h => h.UsuarioId == userId)
                .ToListAsync();

            // Calcular total de acertos e erros
            int totalAcertos = historico.Count(h => h.Acertou);
            int totalErros = historico.Count(h => !h.Acertou);

            // Passar dados para a view
            ViewBag.TotalAcertos = totalAcertos;
            ViewBag.TotalErros = totalErros;
            ViewBag.TotalRespostas = historico.Count;

            // Calcular percentuais
            ViewBag.PercentualAcertos = historico.Count > 0 ? (double)totalAcertos / historico.Count * 100 : 0;
            ViewBag.PercentualErros = historico.Count > 0 ? (double)totalErros / historico.Count * 100 : 0;

            return View();
        }

        // POST: Registrar resposta
        [HttpPost]
        [Route("Historico/RegistrarResposta")] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarResposta(int questaoId, string alternativaSelecionada)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { success = false, message = "Usuário não autenticado" });
            }

            // Buscar a questão
            var questao = await _context.Questoes
                .Include(q => q.Questionario)
                .FirstOrDefaultAsync(q => q.Id == questaoId);

            if (questao == null)
            {
                return Json(new { success = false, message = "Questão não encontrada" });
            }

            // Verificar se a resposta está correta
            bool acertou = questao.AlternativaCorreta == alternativaSelecionada;

            // Criar registro no histórico
            var historico = new Historico
            {
                UsuarioId = userId,
                QuestionarioId = questao.QuestionarioId,
                QuestaoId = questaoId,
                AlternativaSelecionada = alternativaSelecionada,
                Acertou = acertou,
                DataResposta = DateTime.Now,
                Acerto = acertou ? "Sim" : "",
                Erro = acertou ? "" : "Sim"
            };

            _context.Historicos.Add(historico);
            await _context.SaveChangesAsync();

            return Json(new
            {
                success = true,
                acertou = acertou,
                message = acertou ? "Resposta correta!" : "Resposta incorreta. A alternativa correta é " + questao.AlternativaCorreta
            });
        }
    }
}
