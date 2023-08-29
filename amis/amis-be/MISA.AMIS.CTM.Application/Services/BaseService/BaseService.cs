using AutoMapper;
using Microsoft.Extensions.Localization;
using MISA.AMIS.CTM.Domain;

namespace MISA.AMIS.CTM.Application
{
    /// <summary>
    /// Service cơ sở đầy đủ
    /// </summary>
    /// <typeparam name="TEntityDto">Dto thực thể</typeparam>
    /// <typeparam name="TEntityCreateDto">Dto tạo mới thực thể</typeparam>
    /// <typeparam name="TEntityUpdateDto">Dto cập nhật thực thể</typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// Created by: nlnhat (18/07/2023)
    public abstract class BaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto, TEntity>
                        : ReadOnlyService<TEntityDto, TEntity>, 
                          IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto, TEntity>
    {
        #region Fields
        /// <summary>
        /// Repository tương tác đầy đủ với database
        /// </summary>
        /// Created by: nlnhat (13/07/2023)ks
        protected new readonly IBaseRepository<TEntity> _repository;
        /// <summary>
        /// Unit of work
        /// </summary>
        /// Created by: nlnhat (13/07/2023)ks
        protected readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructors
        /// <summary>
        /// Hàm tạo service đầy đủ
        /// </summary>
        /// <param name="repository">Repository tương tác đầy đủ với database</param>
        /// <param name="resource">Resource lưu thông báo</param>
        /// <param name="mapper">Mapper đối tượng</param>
        /// <param name="unitOfWork">Unit of work</param>
        /// Created by: nlnhat (18/07/2023)
        public BaseService(IBaseRepository<TEntity> repository, IStringLocalizer<Resource> resource, IMapper mapper, IUnitOfWork unitOfWork)
            : base(repository, resource, mapper) 
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Validate thực thể trước khi đưa vào database
        /// </summary>
        /// <param name="entity">Thực thể</param>
        /// Created by: nlnhat (18/07/2023)
        public virtual async Task ValidateAsync(TEntity entity)
        {
            await Task.Yield();
        }
        /// <summary>
        /// Map dto tạo mới sang thực thể
        /// </summary>
        /// <param name="entityCreateDto">Dto tạo mới</param>
        /// <returns>Thực thể</returns>
        /// Created by: nlnhat (18/07/2023)
        public abstract TEntity MapCreateDtoToEntity(TEntityCreateDto entityCreateDto);
        /// <summary>
        /// Map dto cập nhật sang thực thể
        /// </summary>
        /// <param name="entityUpdateDto">Dto cập nhật</param>
        /// <returns>Thực thể</returns>
        /// Created by: nlnhat (18/07/2023)
        public abstract TEntity MapUpdateDtoToEntity(TEntityUpdateDto entityUpdateDto);
        /// <summary>
        /// Tạo mới đối tượng 
        /// </summary>
        /// <param name="entityCreateDto">Dto tạo mới đối tượng</param>
        /// <returns>Id của bản ghi mới</returns>
        /// Created by: nlnhat (18/07/2023)
        public async Task<Guid> CreateAsync(TEntityCreateDto entityCreateDto)
        {
            var entity = MapCreateDtoToEntity(entityCreateDto);

            await ValidateAsync(entity);

            var result = await _repository.InsertAsync(entity);
            return result;
        }
        /// <summary>
        /// Cập nhật đối tượng theo id
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <param name="entityUpdateDto">Dto cập nhật đối tượng</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// <exception cref="NotFoundException">Không tìm thấy bản ghi</exception>
        /// Created by: nlnhat (18/07/2023)
        public async Task<int> UpdateAsync(Guid id, TEntityUpdateDto entityUpdateDto)
        {
            _ = await _repository.GetAsync(id) ??
                throw new NotFoundException(data: new ExceptionData(null, "Id", id.ToString()));

            var entity = MapUpdateDtoToEntity(entityUpdateDto);

            await ValidateAsync(entity);

            var result = await _repository.UpdateAsync(entity);
            return result;
        }
        /// <summary>
        /// Xoá đối tượng theo id
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// <exception cref="NotFoundException">Không tìm thấy bản ghi</exception>
        /// Created by: nlnhat (18/07/2023)
        public async Task<int> DeleteAsync(Guid id)
        {
            var result = await _repository.DeleteAsync(id);
            return result;
        }
        /// <summary>
        /// Xoá nhiều đối tượng
        /// </summary>
        /// <param name="ids">List id muốn xoá</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: nlnhat (18/07/2023)
        public async Task<int> DeleteAsync(IEnumerable<Guid> ids)
        {
            var result = await _repository.DeleteAsync(ids);
            return result;
        }
        /// <summary>
        /// [Test]
        /// Xoá nhiều đối tượng
        /// </summary>
        /// <param name="ids">List id muốn xoá</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: nlnhat (18/07/2023)
        public async Task<int> DeleteManyAsync(IEnumerable<Guid> ids)
        {
            // Danh sách rỗng
            if (!ids.Any())
                throw new ValidateException(
                    MISAErrorCode.Parameterless,
                    _resource["Parameterless"]);

            var entities = await _repository.GetManyAsync(ids);

            // Không tìm được hết
            if (entities.Count() < ids.Count())
                throw new NotFoundException(userMsg: _resource["NotFoundAll"]);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var result = await _repository.DeleteAsync(ids);

                // Không xoá được hết
                if (result < ids.Count())
                    throw new IncompleteException(MISAErrorCode.IncompleteDelete, _resource["IncompleteDelete"]);

                await _unitOfWork.CommitAsync();

                return result;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
        #endregion
    }
}