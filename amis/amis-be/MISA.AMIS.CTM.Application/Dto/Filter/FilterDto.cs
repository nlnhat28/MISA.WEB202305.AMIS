namespace MISA.AMIS.CTM.Application
{
    /// <summary>
    /// Dto lọc đối tượng và phân trang
    /// </summary>
    /// Created by: nlnhat (16/07/2023)
    public class FilterDto
    {
        #region Properties
        /// <summary>
        /// Từ khoá tìm kiếm
        /// </summary>
        public string? Key { get; set; }
        /// <summary>
        /// Số trang hiện tại
        /// </summary>
        public int? PageNumber { get; set; }
        /// <summary>
        /// Số bản ghi trên 1 trang
        /// </summary>
        public int? PageSize { get; set; }
        #endregion
    }
}
