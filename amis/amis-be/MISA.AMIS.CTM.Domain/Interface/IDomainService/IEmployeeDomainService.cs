namespace MISA.AMIS.CTM.Domain
{
    /// <summary>
    /// Giao diện validate nghiệp vụ nhân viên
    /// </summary>
    /// Created by: nlnhat (17/07/2023)
    public interface IEmployeeDomainService
    {
        /// <summary>
        /// Check trùng mã nhân viên
        /// </summary>
        /// <param name="employee">Entity nhân viên để check</param>
        /// Created by: nlnhat (17/07/2023)
        Task CheckDuplicatedCodeAsync(Employee employee);
        /// <summary>
        /// Check mã nhân viên có nằm trong khoảng cho phép hay không
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên để check</param>
        /// Created by: nlnhat (17/07/2023)
        Task CheckRangeCodeAsync(string employeeCode);
        /// <summary>
        /// Check tồn tại phòng ban hay không
        /// </summary>
        /// <param name="departmentId">Id phòng ban để check</param>
        /// Created by: nlnhat (17/07/2023)
        Task CheckExistDepartmentAsync(Guid departmentId);
    }
}
