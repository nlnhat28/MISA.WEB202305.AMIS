using MISA.AMIS.CTM.Domain;

namespace MISA.AMIS.CTM.Infrastructure
{
    /// <summary>
    /// Repository phòng ban
    /// </summary>
    /// <typeparam name="Department">Entity phòng ban</typeparam>
    /// Created by: nlnhat (13/07/2023)
    public class DepartmentRepository : ReadOnlyRepository<Department>, IDepartmentRepository
    {
        #region Constructors
        /// <summary>
        /// Hàm tạo repository nhân viên
        /// </summary>
        /// <param name="unitOfWork">Đối tượng unit of work được inject</param>
        /// <param name="helper">Đối tượng helper cho tầng domain</param>
        /// Created by: nlnhat (18/07/2023)
        public DepartmentRepository(IUnitOfWork unitOfWork, IInfrastructureHelper helper) 
             : base(unitOfWork, helper)
        {

        }
        #endregion
    }
}