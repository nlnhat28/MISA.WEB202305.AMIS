using System.ComponentModel.DataAnnotations;

namespace MISA.AMIS.CTM.Domain
{
    /// <summary>
    /// Lớp thực thể cơ sở
    /// </summary>
    /// Created by: nlnhat (11/07/2023)
    public class BaseEntity
    {
        #region Properties
        /// <summary>
        /// Số thứ tự
        /// </summary>
        public int? Index { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        [StringLength(255)]
        public string? CreatedBy { get; set; }
        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Người cập nhật
        /// </summary>  
        [StringLength(255)]
        public string? ModifiedBy { get; set; }
        #endregion
    }
}