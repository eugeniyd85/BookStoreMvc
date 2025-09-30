using BookStoreMvc.DataAccess.Data;
using BookStoreMvc.DataAccess.Repository.IRepository;
using BookStoreMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
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
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
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
            Category? categoryToEdit = _unitOfWork.Category.Get(u=>u.Id==id); // can search only other field Id
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
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
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
            Category? categoryToEdit = _unitOfWork.Category.Get(u => u.Id == id); // can search only other field Id
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
            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }

    }
}
