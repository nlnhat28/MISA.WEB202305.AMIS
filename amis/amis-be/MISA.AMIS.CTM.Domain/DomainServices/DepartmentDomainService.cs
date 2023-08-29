using Microsoft.Extensions.Localization;

namespace MISA.AMIS.CTM.Domain
{
    /// <summary>
    /// Domain service check validate nghiệp vụ phòng ban
    /// </summary>
    /// Created by: nlnhat (17/07/2023)
    public class DepartmentDomainService : IDepartmentDomainService
    {
        #region Fields
        /// <summary>
        /// Repository phòng ban
        /// </summary>
        private readonly IDepartmentRepository _repository;
        /// <summary>
        /// Resource lưu trữ thông báo
        /// </summary>
        private readonly IStringLocalizer<Resource> _resource;
        #endregion

        #region Constructors
        public DepartmentDomainService(
            IDepartmentRepository departmentRepository,
            IStringLocalizer<Resource> resource)
        {
            _repository = departmentRepository;
            _resource = resource;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Check tồn tại phòng ban hay không
        /// </summary>
        /// <param name="departmentId">Id phòng ban để check</param>
        /// Created by: nlnhat (17/07/2023)
        public async Task CheckExistDepartmentAsync(Guid departmentId)
        {
            _ = await _repository.GetAsync(departmentId) ??
                throw new NotFoundException(
                    MISAErrorCode.DepartmentNotFound,
                    _resource["DepartmentNotFound"],
                    new ExceptionData(ExceptionKey.FormItem, "Deparment", departmentId.ToString()));
        }
        #endregion
    }
}
