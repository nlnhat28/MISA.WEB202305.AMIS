namespace MISA.AMIS.CTM.Domain
{
    /// <summary>
    /// Giao diện validate nghiệp vụ phòng ban
    /// </summary>
    /// Created by: nlnhat (17/07/2023)
    public interface IDepartmentDomainService
    {
        /// <summary>
        /// Check tồn tại phòng ban hay không
        /// </summary>
        /// <param name="departmentId">Mã phòng ban để check</param>
        /// Created by: nlnhat (17/07/2023)
        Task CheckExistDepartmentAsync(Guid departmentId);
    }
}
