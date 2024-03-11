using Microsoft.AspNetCore.Mvc;
using SV20T1020661.BusinessLayers;
using SV20T1020661.DomainModels;

namespace SV20T1020661.Web.Controllers
{
    public class ShipperController : Controller
    {
        const int PAGE_SIZE = 20;

        public IActionResult Index(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfShippers(out rowCount, page, PAGE_SIZE, searchValue ?? "");

            var model = new Models.ShipperSearchResult()
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
            ViewBag.Title = "Bổ sung người giao hàng";
            var model = new Shipper()
            {
                ShipperId = 0,
            };
            return View("Edit", model);
        }

        public IActionResult Edit(int id=0)
        {
            ViewBag.Title = "Cập nhật thông tin người giao hàng";
            var model = CommonDataService.GetShipper(id);
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
                bool result = CommonDataService.DeleteShipper(id);
                return RedirectToAction("Index");
            }

            var model = CommonDataService.GetShipper(id);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
        [HttpPost] //attribute => chỉ nhận dữ liệu gửi lên dưới dạng post
        public IActionResult Save(Shipper model)
        {
            if (string.IsNullOrWhiteSpace(model.ShipperName))
                ModelState.AddModelError(nameof(model.ShipperName), "Tên không được để trống");
            if (string.IsNullOrWhiteSpace(model.Phone))
                ModelState.AddModelError(nameof(model.Phone), "Số điện thoại không được để trống");
            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.ShipperId == 0 ? "Bổ sung người giao hàng " : "Cập nhật thông tin người giao hàng";
                return View("Edit", model);
            }

            if (model.ShipperId == 0)
            {
                int id = CommonDataService.AddShipper(model);
                if (id <= 0)
                {
                    ModelState.AddModelError(nameof(model.Phone), "Số điện thoại bị trùng ");
                    ViewBag.Title = "Bổ sung người giao hàng";
                    return View("Edit", model);
                }
            }
            else
            {
                bool result = CommonDataService.UpdateShipper(model);
                if (!result)
                {
                    ModelState.AddModelError("Lỗi", "Không cập nhật được người giao hàng . Có thể số điện thoại bị trùng");
                    ViewBag.Title = "Cập nhật người giao hàng";
                    return View("Edit", model);
                }
            }
            return RedirectToAction("Index");
        }
    }
}