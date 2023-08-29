using AutoMapper;
using Microsoft.Extensions.Localization;
using MISA.AMIS.CTM.Domain;

namespace MISA.AMIS.CTM.Application
{
    /// <summary>
    /// Service cơ sở chỉ đọc dữ liệu
    /// </summary>
    /// <typeparam name="TEntityDto">Dto thực thể</typeparam>
    /// <typeparam name="TEntity">Thực thể</typeparam>
    /// Created by: nlnhat (18/07/2023
    public abstract class ReadOnlyService<TEntityDto, TEntity> : IReadOnlyService<TEntityDto>
    {
        #region Fields
        /// <summary>
        /// Repository chỉ đọc dữ liệu từ database
        /// </summary>
        /// Created by: nlnhat (13/07/2023)ks
        protected readonly IReadOnlyRepository<TEntity> _repository;
        /// <summary>
        /// Resource lưu thông báo
        /// </summary>
        /// Created by: nlnhat (13/07/2023)ks
        protected readonly IStringLocalizer<Resource> _resource;
        /// <summary>
        /// Mapper
        /// </summary>
        /// Created by: nlnhat (13/07/2023)ks
        protected readonly IMapper _mapper;
        #endregion

        #region Properties
        /// <summary>
        /// Tên Entity
        /// </summary>
        public virtual string EntityName { get; set; } = typeof(TEntity).Name;
        #endregion

        #region Constructors
        /// <summary>
        /// Hàm tạo service chỉ đọc
        /// </summary>
        /// <param name="repository">Repository chỉ đọc dữ liệu</param>
        /// <param name="resource">Resource lưu thông báo</param>
        /// <param name="mapper">Mapper</param>
        /// Created by: nlnhat (13/07/2023)ks
        public ReadOnlyService(
            IReadOnlyRepository<TEntity> repository,
            IStringLocalizer<Resource> resource,
            IMapper mapper)
        {
            _repository = repository;
            _resource = resource;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy danh sách toàn bộ đối tượng
        /// </summary>
        /// <returns>Danh sách dto nhân viên</returns>
        /// <exception cref="NotFoundException">Không tìm thấy đối tượng</exception>
        /// Created by: nlnhat (13/07/2023)ks
        public async Task<IEnumerable<TEntityDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync() ?? 
                throw new NotFoundException();

            var result = _mapper.Map<IEnumerable<TEntityDto>>(entities);
            return result;
        }
        /// <summary>
        /// Lấy đối tượng theo danh sách id
        /// </summary>
        /// <param name="ids">Danh sách id</param>
        /// <returns>Đối tượng có id trong danh sách</returns>
        /// Created by: nlnhat (18/07/2023)
        public async Task<IEnumerable<TEntityDto>> GetManyAsync(IEnumerable<Guid> ids)
        {
            var entities = await _repository.GetManyAsync(ids) ?? 
                throw new NotFoundException();

            var result = _mapper.Map<IEnumerable<TEntityDto>>(entities);
            return result;
        }
        /// <summary>
        /// Lấy đối tượng theo id
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns>Dto đối tượng được tìm thấy</returns>
        /// <exception cref="NotFoundException">Không tìm thấy đối tượng</exception>
        /// Created by: nlnhat (18/07/2023)
        public async Task<TEntityDto> GetAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id) ??
                throw new NotFoundException(data: new ExceptionData(null, $"{EntityName}Id", id.ToString()));

            var result = _mapper.Map<TEntityDto>(entity);
            return result;
        }
        /// <summary>
        /// Lọc đối tượng theo từ khoá và phân trang
        /// </summary>
        /// <param name="filterDto">Dto lọc đối tượng</param>
        /// <returns>Tổng số bản ghi và danh sách đối tượng được lọc</returns>
        /// <exception cref="ValidateException">Số bản ghi/trang không hợp lệ</exception>
        /// Created by: nlnhat (13/07/2023)
        public async Task<BaseFilterDto<TEntityDto>> FilterAsync(FilterDto filterDto)
        {
            if (filterDto.PageSize == null || filterDto.PageSize <= 0)
                throw new ValidateException(MISAErrorCode.InvalidPageSize, _resource["InvalidPageSize"]);
            if (filterDto.PageNumber == null || filterDto.PageNumber <= 0)
                filterDto.PageNumber = 1;

            var filterModel = _mapper.Map<FilterModel>(filterDto);

            var baseFilterModel = await _repository.FilterAsync(filterModel);

            var result = _mapper.Map<BaseFilterDto<TEntityDto>>(baseFilterModel);

            return result; ;
        }
        #endregion
    }
}
