using amoboe.DAL;
using amoboe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace amoboe.Areas.AmoboeAdmin.Controllers
{
    [Area("AmoboeAdmin")]
    public class ServiceController : Controller
    {   private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ServiceController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Person> Persons = await _context.Persons.Include(p => p.Team).ToListAsync();
            return View(Persons);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Teams = await _context.Teams.ToListAsync();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Person person)
        {   if(ModelState.IsValid) return View();
            bool result = await _context.Persons.AnyAsync(c => c.Name.ToLower() == person.Name.ToLower());
            if (!result)
            {
                ModelState.AddModelError("Name", "not found");
                return View();
            }
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
           
        }
        public IActionResult Update()
        {
            return View();
        }
        public async Task<IActionResult> Delete()
        {
            return View();
        }
    }
}
