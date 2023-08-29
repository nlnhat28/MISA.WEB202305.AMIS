namespace MISA.AMIS.CTM.Domain
{
    /// <summary>
    /// Giao diện repository chỉ đọc dữ liệu database
    /// </summary>
    /// <typeparam name="TEntity">Thực thể</typeparam>
    /// Created by: nlnhat (18/07/2023)
    public interface IReadOnlyRepository<TEntity>
    {
        /// <summary>
        /// Lấy tất cả đối tượng
        /// </summary>
        /// <returns>Danh sách đối tượng được lấy</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();
        /// <summary>
        /// Lấy đối tượng theo danh sách id
        /// </summary>
        /// <param name="ids">Danh sách id của đối tượng</param>
        /// <returns>Các đối tượng có id trong danh sách</returns>
        /// Created by: nlnhat (18/07/2023)
        Task<IEnumerable<TEntity>> GetManyAsync(IEnumerable<Guid> ids);
        /// <summary>
        /// Lấy đối tượng theo id
        /// </summary>
        /// <param name="id">Id của đối tượng</param>
        /// <returns>Đối tượng được tìm thấy</returns>
        /// Created by: nlnhat (18/07/2023)
        Task<TEntity> GetAsync(Guid id);
        /// <summary>
        /// Lấy tổng số bản ghi trong bảng databse
        /// </summary>
        /// <returns>Số lượng bản ghi trong bảng</returns>
        /// Created by: nlnhat (18/07/2023)
        Task<int> GetTotalRecordAsync();
        /// <summary>
        /// Lọc đối tượng theo từ khoá và phân trang
        /// </summary>
        /// <param name="filter">Mô hình lọc</param>
        /// <returns>Mô hình đối tượng được lọc</returns>
        /// Created by: nlnhat (18/07/2023)
        Task<BaseFilterModel<TEntity>> FilterAsync(FilterModel filter);
    }
}
