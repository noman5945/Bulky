using Bulky.DataAccess.Repository;
using Bulky.Models.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment; //to access 'wwwroot' folder
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> products = _unitOfWork.ProductRepository.GetAll().ToList();
            return View(products);
        }

        //this action is used for both update and insert work and a single page can be used for both purposes 
        public IActionResult Upsert(int? Id)
        {
            //EF Core Projection-->
            /*IEnumerable<SelectListItem> CategoryList = _unitOfWork.CategoryRepository
                                                                   .GetAll()
                                                                   .Select(u => new SelectListItem
                                                                   {
                                                                       Text = u.CategoryName,
                                                                       Value= u.CategoryId.ToString(),
                                                                   }) ;*/
            //Sending to the front end using Viewbag
            //ViewBag.CategoryList = CategoryList;
            ProductVM productsVM = new()
            {
                CategoryList= _unitOfWork.CategoryRepository
                .GetAll()
                .Select(u=>new SelectListItem 
                { Text=u.CategoryName,
                    Value = u.CategoryId.ToString()
                }),
                Product = new Product()
            };
            if (Id == null || Id == 0)
            {
                //create
                return View(productsVM);
            }
            else 
            {
                //update 
                productsVM.Product = _unitOfWork.ProductRepository.Get(u => u.Id == Id);
                return View(productsVM);
            }
            
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM vm,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string wwwRoot = _webHostEnvironment.WebRootPath;
                if (file!=null) 
                {
                    string fileName=Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
                    string productPath=Path.Combine(wwwRoot,@"Images\Products");//creating filename and adding to path
                    using (var fileStream = new FileStream(Path.Combine(productPath,fileName),FileMode.Create)) 
                    {
                        file.CopyTo(fileStream);//coping file to the folder
                    }
                    vm.Product.ProductImage = @"Images\Products\" + fileName;
                }
                _unitOfWork.ProductRepository.Add(vm.Product);
                _unitOfWork.Save();
                return RedirectToAction("Index", "Product");
            }

            return View();
        }

        

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            Product? product = _unitOfWork.ProductRepository.Get(u => u.Id == Id);
            if (product == null)
            {
                return NotFound("Error");
            }
            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? Id)
        {
            Product? product = _unitOfWork.ProductRepository.Get(u => u.Id == Id);

            if (product == null)
            {
                return NotFound();
            }
            _unitOfWork.ProductRepository.Remove(product);
            _unitOfWork.Save();
            return RedirectToAction("Index", "Product");


        }
    }
}
