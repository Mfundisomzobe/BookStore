using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using  BookStore.Data;
using  BookStore.Models;
using BookStore.Repository.IRepository;

namespace  BookStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _CategoryRepo;
        public CategoryController(ICategoryRepository db)
        {
            _CategoryRepo = db;
        }
       public IActionResult Index()
        {
            List<Category> categoryList = _CategoryRepo.GetAll().ToList();
            return View(categoryList);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _CategoryRepo.Add(obj);

                _CategoryRepo.Save();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            
            return View();

        }
        public IActionResult Edit(int? id)
        {
            if(id== null || id == 0)
            {
                return NotFound();
            }
           Category? categoryFrmDb = _CategoryRepo.Get(u=>u.CategoryId==id);
            if (categoryFrmDb == null)
            {
                return NotFound();
            }
            return View(categoryFrmDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _CategoryRepo.Update(obj);

                _CategoryRepo.Save();
                TempData["success"] = "Category Updated Successfully";
                return RedirectToAction(nameof(Index));

            }
            return View();

        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFrmDb = _CategoryRepo.Get(u => u.CategoryId == id);
            if (categoryFrmDb == null)
            {
                return NotFound();
            }
            return View(categoryFrmDb);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _CategoryRepo.Get(u => u.CategoryId == id);
            if (obj == null)
            {
                return NotFound();
            }

            _CategoryRepo.Remove(obj);

            _CategoryRepo.Save();
            TempData["success"] = "Category Deleted Successfully";

            return RedirectToAction(nameof(Index));

        }
    }
}
