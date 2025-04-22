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

        // GET: Questoes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Questoes.Include(q => q.Questionario);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Questoes/Details/5
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

        // GET: Questoes/Create
        public IActionResult Create()
        {
            ViewData["QuestionarioId"] = new SelectList(_context.Questionarios, "Id", "Nome");
            return View();
        }

        // POST: Questoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Questoes/Edit/5
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

        // POST: Questoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Questoes/Delete/5
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

        // POST: Questoes/Delete/5
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

        private bool QuestoesExists(int id)
        {
            return _context.Questoes.Any(e => e.Id == id);
        }
    }
}
