using BookStoreMvc.DataAccess.Data;
using BookStoreMvc.DataAccess.Repository.IRepository;
using BookStoreMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreMvc.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository db)
        {
            _categoryRepository = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _categoryRepository.GetAll().ToList();
            return View(objCategoryList);
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
                _categoryRepository.Add(obj);
                _categoryRepository.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryToEdit = _categoryRepository.Get(u=>u.Id==id); // can search only other field Id
            // Category? categoryToEdit1 = _db.Categories.FirstOrDefault(u=>u.Id==id); // can search by other fields than Id and by their partial entries
            // Category? categoryToEdit2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault(); // can be done more filtering or calculations before getting FirstOrDefault
            if (categoryToEdit == null)
            {
                return NotFound();
            }
            return View(categoryToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Update(obj);
                _categoryRepository.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryToEdit = _categoryRepository.Get(u => u.Id == id); // can search only other field Id
            // Category? categoryToEdit1 = _db.Categories.FirstOrDefault(u=>u.Id==id); // can search by other fields than Id and by their partial entries
            // Category? categoryToEdit2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault(); // can be done more filtering or calculations before getting FirstOrDefault
            if (categoryToEdit == null)
            {
                return NotFound();
            }
            return View(categoryToEdit);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _categoryRepository.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _categoryRepository.Remove(obj);
            _categoryRepository.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }

    }
}
