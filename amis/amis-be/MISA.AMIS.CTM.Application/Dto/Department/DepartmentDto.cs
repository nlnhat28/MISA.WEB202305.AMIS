using System.ComponentModel.DataAnnotations;

namespace MISA.AMIS.CTM.Application
{
    /// <summary>
    /// Lớp dto phòng ban
    /// </summary>
    /// Created by: nlnhat (12/07/2023)
    public class DepartmentDto : BaseDto
    {
        #region Properties
        /// <summary>
        /// Khoá chính
        /// </summary>
        [Required]
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Mã phòng ban
        /// </summary>
        [Required]
        [StringLength(20)]
        public string DepartmentCode { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        [Required]
        [StringLength(255)]
        public string DepartmentName { get; set; }
        /// <summary>
        /// Mô tả phòng ban
        /// </summary>
        [StringLength(255)]
        public string? Description { get; set; }
        #endregion
    }
}