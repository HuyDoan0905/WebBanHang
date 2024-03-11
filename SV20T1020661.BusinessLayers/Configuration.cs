using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV20T1020661.BusinessLayers
{
    /// <summary>
    /// khởi tạo và lưu trữ các thông tin cấu hình của Businesslayer
    /// </summary>
    public static class Configuration
    {
        public static string ConnectionString { get; private set; } = "";
        /// <summary>
        /// khởi tạo cấu hình cho Businesslayer
        /// </summary>
        /// <param name="connectionString"></param>
        public static void Initialize(string connectionString)
        {
            Configuration.ConnectionString = connectionString;
        }
    }
}
