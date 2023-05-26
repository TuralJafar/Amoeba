using amoboe.DAL;
using amoboe.Models;
using amoboe.ViewModels.Account;
using amoboe.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using amoboe.Utilities;

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
            List<Person> Persons = await _context.Persons.Include(e => e.Team).ToListAsync();
            return View(Persons);
        }
        public IActionResult Create()
        {
            ViewBag.Team = _context.Teams;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeVM createPersonVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Team = _context.Teams;
                return View();
            }
            if (!createPersonVM.Photo.CheckFileType(createPersonVM.Photo.ContentType))
            {
                ViewBag.Team = _context.Teams;
                ModelState.AddModelError("Photo", "Faylin formati uygun deyil");
                return View();
            }
            if (!createPersonVM.Photo.CheckFileSize(200))
            {
                ViewBag.Team = _context.Teams;
                ModelState.AddModelError("Photo", "Faylin hecmi boyukdur");
                return View();
            }
            bool result = await _context.Teams.AnyAsync(p => p.Id == createPersonVM.TeamId);
            if (!result)
            {
                ViewBag.Team = _context.Teams;
                ModelState.AddModelError("PositionId", "Bele id'li position yoxdur");
                return View();
            }
            Person Person = new Person()
            {
                Name = createPersonVM.Name,
                Description = createPersonVM.Description,
                TeamId = createPersonVM.TeamId,
                Image = await createPersonVM.Photo.CreateFileAsync(_env.WebRootPath, "assets/img/team")
            };
            Person.Image = await createPersonVM.Photo.CreateFileAsync(_env.WebRootPath, "assets/img/team");
            await _context.Persons.AddAsync(Person);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            Person Person = await _context.Persons.FirstOrDefaultAsync(p => p.Id == id);
            if (Person == null) return NotFound();
            UpdateEmployeeVM updatePersonVM = new UpdateEmployeeVM()
            {
                Name = Person.Name,
                Description = Person.Description,
                PositionId = Person.TeamId,
                Image = Person.Image
            };
            ViewBag.Team = _context.Teams;
            return View(updatePersonVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateEmployeeVM updatePersonVM)
        {
            if (id == null || id < 1) return BadRequest();
            Person Person = await _context.Persons.FirstOrDefaultAsync(p => p.Id == id);
            if (Person == null) return NotFound();
            bool result = await _context.Teams.AnyAsync(p => p.Id == updatePersonVM.PositionId);

            if (!result)
            {
                ViewBag.Team = _context.Teams;
                ModelState.AddModelError("PositionId", "Bele id'li position yoxdur");
                updatePersonVM.Image = Person.Image;
                return View(updatePersonVM);
            }
            if (updatePersonVM == null)
            {
                if (!updatePersonVM.Photo.CheckFileType(updatePersonVM.Photo.ContentType))
                {
                    ViewBag.Team = _context.Teams;
                    ModelState.AddModelError("Photo", "Faylin formati uygun deyil");
                    updatePersonVM.Image = Person.Image;
                    return View(updatePersonVM);
                }
                if (!updatePersonVM.Photo.CheckFileSize(200))
                {
                    ViewBag.Team = _context.Teams;
                    ModelState.AddModelError("Photo", "Faylin hecmi boyukdur");
                    updatePersonVM.Image = Person.Image;
                    return View(updatePersonVM);
                }
                Person.Image.DeleteFile(_env.WebRootPath, "assets/img/team");
                Person.Image = await updatePersonVM.Photo.CreateFileAsync(_env.WebRootPath, "assets/img/team");
            }
            Person.Name = updatePersonVM.Name;
            Person.Description = updatePersonVM.Description;
            Person.TeamId = updatePersonVM.PositionId;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            Person Person = await _context.Persons.FirstOrDefaultAsync(p => p.Id == id);
            if (Person == null) return NotFound();
            Person.Image.DeleteFile(_env.WebRootPath, "assets/img/team");
            _context.Persons.Remove(Person);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }


    }
}
