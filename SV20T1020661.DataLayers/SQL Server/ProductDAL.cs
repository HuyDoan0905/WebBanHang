﻿using Dapper;
using SV20T1020661.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV20T1020661.DataLayers.SQL_Server
{
    public class ProductDAL : _BaseDAL, IProductDAL
    {
        public ProductDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(Product data)
        {
            int id = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"
                            insert into Products(ProductName,ProductDescription,SupplierID,CategoryID,Unit,Price,Photo,IsSelling)
                            values(@ProductName,@ProductDescription,@SupplierID,@CategoryID,@Unit,@Price,@Photo,@IsSelling
                            select @@identity;
                                ";
                var parameters = new
                {
                    ProductName = data.productName ?? "",
                    ProductDescription = data.productDescription ?? "",
                    SupplierID = data.SupplierId,
                    CategoryID = data.CategoryId,
                    Unit = data.Unit ?? "",
                    Price = data.Price,
                    Photo = data.Photo ?? "",
                    IsSelling = data.IsSelling
                };
                id = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }

            return id;
        }

        public long AddAttribute(ProductAttribute data)
        {
            int id = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"
                                    insert into ProductAttributes(ProductID,AttributeName,AttributeValue,DisplayOrder)
                                    values(@ProductID,@AttributeName,@AttributeValue,@DisplayOrder);

                                    select @@identity;
                                ";
                var parameters = new
                {
                    ProductID = data.ProductID,
                    AttributeName = data.AttributeName ?? "",
                    AttributeValue = data.AttributeValue ?? "",
                    DisplayOrder = data.DisplayOrder
                };
                id = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }

            return id;
        }

        public long AddPhoto(ProductPhoto data)
        {
            int id = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"
                                    insert into ProductPhotos(ProductID,Photo,Description,DisplayOrder,IsHidden)
                                    values(@ProductID,@Photo,@Description,@DisplayOrder,@IsHidden);

                                    select @@identity;
                                ";
                var parameters = new
                {
                    ProductID = data.ProductID,
                    Photo = data.Photo ?? "",
                    Description = data.Description ?? "",
                    DisplayOrder = data.DisplayOrder,
                    IsHidden = data.IsHidden
                };
                id = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }

            return id;
        }

        public int Count(string searchValue = "", int categoryID = 0, int supplierID = 0, decimal minPrice = 0, decimal maxPrice = 0)
        {
            int count = 0;

            if (!String.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";

            using (var connection = OpenConnection())
            {
                var sql = @"select count(*) from Products 
                            where (@SearchValue = N'' or ProductName like @SearchValue)
                                    and (@CategoryID = 0 or CategoryID = @CategoryID)
                                    and (@SupplierID = 0 or SupplierId = @SupplierID)
                                    and (Price >= @MinPrice)
                                    and (@MaxPrice <= 0 or Price <= @MaxPrice))";
                var parameters = new
                {
                    searchValue = searchValue ?? "",
                    CategoryID = categoryID,
                    SupplierID = supplierID,
                    MinPrice = minPrice,
                    MaxPrice = maxPrice

                };
                count = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return count;
        }

        public bool Delete(int productID)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"delete from Products where ProductID = @ProductID";
                var parameters = new
                {
                    ProductID = productID,
                };

                result = connection.Execute(sql: sql, param: parameters, commandType: CommandType.Text) > 0;

                connection.Close();
            }

            return result;
        }

        public bool DeleteAttribute(long attributeID)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"delete from ProductAttributes where AttributeID = @AttributeID";
                var parameters = new
                {
                    AttribureID = attributeID
                };
                //Thực thi câu lệnh
                result = connection.Execute(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
                //Đóng kết nối
                connection.Close();
            }

            return result;
        }

        public bool DeletePhoto(long photoID)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"delete from ProductPhotos where PhotoID = @PhotoID";
                var parameters = new
                {
                    PhotoID = photoID
                };
                //Thực thi câu lệnh
                result = connection.Execute(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
                //Đóng kết nối
                connection.Close();
            }

            return result;
        }

        public Product? Get(int productID)
        {
            Product? data = null;
            using (var connection = OpenConnection())
            {
                var sql = @"select * from Products where ProductID = @ProductID";
                var parameters = new
                {
                    ProductID = productID
                };
                data = connection.QueryFirstOrDefault<Product>(sql: sql, param: parameters,
                                                                commandType: CommandType.Text);
                connection.Close();
            }
            return data;
        }

        public ProductAttribute? GetAttribute(long attributeID)
        {
            ProductAttribute? data = null;
            using (var connection = OpenConnection())
            {
                var sql = @"select * from ProductAttributes where AttributeID = @AttributeID";
                var parameters = new
                {
                    AttributeID = attributeID
                };
                data = connection.QueryFirstOrDefault<ProductAttribute>(sql: sql, param: parameters,
                                                                commandType: CommandType.Text);
                connection.Close();
            }
            return data;
        }

        public ProductPhoto? GetPhoto(long photoID)
        {
            ProductPhoto? data = null;
            using (var connection = OpenConnection())
            {
                var sql = @"select * from ProductPhotos where PhotoID = @PhotoID";
                var parameters = new
                {
                    PhotoID = photoID
                };
                data = connection.QueryFirstOrDefault<ProductPhoto>(sql: sql, param: parameters,
                                                                commandType: CommandType.Text);
                connection.Close();
            }
            return data;
        }

        public bool InUsed(int productID)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"if exists(select * from OrderDetails where ProductID = @ProductID)
                                select 1
                            else 
                                select 0";
                var parameters = new
                {
                    ProductID = productID
                };
                result = connection.ExecuteScalar<bool>(sql: sql, param: parameters,
                                                commandType: CommandType.Text);
            }
            return result;
        }

        public IList<Product> List(int page = 1, int pageSize = 0, string searchValue = "", int categoryID = 0, int supplierID = 0, decimal minPrice = 0, decimal maxPrice = 0)
        {
            List<Product> list = new List<Product>();
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";

            using (var connection = OpenConnection())
            {
                var sql = @"with cte as(
                                    select  *, row_number() over(order by ProductName) as RowNumber
                                    from    Products
                                    where   (@SearchValue = N'' or ProductName like @SearchValue)
                                        and (@CategoryID = 0 or CategoryID = @CategoryID)
                                        and (@SupplierID = 0 or SupplierId = @SupplierID)
                                        and (Price >= @MinPrice)
                                        and (@MaxPrice <= 0 or Price <= @MaxPrice)
                                     )
                                    select * from cte
                                    where   (@PageSize = 0)
                                        or (RowNumber between (@Page - 1)*@PageSize + 1 and @Page * @PageSize)";
                var parameters = new
                {
                    Page = page,
                    PageSize = pageSize,
                    SearchValue = searchValue ?? "",
                    CategoryID = categoryID,
                    SupplierID = supplierID,
                    MinPrice = minPrice,
                    MaxPrice = maxPrice
                };
                list = connection.Query<Product>(sql: sql, param: parameters, commandType: CommandType.Text)
                    .ToList();
                connection.Close();
            }

            return list;
        }

        public IList<ProductAttribute> ListAttributes(int productID)
        {
            List<ProductAttribute> list = new List<ProductAttribute>();

            using (var connection = OpenConnection())
            {
                var sql = @"select * from ProductAttributes where ProductID = @ProductID
                            order by DisplayOrder";
                var parameters = new
                {
                    ProductID = productID
                };
                list = connection.Query<ProductAttribute>(sql: sql, param: parameters, commandType: CommandType.Text)
                    .ToList();
                connection.Close();
            }

            return list;
        }

        public IList<ProductPhoto> ListPhotos(int productID)
        {
            List<ProductPhoto> list = new List<ProductPhoto>();

            using (var connection = OpenConnection())
            {
                var sql = @"select * from ProductPhotos where ProductID = @ProductID
                            order by DisplayOrder";
                var parameters = new
                {
                    ProductID = productID
                };
                list = connection.Query<ProductPhoto>(sql: sql, param: parameters, commandType: CommandType.Text)
                    .ToList();
                connection.Close();
            }

            return list;
        }

        public bool Update(Product data)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"
                                    update Products 
                                    set ProductName = @ProductName,
                                        ProductDescription = @ProductDescription,
                                        SupplierID = @SupplierID,
                                        CategoryID = @CategoryID,
                                        Unit = @Unit,
                                        Price = @Price,
                                        Photo = @Photo,
                                        IsSelling = @IsSelling
                                   where ProductID = @ProductID
                               ";
                var parameters = new
                {
                    ProductID = data.productID,
                    ProductName = data.productName ?? "",
                    ProductDescription = data.productDescription ?? "",
                    SupplierID = data.SupplierId,
                    CategoryID = data.CategoryId,
                    Unit = data.Unit ?? "",
                    Price = data.Price,
                    Photo = data.Photo ?? "",
                    IsSelling = data.IsSelling

                };
                result = connection.Execute(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
            }

            return result;
        }

        public bool UpdateAttribute(ProductAttribute data)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"
                                    update ProductAttributes 
                                    set AttributeName = @AttributeName,
                                        AttributeValue = @AttributeValue,
                                        ProductID = @ProductID,
                                        DisplayOrder = @DisplayOrder
                                   where AttributeID = @AttributeID
                               ";
                var parameters = new
                {
                    AttributeName = data.AttributeName ?? "",
                    AttributeValue = data.AttributeValue ?? "",
                    ProductID = data.ProductID,
                    DisplayOrder = data.DisplayOrder,
                    AttributeID = data.AttributeId

                };
                result = connection.Execute(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
            }

            return result;
        }

        public bool UpdatePhoto(ProductPhoto data)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"
                                    update ProductPhotos 
                                    set Photo = @Photo,
                                        Description = @Description,
                                        ProductID = @ProductID,
                                        DisplayOrder = @DisplayOrder
                                        IsHidden = @IsHidden
                                   where PhotoID = @PhotoID
                               ";
                var parameters = new
                {
                    Photo = data.Photo ?? "",
                    Description = data.Description ?? "",
                    ProductID = data.ProductID,
                    DisplayOrder = data.DisplayOrder,
                    IsHidden = data.IsHidden,
                    PhotoID = data.PhotoID

                };
                result = connection.Execute(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
            }

            return result;
        }
    }
}

