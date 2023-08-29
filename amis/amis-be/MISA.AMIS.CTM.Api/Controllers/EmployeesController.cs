using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.CTM.Application;
using MISA.AMIS.CTM.Domain;

namespace MISA.AMIS.CTM.Api
{
    /// <summary>
    /// Lớp controller nhân viên
    /// </summary>
    /// Created by: nlnhat (13/07/2023)
    public class EmployeesController : BaseController<EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto, Employee>
    {
        #region Fields
        /// <summary>
        /// Service nhân viên
        /// </summary>
        /// Created by: nlnhat (18/07/2023)
        private readonly IEmployeeService _service;
        #endregion

        #region Constructors
        /// <summary>
        /// Hàm tạo controller nhân viên
        /// </summary>
        /// <param name="service">Service nhân viên</param>
        /// Created by: nlnhat (13/07/2023)
        public EmployeesController(IEmployeeService service) : base(service) 
        {
            _service = service;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// Created by: nlnhat (18/07/2023)
        [HttpGet("NewCode")]
        public async Task<IActionResult> GetNewCodeAsync()
        {
            var newCode = await _service.GetNewCodeAsync();
            return StatusCode(StatusCodes.Status200OK, newCode);
        }
        /// <summary>
        /// Lọc nhân viên nâng cao (phân trang, sắp xếp, lọc)
        /// </summary>
        /// <param name="filterDto">Dto lọc nhân viên</param>
        /// <param name="Sort">Chuỗi query sắp xếp</param>
        /// <param name="Filter">Chuỗi query filter</param>
        /// <returns>Dto nhân viên được lọc</returns>
        /// Created by: nlnhat (20/07/2023)
        [HttpGet("FilterAdvance")]
        public async Task<IActionResult> FilterAsync([FromQuery] FilterDto filterDto, [FromQuery] string? sort, [FromQuery] string? filter)
        {
            var result = await _service.FilterAsync(filterDto, sort, filter);
            return StatusCode(StatusCodes.Status200OK, result);
        }
        /// <summary>
        /// Xuất dữ liệu ra file excel
        /// </summary>
        /// <returns>Response</returns>
        /// Created by: nlnhat (2/07/2023)
        [HttpGet("Excel")]
        public async Task<IActionResult> ExportAsync()
        {
            var data = await _service.ExportToExcelAsync();
            string fileName = "employees-export.xlsx";
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        #endregion
    }
}