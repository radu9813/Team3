using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using RestSharp;
using Team3Assig.Data;
using Team3Assig.Models;

namespace Team3Assig.Controllers
{
    public class DiplomataController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly string apiKey;


        /// https://core.ac.uk:443/api-v2/articles/search/{search}?page=1&pageSize=10&metadata=true&fulltext=false&citations=false&similar=false&duplicate=false&urls=false&faithfulMetadata=false&apiKey={apikey}

        public DiplomataController(ApplicationDbContext context, IDiplomataControllerSettings diplomataControllerSettings)
        {
            apiKey = diplomataControllerSettings.ApiKey;
            _context = context;
        }
        

        // GET: Diplomata
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Diploma.Include(d => d.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Diplomata/Details/5
        public async Task<IActionResult> Details(int? id)
        {
                    
            if (id == null)
            {
                return NotFound();
            }

            var diploma = await _context.Diploma
                .Include(d => d.Student)
                .FirstOrDefaultAsync(m => m.DiplomaId == id);
            if (diploma == null)
            {
                return NotFound();
            }

            string thesis = diploma.Thesis.Replace(" ", "");

            var client = new RestClient($"https://core.ac.uk:443/api-v2/articles/search/{thesis}?page=1&pageSize=10&metadata=true&fulltext=false&citations=false&similar=false&duplicate=false&urls=false&faithfulMetadata=false&apiKey={apiKey}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            ParseDataApi(response.Content);

            return View(diploma);
        }

        // GET: Diplomata/Create
        public IActionResult Create()
        {
            ViewData["DiplomaId"] = new SelectList(_context.Student, "StudentId", "StudentId");
            return View();
        }

        // POST: Diplomata/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiplomaId,Thesis,Abstract,Completeness,Supervisor")] Diploma diploma)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diploma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiplomaId"] = new SelectList(_context.Student, "StudentId", "StudentId", diploma.DiplomaId);
            return View(diploma);
        }

        // GET: Diplomata/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diploma = await _context.Diploma.FindAsync(id);
            if (diploma == null)
            {
                return NotFound();
            }
            ViewData["DiplomaId"] = new SelectList(_context.Student, "StudentId", "StudentId", diploma.DiplomaId);
            return View(diploma);
        }

        // POST: Diplomata/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiplomaId,Thesis,Abstract,Completeness,Supervisor")] Diploma diploma)
        {
            if (id != diploma.DiplomaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diploma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiplomaExists(diploma.DiplomaId))
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
            ViewData["DiplomaId"] = new SelectList(_context.Student, "StudentId", "StudentId", diploma.DiplomaId);
            return View(diploma);
        }

        // GET: Diplomata/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diploma = await _context.Diploma
                .Include(d => d.Student)
                .FirstOrDefaultAsync(m => m.DiplomaId == id);
            if (diploma == null)
            {
                return NotFound();
            }

            return View(diploma);
        }

        // POST: Diplomata/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diploma = await _context.Diploma.FindAsync(id);
            _context.Diploma.Remove(diploma);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiplomaExists(int id)
        {
            return _context.Diploma.Any(e => e.DiplomaId == id);
        }

        public List<ArticleRecord> ParseDataApi(string content)
        {
            var json = JObject.Parse(content);

            var result = new List<ArticleRecord>();

            var data = json["data"].Take(5);
            foreach (var item in data)
            {
                int id = item.Value<int>("id");
                List<string> authors = new List<string>();
                foreach(var author in item["authors"])
                {
                    authors.Add(author.ToString());
                }

                string title = item.Value<string>("title");
                List<string> topics = new List<string>();
                foreach (var topic in item["topics"])
                {
                    topics.Add(topic.ToString());
                }
                string downloadURL = item.Value<string>("downloadUrl");

                result.Add(new ArticleRecord(id, authors, title, topics, downloadURL));
            }

            return result;
        }
    }
}
