namespace SV20T1020661.Web.Models
{
    /// <summary>
    /// đầu vào tìm kiếm dữ liệu để nhận dữ liệu dưới dạng phân trang
    /// </summary>
    public class PaginationSearchInput
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 0;
        public string searchValue { get; set; } = "";
    }
    public class ProductSearchInput : PaginationSearchInput
    {
        public int CategoryId { get; set; }=0;
        public int SupplierId { get; set; } = 0;
        }
}
