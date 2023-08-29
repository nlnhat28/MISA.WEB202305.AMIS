using AutoMapper;
using MISA.AMIS.CTM.Application;
using MISA.AMIS.CTM.Domain;

namespace MISA.AMIS.CTM.Mapper
{
    /// <summary>
    /// Tạo mapper bộ lọc
    /// </summary>
    /// Created by: nlnhat (13/07/2023)
    public class FilterProfile : Profile
    {
        #region Contructors
        /// <summary>
        /// Hàm khởi tạo mapper bộ lọc
        /// </summary>
        /// Created by: nlnhat (13/07/2023)
        public FilterProfile()
        {
            CreateMap<FilterDto, FilterModel>();
        }
        #endregion
    }
}