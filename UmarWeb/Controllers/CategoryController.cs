using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UmarWeb.Data;
using UmarWeb.Models;

namespace UmarWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db=db;
        }
        public IActionResult Index()
        {
            List<Category> categoryList =_db.Categories.ToList();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
