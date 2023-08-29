using MISA.AMIS.CTM.Application;

namespace MISA.AMIS.CTM.Api
{
    /// <summary>
    /// Lớp controller phòng ban
    /// </summary>
    /// Created by: nlnhat (13/07/2023)
    public class DepartmentsController : ReadOnlyController<DepartmentDto>
    {
        #region Constructors
        /// <summary>
        /// Hàm tạo controller phòng ban
        /// </summary>
        /// <param name="service">Service phòng ban</param>
        /// Created by: nlnhat (13/07/2023)
        public DepartmentsController(IDepartmentService service) : base(service) { }
        #endregion
    }
}