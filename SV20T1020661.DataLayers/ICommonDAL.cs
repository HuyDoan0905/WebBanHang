namespace SV20T1020661.DataLayers
{
    /// <summary>
    /// mô tả các phép xử lý dữ liệu"chung chung"(generic)
    /// </summary>
    public interface ICommonDAL<T> where T : class
    {
        /// <summary>
        /// tìm kiếm và lấy danh sách dữ liệu dưới dạng phân trang
        /// </summary>
        /// <param name="page"> trang  cần hiển thị</param>
        /// <param name="pageSize">số dòng trên mỗi trang(bằng 0 nếu k phân trang)</param>
        /// <param name="searchValue">giá trị tìm kiếm(chuỗi rỗng nếu lấy toàn bộ dữ liệu)</param>
        /// <returns></returns>
        IList<T> List(int page=1, int pageSize=0, string searchValue="");
        /// <summary>
        /// đếm số lượng dòng dữ liệu tìm được
        /// </summary>
        /// <param name="searchValue"> giá trị tìm kiếm(chuỗi rỗng nếu lấy toàn bộ dữ liệu)</param>
        /// <returns></returns>
        int Count(string searchValue = "");
        /// <summary>
        /// lấy bảng ghi hoặc dòng dữ liệu dựa trên ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T? Get(int id);
        /// <summary>
        /// bổ sung dữ liệu vào trong CSDL. Hàm trả về ID của dữ liệu đc bổ sung
        /// (trả về giá trị nhỏ hơn hoặc bằng 0 nếu lỗi)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(T data);
        /// <summary>
        /// cập nhật dữ liệu
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(T data);
        /// <summary>
        /// xoá dữ liệu dựa vào id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);
        /// <summary>
        /// kiểm tra 1 dòng ghi dữ liệu có mã id hiện đang được sử dụng bởi các bảng ghi khác không
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool IsUsed(int id);
    }
}   
//thuần tuý/ hoàn toàn là abstract
