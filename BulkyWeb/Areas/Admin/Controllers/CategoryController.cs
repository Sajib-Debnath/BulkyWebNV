using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
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
            List<Category> categoryList = _unitOfWork.Category.GetAll().OrderBy(u => u.DisplayOrder).ToList();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.CategoryName == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CategoryName", "Display Order can not be matched with Category Name.");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
                TempData["success"] = "Category is created successfully";
                return RedirectToAction("Index");
            }

            return View(category);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category? category = _unitOfWork.Category.Get(u => u.CategoryId == id);
            if (category == null)
            {
                return BadRequest();
            }
            return View(category);
        }


        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (category.CategoryName == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CategoryName", "Display Order can not be matched with Category Name.");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
                TempData["success"] = "Category is updated successfully";
                return RedirectToAction("Index");
            }

            return View(category);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category? category = _unitOfWork.Category.Get(u => u.CategoryId == id);
            if (category == null)
            {
                return BadRequest();
            }
            return View(category);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(Category category)
        {
            if (category != null)
            {
                _unitOfWork.Category.Delete(category);
                _unitOfWork.Save();
                TempData["success"] = "Category is deleted successfully";
                return RedirectToAction("Index");
            }

            return View(category);
        }
    }
}
