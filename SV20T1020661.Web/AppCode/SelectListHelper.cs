using Microsoft.AspNetCore.Mvc.Rendering;
using SV20T1020661.BusinessLayers;

namespace SV20T1020661.Web
{
    public static class SelectListHelper
    {
        /// <summary>
        /// Danh sách tỉnh / thành
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Pronvinces()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "-- Chọn Tỉnh/Thành --",

            });
            foreach (var item in CommonDataService.ListOfProvinces())
            {

                list.Add(new SelectListItem()
                {
                    Value = item.ProvinceName,
                    Text = item.ProvinceName,

                });
            }
            return list;

        }
    }
}
