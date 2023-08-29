using MISA.AMIS.CTM.Domain;

namespace MISA.AMIS.CTM.Application
{
    /// <summary>
    /// Giao diện service nhân viên
    /// </summary>
    /// Created by: nlnhat (13/07/2023)
    public interface IEmployeeService : IBaseService<EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto, Employee>, ICodeService<EmployeeDto, Employee>
    {
        /// <summary>
        /// Lọc nhân viên nâng cao
        /// </summary>
        /// <param name="filterDto">Dto lọc nhân viên</param>
        /// <param name="sort">Chuỗi query sắp xếp</param>
        /// <param name="filter">Chuỗi query filter</param>
        /// <returns>Nhân viên được lọc</returns>
        /// Created by: nlnhat (20/07/2023)
        Task<BaseFilterDto<EmployeeDto>> FilterAsync(FilterDto filterDto, string? sort, string? filter);
        /// <summary>
        /// Xuất dữ liệu ra file excel
        /// </summary>
        /// <returns>Response</returns>
        /// Created by: nlnhat (2/07/2023)
        Task<MemoryStream> ExportToExcelAsync();
    }
}