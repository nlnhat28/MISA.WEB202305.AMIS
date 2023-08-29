
using MISA.AMIS.CTM.Domain;
using System.ComponentModel;

namespace MISA.AMIS.CTM.Application
{
    /// <summary>
    /// Dto nhân viên in ra excel
    /// </summary>
    /// Created by: nlnhat (23/07/2023)
    [DisplayName("Danh sách nhân viên")]
    public class EmployeeExcelDto
    {
        #region Properties
        /// <summary>
        /// Số thứ tự
        /// </summary>
        [ExcelDisplay("A", "Số thứ tự", HorizontalAlign.Left)]
        public int? Index { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [ExcelDisplay("B", "Mã nhân viên")]
        public string EmployeeCode { get; set; }
        /// <summary>
        /// Họ tên đầy đủ nhân viên
        /// </summary>
        [ExcelDisplay("C", "Họ và tên")]
        public string FullName { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        [ExcelDisplay("D", "Phòng ban")]
        public string? DepartmentName { get; set; }
        /// <summary>
        /// Chức vụ
        /// </summary>
        [ExcelDisplay("E", "Chức vụ")]
        public string? Position { get; set; }
        /// <summary>
        /// Vai trò khác
        /// </summary>
        [ExcelDisplay("F", "Vai trò khác")]
        public string? OtherRoles { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        [ExcelDisplay("G", "Ngày sinh", HorizontalAlign.Center, NumberFormat.Date)]
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Tên giới tính 
        /// </summary>
        [ExcelDisplay("H", "Giới tính")]
        public string? GenderName { get; set; }
        /// <summary>
        /// Số căn cước công dân
        /// </summary>
        [ExcelDisplay("I", "Số chứng minh nhân dân")]
        public string? IdentityNumber { get; set; }
        /// <summary>
        /// Ngày cấp căn cước công dân
        /// </summary>
        [ExcelDisplay("J", "Ngày cấp", HorizontalAlign.Center, NumberFormat.Date)]
        public DateTime? IdentityDate { get; set; }
        /// <summary>
        /// Nơi cấp căn cước công dân
        /// </summary>
        [ExcelDisplay("K", "Nơi cấp")]
        public string? IdentityPlace { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        [ExcelDisplay("L", "Địa chỉ")]
        public string? Address { get; set; }
        /// <summary>
        /// Số điện thoại di động
        /// </summary>
        [ExcelDisplay("M", "Số di động")]
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        [ExcelDisplay("N", "Số cố định")]
        public string? LandlineNumber { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        [ExcelDisplay("O", "Email")]
        public string? Email { get; set; }
        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        [ExcelDisplay("P", "Số tài khoản ngân hàng")]
        public string? BankAccount { get; set; }
        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        [ExcelDisplay("Q", "Tên ngân hàng")]
        public string? BankName { get; set; }
        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        [ExcelDisplay("R", "Chi nhánh ngân hàng")]
        public string? BankBranch { get; set; }
        /// <summary>
        /// Cột bắt đầu
        /// </summary>
        public static string FirstColumn { get; set; } = "A";
        /// <summary>
        /// Cột kết thúc
        /// </summary>
        public static string LastColumn { get; set; } = "R";
        #endregion
    }
}
