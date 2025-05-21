using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tech.Data;
using Tech.Models;

namespace Tech.Controllers
{
    public class QuestoesController : Controller
    {
        private readonly AppDbContext _context;

        public QuestoesController(AppDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Questoes.Include(q => q.Questionario);
            return View(await appDbContext.ToListAsync());
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questoes = await _context.Questoes
                .Include(q => q.Questionario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questoes == null)
            {
                return NotFound();
            }

            return View(questoes);
        }

       
        public IActionResult Create()
        {
            ViewData["QuestionarioId"] = new SelectList(_context.Questionarios, "Id", "Nome");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Questao,AlternativaA,AlternativaB,AlternativaC,AlternativaD,AlternativaE,AlternativaCorreta,Imagem,QuestionarioId")] Questoes questoes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(questoes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionarioId"] = new SelectList(_context.Questionarios, "Id", "Nome", questoes.QuestionarioId);
            return View(questoes);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questoes = await _context.Questoes.FindAsync(id);
            if (questoes == null)
            {
                return NotFound();
            }
            ViewData["QuestionarioId"] = new SelectList(_context.Questionarios, "Id", "Nome", questoes.QuestionarioId);
            return View(questoes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Questao,AlternativaA,AlternativaB,AlternativaC,AlternativaD,AlternativaE,AlternativaCorreta,Imagem,QuestionarioId")] Questoes questoes)
        {
            if (id != questoes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questoes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestoesExists(questoes.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionarioId"] = new SelectList(_context.Questionarios, "Id", "Nome", questoes.QuestionarioId);
            return View(questoes);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questoes = await _context.Questoes
                .Include(q => q.Questionario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questoes == null)
            {
                return NotFound();
            }

            return View(questoes);
        }

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questoes = await _context.Questoes.FindAsync(id);
            if (questoes != null)
            {
                _context.Questoes.Remove(questoes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Responder(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questao = await _context.Questoes
                .Include(q => q.Questionario)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (questao == null)
            {
                return NotFound();
            }

            return View(questao);
        }

        private bool QuestoesExists(int id)
        {
            return _context.Questoes.Any(e => e.Id == id);
        }
    }
}
