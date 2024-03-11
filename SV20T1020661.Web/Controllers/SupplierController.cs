using Microsoft.AspNetCore.Mvc;
using SV20T1020661.BusinessLayers;
using SV20T1020661.DomainModels;

namespace SV20T1020661.Web.Controllers
{
    public class SupplierController : Controller
    {
        const int PAGE_SIZE = 20;

        public IActionResult Index(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfSuppliers(out rowCount, page, PAGE_SIZE, searchValue ?? "");

            var model = new Models.SupplierSearchResult()
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
            ViewBag.Title = "Bổ sung nhà cung cấp";
            var model = new Supplier()
            {
                SupplierId = 0
            };
            return View("Edit", model);
        }

        public IActionResult Edit(int id=0)
        {
            ViewBag.Title = "Cập nhật thông tin nhà cung cấp";
            var model = CommonDataService.GetSupplier(id);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
        [HttpPost]//attribute: chỉ nhận dữ liệu gửi lên dưới dạng post
        public IActionResult Save(Supplier model)  // viết tường minh :  int customerID , string custormerName ,....
        {
            if (string.IsNullOrWhiteSpace(model.SupplierName))
                ModelState.AddModelError(nameof(model.SupplierName), "Tên không được để trống");
            if (string.IsNullOrWhiteSpace(model.ContactName))
                ModelState.AddModelError(nameof(model.ContactName), "Tên giao dịch không được để trống");
            if (string.IsNullOrWhiteSpace(model.Email))
                ModelState.AddModelError(nameof(model.Email), "Email không được để trống");
            if (string.IsNullOrWhiteSpace(model.Province))
                ModelState.AddModelError(nameof(model.Province), "Vui lòng chọn tỉnh thành");
            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.SupplierId == 0 ? "Bổ sung nhà cung cấp " : "Cập nhật thông tin nhà cung cấp";
                return View("Edit", model);
            }
            if (model.SupplierId == 0)
            {
                int id = CommonDataService.AddSupplier(model );
                if (id <= 0)
                {
                    ModelState.AddModelError(nameof(model.Email), "Email bị trùng ");
                    ViewBag.Title = "Bổ sung khách hàng";
                    return View("Edit", model);
                }
            }
            else
            {
                bool result = CommonDataService.UpdateSupplier(model);
                if (!result)
                {
                    ModelState.AddModelError("Lỗi", "Không cập nhật được nhà cung cấp . Có thể email bị trùng");
                    ViewBag.Title = "Cập nhật nhà cung cấp";
                    return View("Edit", model);
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id=0)
        {
            if (Request.Method == "POST")
            {
                bool result = CommonDataService.DeleteSupplier(id);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetSupplier(id);
            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }
    }
}
