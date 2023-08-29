using System.Data;
using System.Data.Common;
using Dapper;
using MISA.AMIS.CTM.Domain;

namespace MISA.AMIS.CTM.Infrastructure
{
    /// <summary>
    /// Triển khai giao diện cơ sở repository đọc dữ liệu có mã
    /// </summary>
    /// <typeparam name="TEntity">Thực thể</typeparam>
    /// Created by: nlnhat (18/07/2023)
    public class CodeRepository<TEntity> : ICodeRepository<TEntity>
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
        /// Tên bảng
        /// </summary>
        /// Created by: nlnhat (18/07/2023)
        public virtual string TableName { get; set; } = typeof(TEntity).Name;
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
        /// Created by: nlnhat (18/07/2023)
        public CodeRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _connection = _unitOfWork.Connection;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy đối tượng theo mã
        /// </summary>
        /// <param name="code">Mã đối tượng</param>
        /// <returns>Đối tượng có mã được truy vấn</returns>
        /// Created by: nlnhat (18/07/2023)
        public async Task<TEntity> GetAsync(string code)
        {
            var proc = $"{Procedure}GetByCode";

            var param = new DynamicParameters();
            param.Add($"p_{TableCode}", code);

            var result = await _connection.QueryFirstOrDefaultAsync<TEntity>(
                proc, param, commandType: CommandType.StoredProcedure);
            return result;
        }
        /// <summary>
        /// Lấy mã lớn nhất hiện tại trong bảng
        /// </summary>
        /// <returns>Mã lớn nhất</returns>
        /// Created by: nlnhat (18/07/2023)
        public async Task<int> GetMaxCodeAsync()
        {
            var proc = $"{Procedure}GetMaxCode";

            var result = await _connection.QueryFirstOrDefaultAsync<int>(
                proc, commandType: CommandType.StoredProcedure);
            return result;
        }
        #endregion
    }
}
