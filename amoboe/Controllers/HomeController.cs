using amoboe.DAL;
using amoboe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace amoboe.Controllers
{
    public class HomeController : Controller
    { private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async  Task<IActionResult> Index()
        {   List<Person> persons = await _context.Persons.Include(p=>p.Team).ToListAsync();
            return View(persons);
        }

        
    }
}