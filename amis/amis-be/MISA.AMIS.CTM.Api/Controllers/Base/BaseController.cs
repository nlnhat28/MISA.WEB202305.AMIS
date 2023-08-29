using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.CTM.Application;

namespace MISA.AMIS.CTM.Api
{
    /// <summary>
    /// Lớp controller nhân viên
    /// </summary>
    /// Created by: nlnhat (18/07/2023)
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BaseController<TEntityDto, TEntityCreateDto, TEntityUpdateDto, TEntity> : ReadOnlyController<TEntityDto>
    {
        #region Fields
        /// <summary>
        /// Service cơ sở
        /// </summary>
        /// Created by: nlnhat (15/07/2023)
        private readonly IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto, TEntity> _service;
        #endregion

        #region Constructors
        /// <summary>
        /// Hàm tạo controller cơ sở
        /// </summary>
        /// <param name="service">Service cơ sở</param>
        /// Created by: nlnhat (18/07/2023)
        public BaseController(IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto, TEntity> service)
             : base(service)
        {
            _service = service;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Tạo mới 1 đối tượng
        /// </summary>
        /// <param name="entityCreateDto">Dto tạo đối tượng</param>
        /// <returns>Id bản ghi mới</returns>
        /// Created by: nlnhat (18/07/2023)
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] TEntityCreateDto entityCreateDto)
        {
            var result = await _service.CreateAsync(entityCreateDto);
            return StatusCode(StatusCodes.Status201Created, result);
        }
        /// <summary>
        /// Sửa 1 đối tượng
        /// </summary>
        /// <param name="id">Id của đối tượng</param>
        /// <param name="entityUpdateDto">Dto cập nhật đối tượng</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: nlnhat (18/07/2023)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] TEntityUpdateDto entityUpdateDto)
        {
            var result = await _service.UpdateAsync(id, entityUpdateDto);
            return StatusCode(StatusCodes.Status200OK, result);
        }
        /// <summary>
        /// Xoá 1 đối tượng theo id
        /// </summary>
        /// <param name="id">Id của đối tượng</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: nlnhat (18/07/2023)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return StatusCode(StatusCodes.Status200OK, result);
        }
        /// <summary>
        /// Xoá nhiều đối tượng từ danh sách id
        /// </summary>
        /// <param name="ids">Danh sách id đối tượng</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: nlnhat (18/07/2023)
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody] List<Guid> ids)
        {
            var result = await _service.DeleteAsync(ids);
            return StatusCode(StatusCodes.Status200OK, result);
        }
        #endregion
    }
}

