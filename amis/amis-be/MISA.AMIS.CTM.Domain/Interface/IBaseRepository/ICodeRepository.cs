namespace MISA.AMIS.CTM.Domain
{
    /// <summary>
    /// Giao diện repository đọc dữ liệu có mã
    /// </summary>
    /// <typeparam name="TEntity">Thực thể</typeparam>
    /// Created by: nlnhat (18/07/2023)
    public interface ICodeRepository<TEntity>
    {
        /// <summary>
        /// Lấy đối tượng theo code
        /// </summary>
        /// <param name="code">Code của đối tượng</param>
        /// <returns>Đối tượng được tìm thấy</returns>
        /// Created by: nlnhat (18/07/2023)
        Task<TEntity> GetAsync(string code);
        /// <summary>
        /// Lấy mã lớn nhất trong bảng
        /// </summary>
        /// <returns>Mã lớn nhất</returns>
        /// Created by: nlnhat (18/07/2023)
        Task<int> GetMaxCodeAsync();
    }
}
