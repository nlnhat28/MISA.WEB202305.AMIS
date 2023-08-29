using MISA.AMIS.CTM.Domain;
using System.ComponentModel.DataAnnotations;

namespace MISA.AMIS.CTM.Application
{
    /// <summary>
    /// DTO Tạo nhân viên
    /// </summary>
    /// Created by: nlnhat (13/07/2023)
    public class EmployeeCreateDto : BaseDto
    {
        #region Properties
        /// <summary>
        /// Id nhân viên
        /// </summary>
        [Required]
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [Required]
        [StringLength(20)]
        public string EmployeeCode { get; set; }
        /// <summary>
        /// Họ tên đầy đủ nhân viên
        /// </summary>
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        /// <summary>
        /// Khoá ngoại tới phòng ban
        /// </summary>
        [Required]
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Chức vụ
        /// </summary>
        [StringLength(100)]
        public string? Position { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        [PastDate]
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Giới tính 
        /// </summary>
        public Gender? Gender { get; set; }
        /// <summary>
        /// Số căn cước công dân
        /// </summary>
        [StringLength(25)]
        public string? IdentityNumber { get; set; }
        /// <summary>
        /// Ngày cấp căn cước công dân
        /// </summary>
        [PastDate]
        public DateTime? IdentityDate { get; set; }
        /// <summary>
        /// Nơi cấp căn cước công dân
        /// </summary>
        [StringLength(255)]
        public string? IdentityPlace { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        [StringLength(255)]
        public string? Address { get; set; }
        /// <summary>
        /// Số điện thoại di động
        /// </summary>
        [StringLength(50)]
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        [StringLength(50)]
        public string? LandlineNumber { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }
        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        [StringLength(25)]
        public string? BankAccount { get; set; }
        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        [StringLength(255)]
        public string? BankName { get; set; }
        /// <summary>
        /// Là khách hàng hay không
        /// </summary>
        public bool? IsCustomer { get; set; }
        /// <summary>
        /// Là nhà cung cấp hay không
        /// </summary>
        public bool? IsProvider { get; set; }
        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        [StringLength(255)]
        public string? BankBranch { get; set; }
        #endregion
    }
}