using AutoMapper;
using MISA.AMIS.CTM.Domain;

namespace MISA.AMIS.CTM.Application
{
    /// <summary>
    /// Tạo mapper nhân viên
    /// </summary>
    /// Created by: nlnhat (13/07/2023)
    public class EmployeeProfile : Profile
    {
        #region Fields
        /// <summary>
        /// Trợ giúp tầng application
        /// </summary>
        /// Created by: nlnhat (13/07/2023)
        private readonly IApplicationHelper _helper = new ApplicationHelper();
        #endregion

        #region Constructors
        /// <summary>
        /// Hàm tạo mapper nhân viên
        /// </summary>
        /// Created by: nlnhat (13/07/2023)
        public EmployeeProfile()
        {
            // Map thực thể với dto
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.GenderName, opt =>
                {
                    opt.MapFrom(src =>
                        _helper.GetGenderName(src.Gender));
                })
                .ForMember(dest => dest.OtherRoles, opt =>
                {
                    opt.MapFrom(src =>
                     _helper.GetOtherRoles(src.IsCustomer, src.IsProvider));
                })
                .ForMember(dest => dest.CreatedDate, opt =>
                {
                    opt.MapFrom(src =>
                     _helper.ConvertDateUtcToLocal(src.CreatedDate));
                })
                .ForMember(dest => dest.ModifiedDate, opt =>
                {
                    opt.MapFrom(src =>
                     _helper.ConvertDateUtcToLocal(src.ModifiedDate));
                });

            CreateMap<Employee, EmployeeExcelDto>()
                .ForMember(dest => dest.GenderName, opt =>
                {
                    opt.MapFrom(src =>
                        _helper.GetGenderName(src.Gender));
                })
                .ForMember(dest => dest.OtherRoles, opt =>
                {
                    opt.MapFrom(src =>
                     _helper.GetOtherRolesName(src.IsCustomer, src.IsProvider));
                });

            CreateMap<EmployeeCreateDto, Employee>()
                .ForMember(dest => dest.CreatedDate, opt =>
                {
                    opt.MapFrom(src =>
                     _helper.ConvertDateLocalToUtc(src.CreatedDate));
                })
                .ForMember(dest => dest.ModifiedDate, opt =>
                {
                    opt.MapFrom(src =>
                     _helper.ConvertDateLocalToUtc(src.ModifiedDate));
                });
            CreateMap<EmployeeUpdateDto, Employee>()
                .ForMember(dest => dest.CreatedDate, opt =>
                {
                    opt.MapFrom(src =>
                     _helper.ConvertDateLocalToUtc(src.CreatedDate));
                })
                .ForMember(dest => dest.ModifiedDate, opt =>
                {
                    opt.MapFrom(src =>
                     _helper.ConvertDateLocalToUtc(src.ModifiedDate));
                });
            CreateMap<BaseFilterModel<Employee>, BaseFilterDto<EmployeeDto>>();
        }
        #endregion
    }
}