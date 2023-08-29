namespace MISA.AMIS.CTM.Application
{
    /// <summary>
    /// Giao diện service đọc dữ liệu có mã
    /// </summary>
    /// <typeparam name="TEntity">Thực thể</typeparam>
    /// <typeparam name="TEntityDto">Dto thực thể</typeparam>
    /// Created by: nlnhat (18/07/2023)
    public interface ICodeService<TEntityDto, TEntity>
    {
        /// <summary>
        /// Lấy đối tượng theo mã
        /// </summary>
        /// <param name="code">Mã đối tượng</param>
        /// <returns>Dto đối tượng</returns>
        /// Created by: nlnhat (17/07/2023)
        Task<TEntityDto> GetAsync(string code);
        /// <summary>
        /// Lấy mã đối tượng mới
        /// </summary>
        /// <returns>Mã đối tượng mới</returns>
        /// Created by: nlnhat (17/07/2023)
        Task<string> GetNewCodeAsync();
    }
}
