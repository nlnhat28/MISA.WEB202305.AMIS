using System.Data;
using System.Data.Common;
using Dapper;
using MISA.AMIS.CTM.Domain;
using Newtonsoft.Json;

namespace MISA.AMIS.CTM.Infrastructure
{
    /// <summary>
    /// Triển khai giao diện cơ sở repository chỉ đọc dữ liệu từ database
    /// </summary>
    /// <typeparam name="TEntity">Thực thể</typeparam>
    /// Created by: nlnhat (18/07/2023)
    public abstract class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity>
    {
        #region Fields
        /// <summary>
        /// Unit of work
        /// </summary>
        /// Created by: nlnhat (18/07/2023)
        protected readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// Kết nối database
        /// </summary>
        /// Created by: nlnhat (18/07/2023)
        protected readonly DbConnection _connection;
        /// <summary>
        /// Transaction
        /// </summary>
        /// Created by: nlnhat (18/07/2023)
        protected readonly DbTransaction? _transaction;
        /// <summary>
        /// Helper cho tầng domain
        /// </summary>
        /// Created by: nlnhat (20/07/2023)
        protected readonly IInfrastructureHelper _helper;
        /// <summary>
        /// Tên bảng
        /// </summary>
        /// Created by: nlnhat (18/07/2023)
        public virtual string TableName { get; set; } = typeof(TEntity).Name;
        /// <summary>
        /// Tên cột khoá chính 
        /// </summary>
        /// Created by: nlnhat (18/07/2023)
        public virtual string TableId { get; set; } = $"{typeof(TEntity).Name}Id";
        /// <summary>
        /// Tên cột mã
        /// </summary>
        /// Created by: nlnhat (18/07/2023)
        public virtual string TableCode { get; set; } = $"{typeof(TEntity).Name}Code";
        /// <summary>
        /// Tên stored procedure
        /// </summary>
        /// Created by: nlnhat (18/07/2023)
        public virtual string Procedure { get; set; } = $"Proc_{typeof(TEntity).Name}_";
        #endregion

        #region Constructors
        /// <summary>
        /// Hàm tạo repository phòng ban
        /// </summary>
        /// <param name="unitOfWork">Đối tượng unit of work được inject</param>
        /// <param name="helper">Đối tượng helper cho tầng domain</param>
        /// Created by: nlnhat (18/07/2023)
        public ReadOnlyRepository(IUnitOfWork unitOfWork, IInfrastructureHelper helper)
        {
            _unitOfWork = unitOfWork;
            _connection = _unitOfWork.Connection;
            _transaction = _unitOfWork.Transaction;
            _helper = helper;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: nlnhat (18/07/2023)
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var proc = $"{Procedure}GetAll";

            var result = await _connection.QueryAsync<TEntity>(
                proc, transaction: _transaction, commandType: CommandType.StoredProcedure);
            return result;
        }
        /// <summary>
        /// Lấy đối tượng theo danh sách id
        /// </summary>
        /// <param name="ids">Danh sách id</param>
        /// <returns>Đối tượng có id trong danh sách</returns>
        /// Created by: nlnhat (18/07/2023)
        public async Task<IEnumerable<TEntity>> GetManyAsync(IEnumerable<Guid> ids)
        {
            var proc = $"{Procedure}GetManyById";

            var idsJson = JsonConvert.SerializeObject(ids);

            var param = new DynamicParameters();
            param.Add($"p_{TableId}", idsJson);

            var result = await _connection.QueryAsync<TEntity>(
                proc, param, transaction: _transaction, commandType: CommandType.StoredProcedure);

            return result;
        }
        /// <summary>
        /// Lấy đối tượng theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Bản ghi có id được truy vấn</returns>
        /// Created by: nlnhat (18/07/2023)
        public async Task<TEntity> GetAsync(Guid id)
        {
            var proc = $"{Procedure}GetById";

            var param = new DynamicParameters();
            param.Add($"p_{TableId}", id);

            var result = await _connection.QueryFirstOrDefaultAsync<TEntity>(
                proc, param, transaction: _transaction, commandType: CommandType.StoredProcedure);
            return result;
        }
        /// <summary>
        /// Lấy tổng số bản ghi trong bảng
        /// </summary>
        /// <returns>Tổng số bản ghi</returns>
        /// Created by: nlnhat (18/07/2023)
        public async Task<int> GetTotalRecordAsync()
        {
            var proc = $"{Procedure}GetTotalRecord";

            var result = await _connection.QueryFirstOrDefaultAsync<int>(
                proc, transaction: _transaction, commandType: CommandType.StoredProcedure);
            return result;
        }
        /// <summary>
        /// Lọc đối tượng theo từ khoá và phân trang
        /// </summary>
        /// <param name="filter">Mô hình lọc</param>
        /// <returns>Đối tượng thoả mãn điều kiện lọc</returns>
        /// Created by: nlnhat (18/07/2023)
        public async Task<BaseFilterModel<TEntity>> FilterAsync(FilterModel filter)
        {
            var proc = $"{Procedure}Filter";

            var param = _helper.GetParamFromEntity(filter);

            param.Add("p_TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("p_AllRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("p_PageNumberOut", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var data = await _connection.QueryAsync<TEntity>(
                proc, param, transaction: _transaction, commandType: CommandType.StoredProcedure);

            var totalRecord = param.Get<int>("p_TotalRecord");
            var allRecord = param.Get<int>("p_AllRecord");
            var pageNumber = param.Get<int>("p_PageNumberOut");

            var result = new BaseFilterModel<TEntity>()
            {
                TotalRecord = totalRecord,
                AllRecord = allRecord,
                PageNumber = pageNumber,
                Data = data
            };

            return result;
        }
        #endregion
    }
}
