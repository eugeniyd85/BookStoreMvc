using BookStoreRazor.Data;
using BookStoreRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStoreRazor.Pages.Categories
{
    [BindProperties] // To bind all properties defined in Page Model
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        //[BindProperty] // To bind the form data to this property. Can be used in both OnGet and OnPost methods.
        public Category Category { get; set; }
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            _db.Categories.Add(Category);
            _db.SaveChanges();
            TempData["success"] = "Category created successfully";
            return RedirectToPage("Index");
        }
    }
}
