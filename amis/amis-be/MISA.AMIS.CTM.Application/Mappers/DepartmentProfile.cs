using AutoMapper;
using MISA.AMIS.CTM.Application;
using MISA.AMIS.CTM.Domain;

namespace MISA.AMIS.CTM.Mapper
{
    /// <summary>
    /// Tạo mapper phòng ban
    /// </summary>
    /// Created by: nlnhat (13/07/2023)
    public class DepartmentProfile : Profile
    {
        #region Constructors
        /// <summary>
        /// Hàm tạo mapper phòng ban
        /// </summary>
        /// Created by: nlnhat (13/07/2023)
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentDto>();
            CreateMap<BaseFilterModel<Department>, BaseFilterDto<DepartmentDto>>();
        }
        #endregion
    } 
}