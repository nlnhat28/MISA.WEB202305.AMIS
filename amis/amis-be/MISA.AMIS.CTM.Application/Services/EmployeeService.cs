using AutoMapper;
using MISA.AMIS.CTM.Domain;
using OfficeOpenXml;
using System.Reflection;
using System.ComponentModel;
using System.Text.RegularExpressions;
using OfficeOpenXml.Style;
using System.Drawing;
using Microsoft.Extensions.Localization;

namespace MISA.AMIS.CTM.Application
{
    /// <summary>
    /// Triển khai service nhân viên
    /// </summary> 
    public class EmployeeService
               : BaseService<EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto, Employee>,
                 IEmployeeService
    {

        #region Fields
        /// <summary>
        /// Repository nhân viên
        /// </summary>
        /// Created by: nlnhat (18/07/2023)
        private new readonly IEmployeeRepository _repository;
        /// <summary>
        /// Domain service nhân viên, validate nghiệp vụ
        /// </summary>
        /// Created by: nlnhat (18/07/2023)
        private readonly IEmployeeDomainService _domainService;
        #endregion

        #region Properties
        /// <summary>
        /// Tiền tố mã nhân viên
        /// </summary>
        /// Created by: nlnhat (18/07/2023)
        public string PrefixCode { get; set; }
        /// <summary>
        /// Độ dài tối thiểu mã nhân viên
        /// </summary>
        /// Created by: nlnhat (18/07/2023)
        public int MinLengthCode { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Hàm tạo service nhân viên
        /// </summary>
        /// <param name="repository">Repository nhân viên</param>
        /// <param name="domainService">Domain service nhân viên</param>
        /// <param name="resource">Resource thông báo</param>
        /// <param name="mapper">Mapper map đối tượng</param>
        /// <param name="unitOfWork">Unit of work</param>
        /// Created by: nlnhat (18/07/2023)
        public EmployeeService(IEmployeeRepository repository, IEmployeeDomainService domainService,
                               IStringLocalizer<Resource> resource, IMapper mapper, IUnitOfWork unitOfWork)
                             : base(repository, resource, mapper, unitOfWork)
        {
            _repository = repository;
            _domainService = domainService;
            PrefixCode = EmployeeConstant.PrefixCode;
            MinLengthCode = EmployeeConstant.MinLengthCode;
            EntityName = EmployeeConstant.EntityName;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy nhân viên theo mã
        /// </summary>
        /// <param name="code">Mã nhân viên</param>
        /// <returns>Dto nhân viên được tìm thấy</returns>
        /// <exception cref="NotFoundException">Không tìm thấy nhân viên</exception>
        /// Created by: nlnhat (18/07/2023)
        public async Task<EmployeeDto> GetAsync(string code)
        {
            var employee = await _repository.GetAsync(code) ??
                throw new NotFoundException(data: new ExceptionData(null, $"{EntityName}Code", code));

            var result = _mapper.Map<EmployeeDto>(employee);
            return result;
        }
        /// <summary>
        /// Lấy mã mới cho nhân viên
        /// </summary>
        /// <returns>Mã mới có dạng Prefix-Số</returns>
        /// Created by: nlnhat (17/07/2023)
        public async Task<string> GetNewCodeAsync()
        {
            var maxCode = await _repository.GetMaxCodeAsync();

            // Định dạng mã mới từ tiền tố mã, mã lớn nhất và độ dài mã tối thiểu
            // Vd: tiền tố (NV-), mã lớn nhất (27), độ dài tối thiểu (4) => mã mới NV-0028 
            var newCode = (++maxCode).ToString($"D{MinLengthCode}");
            var result = $"{PrefixCode}{newCode}";
            return result;
        }
        /// <summary>
        /// Validate tổng thể (input + nghiệp vụ) nhân viên
        /// </summary>
        /// <param name="employee">Entity nhân viên</param>
        /// Created by: nlnhat (17/07/2023)
        public override async Task ValidateAsync(Employee employee)
        {
            var employeeCode = employee.EmployeeCode;
            var departmentId = employee.DepartmentId;

            // Check code đúng format NV-Số không
            string pattern = string.Format(@"^{0}\d+$", EmployeeConstant.PrefixCode);
            bool isValid = Regex.IsMatch(employeeCode, pattern);

            if (!isValid)
                throw new ValidateException(
                    MISAErrorCode.EmployeeCodeWrongFormat,
                    $"{_resource["EmployeeCodeFormat"]} {EmployeeConstant.PrefixCode}{_resource["Number"]}",
                    new ExceptionData(ExceptionKey.FormItem, "EmployeeCode", employeeCode));

            // Check trùng mã nhân viên
            await _domainService.CheckDuplicatedCodeAsync(employee);

            // Check mã trong khoảng cho phép không
            await _domainService.CheckRangeCodeAsync(employeeCode);

            // Check tồn tại phòng ban hay không
            await _domainService.CheckExistDepartmentAsync(departmentId);
        }
        /// <summary>
        /// Map dto tạo mới sang thực thể nhân viên
        /// </summary>
        /// <param name="entityCreateDto">Dto tạo mới nhân viên</param>
        /// <returns>Thực thể nhân viên</returns>
        /// Created by: nlnhat (18/07/2023)
        public override Employee MapCreateDtoToEntity(EmployeeCreateDto employeeCreateDto)
        {
            employeeCreateDto.EmployeeId = Guid.NewGuid();
            if (employeeCreateDto.CreatedDate == null)
                employeeCreateDto.CreatedDate = DateTime.UtcNow;

            var employee = _mapper.Map<Employee>(employeeCreateDto);
            return employee;
        }
        /// <summary>
        /// Map dto cập nhật sang thực thể nhân viên
        /// </summary>
        /// <param name="employeeUpdateDto">Dto cập nhật nhân viên</param>
        /// <returns>Thực thể nhân viên</returns>
        /// Created by: nlnhat (18/07/2023)
        public override Employee MapUpdateDtoToEntity(EmployeeUpdateDto employeeUpdateDto)
        {
            employeeUpdateDto.ModifiedDate = DateTime.UtcNow;

            var employee = _mapper.Map<Employee>(employeeUpdateDto);
            return employee;
        }
        /// <summary>
        /// Lọc đối tượng nâng cao
        /// </summary>
        /// <param name="filterDto">Dto lọc đối tượng</param>
        /// <param name="sort">Chuỗi query sắp xếp</param>
        /// <param name="filter">Chuỗi query filter</param>
        /// <returns>Tổng số bản ghi và danh sách đối tượng được lọc</returns>
        /// <exception cref="ValidateException">Số bản ghi/trang không hợp lệ</exception>
        /// Created by: nlnhat (20/07/2023)
        public async Task<BaseFilterDto<EmployeeDto>> FilterAsync(FilterDto filterDto, string? sort, string? filter)
        {
            if (filterDto.PageSize == null || filterDto.PageSize <= 0)
                throw new ValidateException(MISAErrorCode.InvalidPageSize, _resource["InvalidPageSize"]);
            if (filterDto.PageNumber == null || filterDto.PageNumber <= 0)
                filterDto.PageNumber = 1;

            var filterModel = _mapper.Map<FilterModel>(filterDto);

            var baseFilterModel = await _repository.FilterAsync(filterModel, sort, filter);

            var result = _mapper.Map<BaseFilterDto<EmployeeDto>>(baseFilterModel);

            return result;
        }
        /// <summary>
        /// Xuất dữ liệu tất cả nhân viên ra file excel
        /// </summary>
        /// <returns>Memory stream chứa nội dung excel</returns>
        /// Created by: nlnhat (22/07/2023)
        public async Task<MemoryStream> ExportToExcelAsync()
        {
            var employees = await _repository.GetAllAsync() ??
                throw new NotFoundException();

            var employeesExcel = _mapper.Map<IEnumerable<EmployeeExcelDto>>(employees);
            var lengthRow = employeesExcel.Count();
            var startRow = 3;
            var endRow = startRow + lengthRow;
            var firstColumn = EmployeeExcelDto.FirstColumn;
            var lastColumn = EmployeeExcelDto.LastColumn;

            try
            {
                // Khởi tạo excel package
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using var package = new ExcelPackage();

                var type = typeof(EmployeeExcelDto);

                var sheetName = type.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName;
                var sheet = package.Workbook.Worksheets.Add(sheetName);
                sheet.Cells[$"{firstColumn}{startRow + 1}"].LoadFromCollection(employeesExcel, false);

                // Chỉnh lại style các cột
                var properties = type.GetProperties();
                foreach (var property in properties)
                {
                    // Lấy thông tin cột
                    var excelDisplayAttribute = property.GetCustomAttribute<ExcelDisplayAttribute>();
                    if (excelDisplayAttribute == null)
                        continue;
                    var columnLetter = excelDisplayAttribute.ColumnLetter;
                    var columnName = excelDisplayAttribute.ColumnName;
                    var horizontalAlign = excelDisplayAttribute?.HorizontalAlignment ?? ExcelHorizontalAlignment.Left;
                    var numberFormat = excelDisplayAttribute?.NumberFormat;

                    // Chỉnh phần header
                    var headerCell = sheet.Cells[$"{columnLetter}{startRow}"];
                    headerCell.Value = columnName;
                    headerCell.Style.Font.Bold = true;
                    headerCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    headerCell.Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                    // Format dữ liệu
                    if (numberFormat != null)
                    {
                        sheet.Cells[$"{columnLetter}:{columnLetter}"].Style.Numberformat.Format = numberFormat;
                    }

                    // Chỉnh cho 1 cột
                    var column = sheet.Cells[$"{columnLetter}:{columnLetter}"];
                    column.AutoFitColumns();
                    column.Style.HorizontalAlignment = horizontalAlign;
                    column.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    // Thêm border
                    sheet.Cells[$"{columnLetter}{startRow}:{columnLetter}{endRow}"].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Gray);
                }

                // Chỉnh chiều cao dòng header
                var header = sheet.Cells[$"{firstColumn}{startRow}:{lastColumn}{startRow}"];
                header.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Gray);
                sheet.Rows[startRow].Height = 22;

                // Chỉnh tiêu đề
                var titleRange = sheet.Cells[$"{firstColumn}1:{lastColumn}1"];
                titleRange.Merge = true;
                titleRange.Value = sheetName;
                titleRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                titleRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                titleRange.Style.Font.Bold = true;
                titleRange.Style.Font.Size = 22;

                var memoryStream = new MemoryStream();
                package.SaveAs(memoryStream);
                memoryStream.Position = 0;

                return memoryStream;
            }
            catch (Exception exception)
            {
                throw new IncompleteException(MISAErrorCode.EmployeeExportError, _resource["EmployeeExportError"], exception.Message);
            }
        }
        #endregion
    }
}