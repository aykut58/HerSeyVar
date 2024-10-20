using HerSeyVar.Models;
using HerSeyVar.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HerSeyVar.Controllers
{
    public class HomeController : Controller
    {
        private readonly RepositoryContext _context;

        public HomeController(RepositoryContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
