using Dapper;
using MISA.AMIS.CTM.Domain;
using Newtonsoft.Json;
using System.Data;

namespace MISA.AMIS.CTM.Infrastructure
{
    /// <summary>
    /// Triển khai repository cơ sở
    /// </summary>
    /// <typeparam name="TEntity">Đối tượng</typeparam>
    /// Created by: nlnhat (18/07/2023)
    public abstract class BaseRepository<TEntity> : ReadOnlyRepository<TEntity>, IBaseRepository<TEntity>
    {
        #region Constructors
        /// <summary>
        /// Hàm tạo repository cơ sở
        /// </summary>
        /// <param name="unitOfWork">Đối tượng unit of work được inject</param>
        /// <param name="helper">Đối tượng helper cho tầng domain</param>
        /// Created by: nlnhat (18/07/2023)
        public BaseRepository(IUnitOfWork unitOfWork, IInfrastructureHelper helper) : base(unitOfWork, helper) { }
        #endregion

        #region Methods
        /// <summary>
        /// Tạo mới 1 bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng mới</param>
        /// <returns>Id của bản ghi mới</returns>
        /// Created by: nlnhat (18/07/2023)
        public async Task<Guid> InsertAsync(TEntity entity)
        {
            var proc = $"{Procedure}Insert";

            var param = _helper.GetParamFromEntity(entity);
            param.Add($"p_{TableId}Out", dbType: DbType.Guid, direction: ParameterDirection.Output);
        
            _ = await _connection.ExecuteAsync(
                proc, param, transaction: _transaction, commandType: CommandType.StoredProcedure);

            var result = param.Get<Guid>($"p_{TableId}Out");
            return result;
        }
        /// <summary>
        /// Cập nhật 1 đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng muốn cập nhật</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: nlnhat (18/07/2023)
        public async Task<int> UpdateAsync(TEntity entity)
        {
            var proc = $"{Procedure}Update";

            var param = _helper.GetParamFromEntity(entity);

            var result = await _connection.ExecuteAsync(
                proc, param, transaction: _transaction, commandType: CommandType.StoredProcedure);
            return result;
        }
        /// <summary>
        /// Xoá 1 đối tượng theo id
        /// </summary>
        /// <param name="id">Id đối tượng muốn xoá</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: nlnhat (18/07/2023)
        public async Task<int> DeleteAsync(Guid id)
        {
            var proc = $"{Procedure}Delete";

            var param = new DynamicParameters();
            param.Add($"p_{TableId}", id);

            var result = await _connection.ExecuteAsync(
                proc, param, transaction: _transaction, commandType: CommandType.StoredProcedure);
            return result;
        }
        /// <summary>
        /// Xoá nhiều đối tượng theo id
        /// </summary>
        /// <param name="ids">Danh sách id</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: nlnhat (18/07/2023)
        public async Task<int> DeleteAsync(IEnumerable<Guid> ids)
        {
            var proc = $"{Procedure}DeleteMany";

            var idsJson = JsonConvert.SerializeObject(ids);

            var param = new DynamicParameters();
            param.Add($"p_{TableId}", idsJson);

            var result = await _connection.ExecuteAsync(
                proc, param, transaction: _transaction, commandType: CommandType.StoredProcedure);

            return result;
        }
        #endregion
    }
}