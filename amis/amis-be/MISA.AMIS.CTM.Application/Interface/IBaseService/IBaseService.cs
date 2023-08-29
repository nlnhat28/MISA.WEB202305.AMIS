namespace MISA.AMIS.CTM.Application
{
    /// <summary>
    /// Giao diện service cơ sở
    /// </summary>
    /// <typeparam name="TEntityDto">Dto đối tượng</typeparam>
    /// <typeparam name="TEntityCreateDto">Dto tạo mới đối tượng</typeparam>
    /// <typeparam name="TEntityUpdateDto">Dto cập nhật đối tượng</typeparam>
    /// Created by: nlnhat (18/07/2023)
    public interface IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto, TEntity> 
        : IReadOnlyService<TEntityDto>
    {
        /// <summary>
        /// Tạo mới đối tượng
        /// </summary>
        /// <param name="entityCreateDto">Dto tạo mới đối tượng</param>
        /// <returns>Id của bản ghi mới</returns>
        /// Created by: nlnhat (18/07/2023)
        Task<Guid> CreateAsync(TEntityCreateDto entityCreateDto);
        /// <summary>
        /// Sửa đối tượng
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <param name="entityUpdateDto">Dto cập nhật đối tượng</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: nlnhat (18/07/2023)
        Task<int> UpdateAsync(Guid id, TEntityUpdateDto entityUpdateDto);
        /// <summary>
        /// Xoá đối tượng theo id
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: nlnhat (18/07/2023)
        Task<int> DeleteAsync(Guid id);
        /// <summary>
        /// Xoá nhiều đối tượng
        /// </summary>
        /// <param name="ids">List id đối tượng muốn xoá</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: nlnhat (17/07/2023)
        Task<int> DeleteAsync(IEnumerable<Guid> ids);
        /// <summary>
        /// [Test]
        /// Xoá nhiều đối tượng
        /// </summary>
        /// <param name="ids">List id đối tượng muốn xoá</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: nlnhat (17/07/2023)
        Task<int> DeleteManyAsync(IEnumerable<Guid> ids);
        /// <summary>
        /// Validate đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng để validate</param>
        /// Created by: nlnhat (18/07/2023)
        Task ValidateAsync(TEntity entity);
        /// <summary>
        /// Map dto tạo mới sang thực thể
        /// </summary>
        /// <param name="entityCreateDto">Dto tạo mới</param>
        /// <returns>Thực thể</returns>
        /// Created by: nlnhat (18/07/2023)
        TEntity MapCreateDtoToEntity(TEntityCreateDto entityCreateDto);
        /// <summary>
        /// Map dto cập nhật sang thực thể
        /// </summary>
        /// <param name="entityUpdateDto">Dto cập nhật</param>
        /// <returns>Thực thể</returns>
        /// Created by: nlnhat (18/07/2023)
        TEntity MapUpdateDtoToEntity(TEntityUpdateDto entityUpdateDto);
    }
}
