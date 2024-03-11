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
    public class ShipperDAL : _BaseDAL, ICommonDAL<Shipper>
    {
        public ShipperDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(Shipper data)
        {
            int id = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"
                                    insert into Shippers(ShipperName,Phone)
                                    values(@ShipperName,@Phone);

                                    select @@identity;
                               ";
                var parameters = new
                {
                    ShipperName = data.ShipperName ?? "",
                    Phone = data.Phone ?? ""

                };
                id = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }

            return id;
        }

        public int Count(string searchValue = "")
        {
            int count = 0;

            if (!String.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";

            using (var connection = OpenConnection())
            {
                var sql = @"select count(*) from Shippers 
                            where (@searchValue = N'') or (ShipperName like @searchValue)";
                var parameters = new
                {
                    searchValue = searchValue ?? ""
                };
                count = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return count;
        }

        public bool Delete(int id)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"delete from Shippers where ShipperID = @ShipperID";
                var parameters = new
                {
                    ShipperID = id,
                };
                //Thực thi câu lệnh
                result = connection.Execute(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
                //Đóng kết nối
                connection.Close();
            }

            return result;
        }

        public Shipper? Get(int id)
        {
            Shipper? data = null;
            using (var connection = OpenConnection())
            {
                var sql = @"select * from Shippers where ShipperID = @ShipperID";
                var parameters = new
                {
                    ShipperID = id
                };
                data = connection.QueryFirstOrDefault<Shipper>(sql: sql, param: parameters,
                                                                commandType: CommandType.Text);
                connection.Close();
            }
            return data;
        }

        public bool IsUsed(int id)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"if exists(select * from Orders where ShipperID = @ShipperID)
                                select 1
                            else 
                                select 0";
                var parameters = new
                {
                    ShipperID = id
                };
                result = connection.ExecuteScalar<bool>(sql: sql, param: parameters,
                                                commandType: CommandType.Text);
            }
            return result;
        }

        public IList<Shipper> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            List<Shipper> list = new List<Shipper>();
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";

            using (var connection = OpenConnection())
            {
                var sql = @"with cte as
                            (
	                            select	*, row_number() over (order by ShipperName) as RowNumber
	                            from	Shippers
	                            where	(@searchValue = N'') or (ShipperName like @searchValue)
                            )
                            select * from cte
                            where  (@pageSize = 0) 
                                or (RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize)
                            order by RowNumber";
                var parameters = new
                {
                    page = page,
                    pageSize = pageSize,
                    searchValue = searchValue ?? ""
                };
                list = connection.Query<Shipper>(sql: sql, param: parameters, commandType: CommandType.Text)
                    .ToList();
                connection.Close();
            }
            return list;
        }

        public bool Update(Shipper data)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"
                                    update Shippers 
                                    set ShipperName = @ShipperName,
                                        Phone = @Phone
                                        
                                   where ShipperID = @ShipperID
                               ";
                var parameters = new
                {
                    ShipperID = data.ShipperId,
                    ShipperName = data.ShipperName ?? "",
                    Phone = data.Phone ?? ""


                };
                result = connection.Execute(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
            }

            return result;
        }
    }
}
