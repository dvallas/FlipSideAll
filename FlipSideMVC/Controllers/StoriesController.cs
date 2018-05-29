using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlipSideModels;
using FlipsideMVC;
using FlipSideDataAccess;

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

        private Story CreateStoryObject(IEnumerable<string> vals)
        {
            //int thisLean =int.Parse(vals.ElementAt(5));
            try
            {
                //var stry = (Story) vals;
                var stry = new Story()
                {
                    dateRan = DateTime.Parse(vals.ElementAt(0)),
                    slug = vals.ElementAt(1),
                    summary = vals.ElementAt(2),
                    link = vals.ElementAt(3),
                    byline = vals.ElementAt(4),
                    //lean = thisLean,
                    lean = 1,
                    topic = vals.ElementAt(6)
                };
                return stry;
            }catch(Exception e)

            {
                return new Story();
            }

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
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("id,dateRan,slug,summary,byline,link,lean,topic")] Story story)
        //public async Task<IActionResult> Create(int id, DateTime dateRan, string slug, string summary, string byline, string link, int lean, string topic)
        public async Task<IActionResult> Create(IEnumerable<string> vals)
        {
            var stry = CreateStoryObject(vals);
            var result = new DA().WriteStory(stry);
            if (!ModelState.IsValid) return View(stry);
            return RedirectToAction(nameof(Index));

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