
using MISA.AMIS.CTM.Domain;

namespace MISA.AMIS.CTM.Application
{
    /// <summary>
    /// Giao diện các phương thức trợ giúp tầng application
    /// </summary>
    /// Created by: nlnhat (22/07/2023)
    public interface IApplicationHelper
    {
        /// <summary>
        /// Lấy tên giới tính
        /// </summary>
        /// <param name="gender">Enum giới tính</param>
        /// <returns>Tên giới tính</returns>
        /// Created by: nlnhat (22/07/2023)
        string GetGenderName(Gender gender);
        /// <summary>
        /// Lấy danh sách các vai trò khác
        /// </summary>
        /// <param name="isCustomer">Là khách hàng hay không</param>
        /// <param name="isProvider">Là nhà cung cấp hay không</param>
        /// <returns>Danh sách các vai trò khác</returns>
        /// Created by: nlnhat (22/07/2023)
        List<string> GetOtherRoles(bool? isCustomer, bool? isProvider);
        /// <summary>
        /// Lấy tên các vai trò khác
        /// </summary>
        /// <param name="isCustomer">Là khách hàng hay không</param>
        /// <param name="isProvider">Là nhà cung cấp hay không</param>
        /// <returns>Tên các vai trò khác</returns>
        /// Created by: nlnhat (22/07/2023)
        string GetOtherRolesName(bool? isCustomer, bool? isProvider);
        /// <summary>
        /// Chuyển thời gian từ utc sang local
        /// </summary>
        /// <param name="dateTime">Thời gian utc</param>
        /// <returns>Thời gian local</returns>
        /// Created by: nlnhat (22/07/2023)
        DateTime? ConvertDateUtcToLocal(DateTime? dateTime);
        /// <summary>
        /// Chuyển thời gian từ local sang utc
        /// </summary>
        /// <param name="dateTime">Thời gian local</param>
        /// <returns>Thời gian utc</returns>
        /// Created by: nlnhat (22/07/2023)
        DateTime? ConvertDateLocalToUtc(DateTime? dateTime);
    }
}
