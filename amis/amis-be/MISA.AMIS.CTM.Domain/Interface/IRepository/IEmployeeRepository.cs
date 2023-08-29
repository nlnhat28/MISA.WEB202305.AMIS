namespace MISA.AMIS.CTM.Domain
{
    /// <summary>
    /// Giao diện repository nhân viên tương tác với database
    /// </summary>
    /// Created by: nlnhat (13/07/2023)
    public interface IEmployeeRepository : IBaseRepository<Employee>, ICodeRepository<Employee>
    {
        /// <summary>
        /// Lọc nhân viên nâng cao
        /// </summary>
        /// <param name="filterModel">Mô hình lọc</param>
        /// <param name="sort">Chuỗi query sắp xếp</param>
        /// <param name="filter">Chuỗi query filter</param>
        /// <returns>Mô hình nhân viên thoả mãn điều kiện lọc</returns>
        /// Created by: nlnhat (18/07/2023)
        Task<BaseFilterModel<Employee>> FilterAsync(FilterModel filterModel, string? sort, string? filter);
    }
}
