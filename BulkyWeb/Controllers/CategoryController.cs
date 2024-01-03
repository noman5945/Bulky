using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _db;
        public CategoryController(ApplicationDBContext db) { 
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> categories = _db.Categories.ToList();
            //Sending the list to the view to display at frontend
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category) {

            if (category.CategoryName==category.DisplayOrder.ToString()) {
                ModelState.AddModelError("CategoryName", "Name and Display order connot be same");
            }
            if (ModelState.IsValid) {
                _db.Categories.Add(category);
                _db.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
            
            return View();
        }

        public IActionResult Edit(int? Id)
        {   
            if (Id == null || Id==0)
            {
                return NotFound();
            }
            Category? category = _db.Categories.FirstOrDefault(u=>u.CategoryId==Id);
            if (category == null)
            {
                return NotFound("Error");
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {

            if (category.CategoryName == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CategoryName", "Name and Display order connot be same");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                return RedirectToAction("Index", "Category");
            }

            return View();
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Category? category = _db.Categories.FirstOrDefault(u => u.CategoryId == Id);
            if (category == null)
            {
                return NotFound("Error");
            }
            return View(category);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? Id)
        {
            Category? category = _db.Categories.FirstOrDefault(u => u.CategoryId == Id);
            
            if (category==null)
            {
                return NotFound();
            }
            _db.Categories.Remove(category);
            _db.SaveChanges();
            return RedirectToAction("Index", "Category");

            
        }

    }
}
