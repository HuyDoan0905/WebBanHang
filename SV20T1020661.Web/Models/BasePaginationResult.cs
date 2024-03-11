using SV20T1020661.DomainModels;

namespace SV20T1020661.Web.Models;

/// <summary>
/// lớp cơ sở cho các lớp biểu diễn dữ liệu là kết quả của thao tác 
/// tìm kiếm phân trang
/// </summary>
public abstract class BasePaginationResult
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string SearchValue { get; set; } = "";
    public int RowCount { get; set; }
    public int PageCount
    {
        get
        {
            if (PageSize == 0)
                return 1;
            int c = RowCount / PageSize;
            if (RowCount % PageSize > 0)
                c += 1;
            return c;
        }
    }
}
/// <summary>
/// Biểu diễn kết quả tìm kiếm và lấy danh sách khách hàng
/// </summary>
public class CustomerSearchResult : BasePaginationResult
{
    public List<Customer> Data { get; set; } = new List<Customer>();
}

/// <summary>
/// Biểu diễn kết quả tìm kiếm và lấy danh sách loại hàng
/// </summary>
public class CategorySearchResult : BasePaginationResult
{
    public List<Category> Data { get; set; } = new List<Category>();
}

/// <summary>
/// Biểu diễn kết quả tìm kiếm và lấy danh sách nhà cung cấp
/// </summary>
public class SupplierSearchResult : BasePaginationResult
{
    public List<Supplier> Data { get; set; } = new List<Supplier>();
}

/// <summary>
/// Biểu diễn kết quả tìm kiếm và lấy danh sách người giao hàng
/// </summary>
public class ShipperSearchResult : BasePaginationResult
{
    public List<Shipper> Data { get; set; } = new List<Shipper>();
}

/// <summary>
/// Biểu diễn kết quả tìm kiếm và lấy danh sách nhân viên
/// </summary>
public class EmployeeSearchResult : BasePaginationResult
{
    public List<Employee> Data { get; set; } = new List<Employee>();
}

