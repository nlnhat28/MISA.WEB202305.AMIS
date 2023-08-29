using AutoMapper;
using Microsoft.Extensions.Localization;
using MISA.AMIS.CTM.Domain;

namespace MISA.AMIS.CTM.Application
{
    /// <summary>
    /// Triển khai service phòng ban từ giao diện
    /// </summary> 
    /// Created by: nlnhat (15/07/2023)
    public class DepartmentService : ReadOnlyService<DepartmentDto, Department>, IDepartmentService
    {
        #region Constructors
        /// <summary>
        /// Hàm tạo service phòng ban
        /// </summary>
        /// <param name="repository">Repository phòng ban</param>
        /// <param name="resource">Resource thông báo</param>
        /// <param name="mapper">Mapper</param>
        /// Created by: nlnhat (13/07/2023)
        #endregion
        public DepartmentService(IDepartmentRepository repository, IStringLocalizer<Resource> resource, IMapper mapper) : base(repository, resource, mapper)
        {
            EntityName = DepartmentConstant.EntityName;
        }
    }
}