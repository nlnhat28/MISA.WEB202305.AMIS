using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.CTM.Domain
{
    /// <summary>
    /// Lớp thực thể phòng ban
    /// </summary>
    /// Created by: nlnhat (12/07/2023)
    public class Department : BaseEntity
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