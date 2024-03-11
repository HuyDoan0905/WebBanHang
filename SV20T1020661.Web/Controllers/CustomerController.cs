using Microsoft.AspNetCore.Mvc;
using SV20T1020661.BusinessLayers;
using SV20T1020661.DomainModels;
using SV20T1020661.Web.Models;
using System.Buffers;

namespace SV20T1020661.Web.Controllers
{
    public class CustomerController : Controller
    {
        const int PAGE_SIZE = 20;
        const string CREATE_TITLE = "Nhập khách hàng mới";
        const string CUSTOMER_SEARCH = "customer_search";
        public IActionResult Index()
        {
            // kiểm tra xem ở session có lưu điều kiện tìm kiếm kh
            //nếu có thì sử dụng lại điều kiện tìm, ngược lại thì tìm kiếm theo dk mặc định
            Models.PaginationSearchInput? input = ApplicationContext.GetSessionData<PaginationSearchInput>(CUSTOMER_SEARCH);
            if (input == null)
            {
                input = new PaginationSearchInput
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    searchValue = "",
                };

            }
            return View(input);

        }
        public IActionResult Search(PaginationSearchInput input)
        {
            int rowCount = 0;
            var data=CommonDataService.ListOfCustomers(out rowCount,input.Page,input.PageSize,input.searchValue??"");
            var model = new CustomerSearchResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.searchValue ?? "",
                RowCount = rowCount,
                Data = data
            };
            ApplicationContext.SetSessionData(CUSTOMER_SEARCH,input);
            return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.Title = "Bổ sung khách hàng";
            var model = new Customer()
            {
                CustomerID = 0
            };
            // dùng chung giao diện với Edit
            return View("Edit", model);
        }

        public IActionResult Edit(int id = 0)
        {
            ViewBag.Title = "Cập nhật thông tin khách hàng";
            var model = CommonDataService.GetCustomer(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }
        [HttpPost] //Attribute (chỉ nhận dữ liệu gửi lên dưới dạng là POST)
        public IActionResult Save(Customer model)  // viết tường minh :  int customerID , string custormerName ,....
        {
            //TODO : Kiểm soát dữ liệu trong model xem có hợp lệ hay không 
            //Yêu cầu : Tên khách hàng , tên giao dịch ,Email và tỉnh thành không được để trống 
            if (string.IsNullOrWhiteSpace(model.CustomerName))
            {
                ModelState.AddModelError(nameof(model.CustomerName), "Tên không được để trống");
            }
            if (string.IsNullOrWhiteSpace(model.ContactName))
            {
                ModelState.AddModelError(nameof(model.ContactName), "Tên giao dịch không được để trống");
            }
            if (string.IsNullOrWhiteSpace(model.Email))
            {
                ModelState.AddModelError(nameof(model.Email), "Email không được để trống");
            }
            if (string.IsNullOrWhiteSpace(model.Province))
            {
                ModelState.AddModelError(nameof(model.Province), "Vui lòng chọn tỉnh/thành ");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.CustomerID == 0 ? "Bổ sung khách hàng" : "Cập nhật thông tin khách hàng";
                return View("Edit", model);
            }

            if (model.CustomerID == 0)
            {
                int id = CommonDataService.AddCustomer(model);
                if (id <= 0)
                {
                    ModelState.AddModelError(nameof(model.Email), "Email bị trùng ");
                    ViewBag.Title = "Bổ sung khách hàng";
                    return View("Edit", model);
                }
            }
            else
            {
                bool result = CommonDataService.UpdateCustomer(model);
                if (!result)
                {
                    ModelState.AddModelError("Lỗi", "Không cập nhật được khách hàng . Có thể email bị trùng");
                    ViewBag.Title = "Cập nhật khách hàng";
                    return View("Edit", model);
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id = 0)
        {
            if (Request.Method == "POST")
            {
                bool result = CommonDataService.DeleteCustomer(id);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetCustomer(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}

