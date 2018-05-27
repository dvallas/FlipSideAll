using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlipSideModels;
using FlipsideMVC;

namespace FlipSide.Controllers
{
    public class StoriesController : Controller
    {
        private readonly FlipSideDataContext _context;

        public StoriesController(FlipSideDataContext context)
        {
            _context = context;
        }

        // GET: Stories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Story.ToListAsync());
        }

        //private IActionResult View(object p)
        //{
        //    throw new NotImplementedException();
        //}

        // GET: Stories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var story = await _context.Story
                .SingleOrDefaultAsync(m => m.id == id);
            if (story == null)
            {
                return NotFound();
            }

            return View(story);
        }

        // GET: Stories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,dateRan,slug,summary,byline,link,lean,topic")] Story story)
        {
            if (ModelState.IsValid)
            {
                _context.Add(story);
                await _context.SaveChangesAsync();
                //_context.Add(Topics);
                return RedirectToAction(nameof(Index));
            }
            return View(story);
        }

        // GET: Stories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var story = await _context.Story.SingleOrDefaultAsync(m => m.id == id);
            if (story == null)
            {
                return NotFound();
            }
            return View(story);
        }

        // POST: Stories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,dateRan,slug,summary,byline,link,lean")] Story story)
        {
            if (id != story.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(story);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoryExists(story.id))
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
            return View(story);
        }

        // GET: Stories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var story = await _context.Story
                .SingleOrDefaultAsync(m => m.id == id);
            if (story == null)
            {
                return NotFound();
            }

            return View(story);
        }

        // POST: Stories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var story = await _context.Story.SingleOrDefaultAsync(m => m.id == id);
            _context.Story.Remove(story);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoryExists(int id)
        {
            return _context.Story.Any(e => e.id == id);
        }
    }
}