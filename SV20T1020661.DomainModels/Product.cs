using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV20T1020661.DomainModels
{
    public class Product
    {
        public int productID {  get; set; }
        public string productName { get; set; } = "";
        public string productDescription { get; set; } = "";
        public int SupplierId { get; set; }

        public int CategoryId { get; set; }
        public string Unit { get; set; } = "";
        public decimal Price { get; set; }
        public string Photo { get; set; } = "";
        public bool IsSelling {  get; set; }



    }
}
