using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork) { 
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> categories = _unitOfWork.CategoryRepository.GetAll().ToList();
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
                _unitOfWork.CategoryRepository.Add(category);
                _unitOfWork.Save();
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
            Category? category = _unitOfWork.CategoryRepository.Get(u=>u.CategoryId == Id);
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
                _unitOfWork.CategoryRepository.Update(category);
                _unitOfWork.Save();
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
            Category? category = _unitOfWork.CategoryRepository.Get(u=>u.CategoryId==Id);
            if (category == null)
            {
                return NotFound("Error");
            }
            return View(category);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? Id)
        {
            Category? category = _unitOfWork.CategoryRepository.Get(u => u.CategoryId == Id);

            if (category==null)
            {
                return NotFound();
            }
            _unitOfWork.CategoryRepository.Remove(category);
            _unitOfWork.Save();
            return RedirectToAction("Index", "Category");

            
        }

    }
}
