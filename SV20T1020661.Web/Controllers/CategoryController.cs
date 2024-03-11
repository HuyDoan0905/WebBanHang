using Microsoft.AspNetCore.Mvc;
using SV20T1020661.BusinessLayers;
using SV20T1020661.DomainModels;

namespace SV20T1020661.Web.Controllers
{
    public class CategoryController : Controller
    {
        const int PAGE_SIZE = 20;
        public IActionResult Index(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfCategories(out rowCount, page, PAGE_SIZE, searchValue ?? "");

            var model = new Models.CategorySearchResult()
            {
                Page = page,
                PageSize = PAGE_SIZE,
                SearchValue = searchValue ?? "",
                RowCount = rowCount,
                Data = data
            };
            return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.Title = "Bổ sung loại hàng";
            var model = new Category()
            {
                CategoryId = 0,
            };
            return View("Edit", model);
        }

        public IActionResult Edit(int id = 0)
        {
            ViewBag.Title = "Chỉnh sửa thông tin loại hàng";
            var model = CommonDataService.GetCategory(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }
        public IActionResult Delete(int id = 0)
        {
            if (Request.Method == "POST")
            {
                bool result = CommonDataService.DeleteCategory(id);
                return RedirectToAction("Index");
            }

            var model = CommonDataService.GetCategory(id);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        [HttpPost]
        public IActionResult Save(Category model)
        {
            if (string.IsNullOrWhiteSpace(model.CategoryName))
                ModelState.AddModelError(nameof(model.CategoryName), "Tên không được để trống");
            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.CategoryId == 0 ? "Bổ sung loại hàng " : "Cập nhật thông tin loại hàng";
                return View("Edit", model);
            }
            if (model.CategoryId == 0)
            {
                int id = CommonDataService.AddCategory(model);
            }
            else
            {
                bool result = CommonDataService.UpdateCategory(model);
            }
            return RedirectToAction("Index");
        }
    }
}
