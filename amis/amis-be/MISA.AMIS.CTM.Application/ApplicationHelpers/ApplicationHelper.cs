using MISA.AMIS.CTM.Domain;

namespace MISA.AMIS.CTM.Application
{
    /// <summary>
    /// Các phương thức trợ giúp tầng Appication
    /// </summary>
    /// Created by: nlnhat (22/07/2023)
    public class ApplicationHelper : IApplicationHelper
    {
        #region Methods
        /// <summary>
        /// Lấy tên giới tính
        /// </summary>
        /// <param name="gender">Enum giới tính</param>
        /// <returns>Tên giới tính</returns>
        /// Created by: nlnhat (22/07/2023)
        public string GetGenderName(Gender gender)
        {
            switch (gender)
            {
                case Gender.Male: return "Nam";
                case Gender.Female: return "Nữ";
                case Gender.Other: return "Khác";
                default: return "Khác";
            }
        }
        /// <summary>
        /// Lấy danh sách các vai trò khác
        /// </summary>
        /// <param name="isCustomer">Là khách hàng hay không</param>
        /// <param name="isProvider">Là nhà cung cấp hay không</param>
        /// <returns>Danh sách các vai trò khác</returns>
        /// Created by: nlnhat (22/07/2023)
        public List<string> GetOtherRoles(bool? isCustomer, bool? isProvider)
        {
            var otherRoles = new List<string>();
            if (isCustomer == true)
            {
                otherRoles.Add("Khách hàng");
            }
            if (isProvider == true)
            {
                otherRoles.Add("Nhà cung cấp");
            };
            return otherRoles;
        }
        /// <summary>
        /// Lấy tên các vai trò khác
        /// </summary>
        /// <param name="isCustomer">Là khách hàng hay không</param>
        /// <param name="isProvider">Là nhà cung cấp hay không</param>
        /// <returns>Tên các vai trò khác</returns>
        /// Created by: nlnhat (22/07/2023)
        public string GetOtherRolesName(bool? isCustomer, bool? isProvider)
        {
            var otherRoles = GetOtherRoles(isCustomer, isProvider);
            return string.Join(", ", otherRoles);
        }
        /// <summary>
        /// Chuyển thời gian từ utc sang local
        /// </summary>
        /// <param name="dateTime">Thời gian utc</param>
        /// <returns>Thời gian local</returns>
        /// Created by: nlnhat (22/07/2023)
        public DateTime? ConvertDateUtcToLocal(DateTime? dateTime)
        {
            if (dateTime != null)
            {
                var localTimeZone = TimeZoneInfo.Local;
                var localDateTime = TimeZoneInfo.ConvertTimeFromUtc((DateTime)dateTime, localTimeZone);
                localDateTime = DateTime.SpecifyKind(localDateTime, DateTimeKind.Local);

                return localDateTime;
            }
            return null;
        }
        /// <summary>
        /// Chuyển thời gian từ local sang utc
        /// </summary>
        /// <param name="dateTime">Thời gian local</param>
        /// <returns>Thời gian utc</returns>
        /// Created by: nlnhat (22/07/2023)
        public DateTime? ConvertDateLocalToUtc(DateTime? dateTime)
        {
            if (dateTime != null)
            {
                var localDateTimeOffset = new DateTimeOffset((DateTime)dateTime);

                var utcDateTimeOffset = localDateTimeOffset.ToUniversalTime();
                var utcDateTime = utcDateTimeOffset.UtcDateTime;

                return utcDateTime;
            }
            return null;
        }
        #endregion
    }
}
