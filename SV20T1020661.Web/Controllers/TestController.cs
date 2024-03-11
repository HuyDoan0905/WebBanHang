using Microsoft.AspNetCore.Mvc;

namespace SV20T1020661.Web.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Create()
        {
            var model = new Models.Person()
            {
                Name = "Đoàn Quốc Huy",
                Birthday = new DateTime(2002, 05, 09),
                Salary = 500m
            };
            return View();
        }
        public IActionResult Save(Models.Person model)
        {
            return Json(model);
        }
    }
}
