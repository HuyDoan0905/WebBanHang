using Microsoft.Data.SqlClient;

namespace SV20T1020661.DataLayers.SQL_Server
{
    /// <summary>
    /// lớp cha của các lớp cài đặt các phép xử lý dữ liệu trên SQL Server
    /// </summary>
    public abstract class _BaseDAL
    {
        protected string _connectionString = "";
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="connectionString"></param>
        public _BaseDAL(string connectionString)
        {
            _connectionString = connectionString;
        }
        /// <summary>
        /// tạo và mở kết nối đến SQl
        /// </summary>
        /// <returns></returns>
        protected SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();
            return connection;
        }
    }
}
