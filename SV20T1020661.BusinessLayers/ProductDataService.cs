using SV20T1020661.DataLayers;
using SV20T1020661.DataLayers.SQL_Server;
using SV20T1020661.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV20T1020661.BusinessLayers
{
    public class ProductDataService
    {
        private static readonly IProductDAL productDB;

        /// <summary>
        /// ctor
        /// </summary>
        static ProductDataService()
        {
            productDB = new ProductDAL(Configuration.ConnectionString);
        }

        /// <summary>
        /// Tìm kiếm và lấy danh sách mặt hàng dưới dạng phân trang
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="categoryID"></param>
        /// <param name="supplierID"></param>
        /// <param name="minPrice"></param>
        /// <param name="maxPrice"></param>
        /// <returns></returns>
        public static List<Product> ListProducts(out int rowCount, int page = 1, int pageSize = 0,
                                                string searchValue = "", int categoryID = 0, int supplierID = 0,
                                                decimal minPrice = 0, decimal maxPrice = 0)
        {
            rowCount = productDB.Count(searchValue,categoryID,supplierID,minPrice,maxPrice);
            return productDB.List(page,pageSize,searchValue,categoryID,supplierID,minPrice, maxPrice).ToList();
        }

        /// <summary>
        /// Lấy thông tin 1 mặt hàng theo mã mặt hàng
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>

        public static Product? GetProduct(int productID)
        {
            return productDB.Get(productID);
        }

        /// <summary>
        /// Bổ sung mặt hàng mới
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>

        public static int AddProduct(Product product)
        {
            return productDB.Add(product);
        }
        /// <summary>
        /// Cập nhật thông tin mặt hàng
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static bool UpdateProduct(Product product)
        {
            return productDB.Update(product);
        }
        /// <summary>
        /// Xóa mặt hàng theo mã hàng
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static bool DeleteProduct(int productID)
        {
            if(productDB.InUsed(productID))
            {
                return false;
            }
            return productDB.Delete(productID);
        }
        /// <summary>
        /// Kiểm tra mặt hàng có dữ liệu liên quan hay không
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static bool InUsedProduct(int productID)
        {
            return productDB.InUsed(productID);
        }
        /// <summary>
        /// Lấy danh sách ảnh của mặt hàng theo mã hàng
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static List<ProductPhoto> ListPhotos(int productID)
        {
            return productDB.ListPhotos(productID).ToList();
        }
        /// <summary>
        /// Lấy thông tin 1 ảnh theo mã ảnh
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        public static ProductPhoto? GetPhoto(long photoID)
        {
            return productDB.GetPhoto(photoID);
        }
        /// <summary>
        /// Thêm mới 1 ảnh của mặt hàng
        /// </summary>
        /// <param name="photo"></param>
        /// <returns></returns>
        public static long AddPhoto(ProductPhoto photo)
        {
            return productDB.AddPhoto(photo);
        }
        /// <summary>
        /// Xóa 1 ảnh của mặt hàng
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        public static bool DeletePhoto(long photoID)
        {
            return productDB.DeletePhoto(photoID);
        }
        /// <summary>
        /// Cập nhật ảnh của mặt hàng
        /// </summary>
        /// <param name="photo"></param>
        /// <returns></returns>
        public static bool UpdatePhoto(ProductPhoto photo)
        {
            return productDB.UpdatePhoto(photo);
        }
        /// <summary>
        /// Lấy danh sách thuộc tính của 1 mặt hàng
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public List<ProductAttribute> ListAttributes(int productID)
        {
            return productDB.ListAttributes(productID).ToList();
        }
        /// <summary>
        /// Lấy 1 thuộc tính của mặt hàng 
        /// </summary>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        public static ProductAttribute? GetAttribute(long attributeID)
        {
            return productDB.GetAttribute(attributeID);
        }
        /// <summary>
        /// Bổ sung thuộc tính cho mặt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static long AddAttribute(ProductAttribute data)
        {
            return productDB.AddAttribute(data);
        }
        /// <summary>
        /// Xóa thuộc tính theo mã thuộc tính
        /// </summary>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        public static bool DeleteAttribute(long attributeID)
        {
            return productDB.DeleteAttribute(attributeID);
        }
        /// <summary>
        /// Cập nhật thuộc tính
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateAttribute(ProductAttribute data)
        {
            return productDB.UpdateAttribute(data);
        }
    }
}
