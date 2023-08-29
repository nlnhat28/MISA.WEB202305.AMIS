using Dapper;
using MISA.AMIS.CTM.Domain;
using System.Data;
using static Dapper.SqlMapper;

namespace MISA.AMIS.CTM.Infrastructure
{
    /// <summary>
    /// Repository nhân viên
    /// </summary>
    /// <typeparam name="Employee">Entity nhân viên</typeparam>
    /// Created by: nlnhat (13/07/2023)
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region Fields
        /// <summary>
        /// Helper cho tầng domain
        /// </summary>
        /// Created by: nlnhat (20/07/2023)
        private new readonly IInfrastructureHelper _helper;
        #endregion

        #region Constructors
        /// <summary>
        /// Hàm tạo repository nhân viên
        /// </summary>
        /// <param name="unitOfWork">Đối tượng unit of work được inject</param>
        /// <param name="helper">Đối tượng helper cho tầng domain</param>
        /// Created by: nlnhat (18/07/2023)
        public EmployeeRepository(IUnitOfWork unitOfWork, IInfrastructureHelper helper) : base(unitOfWork, helper)
        {
            _helper = helper;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy nhân viên theo mã
        /// </summary>
        /// <param name="code">Mã nhân viên</param>
        /// <returns>Nhân viên có mã tương ứng</returns>
        public async Task<Employee> GetAsync(string code)
        {
            var proc = $"{Procedure}GetByCode";

            var param = new DynamicParameters();
            param.Add($"p_{TableCode}", code);

            var result = await _connection.QueryFirstOrDefaultAsync<Employee>(
                proc, param, commandType: CommandType.StoredProcedure);
            return result;
        }
        /// <summary>
        /// Lấy mã nhân viên lớn nhất trong database
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        public async Task<int> GetMaxCodeAsync()
        {
            var proc = $"{Procedure}GetMaxCode";

            var result = await _connection.QueryFirstOrDefaultAsync<int>(
                proc, commandType: CommandType.StoredProcedure);
            return result;
        }
        /// <summary>
        /// Lọc nhân viên nâng cao
        /// </summary>
        /// <param name="filterModel">Mô hình lọc</param>
        /// <param name="sort">Chuỗi query sắp xếp</param>
        /// <param name="filter">Chuỗi query filter</param>
        /// <returns>Mô hình nhân viên thoả mãn điều kiện lọc</returns>
        /// Created by: nlnhat (18/07/2023)
        public async Task<BaseFilterModel<Employee>> FilterAsync(FilterModel filterModel, string? sort, string? filter)
        {
            var proc = $"{Procedure}FilterAdvance";

            var param = _helper.GetParamFromEntity(filterModel);

            var querySort = GenerateQuerySort(sort);
            param.Add("p_QuerySort", querySort);

            var queryFilter = GenerateQueryFilter(filter);
            param.Add("p_QueryFilter", queryFilter);

            param.Add("p_TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("p_AllRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            param.Add("p_PageNumberOut", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var data = await _connection.QueryAsync<Employee>(
                proc, param, transaction: _transaction, commandType: CommandType.StoredProcedure);

            var totalRecord = param.Get<int>("p_TotalRecord");
            var allRecord = param.Get<int>("p_AllRecord");
            var pageNumber = param.Get<int>("p_PageNumberOut");

            var result = new BaseFilterModel<Employee>()
            {
                TotalRecord = totalRecord,
                AllRecord = allRecord,
                PageNumber = pageNumber,
                Data = data
            };

            return result;
        }
        /// <summary>
        /// Tạo các câu truy vấn Orderby 
        /// </summary>
        /// <param name="sort">Chuỗi query sắp xếp</param>
        /// <example>sort=+EmloyeeCode,-DateOfBirth => ORDER BY EmployeeCode ASC, DateOfBirth DESC</example>
        /// <returns>Các câu truy vấn Orderby</returns>
        private string GenerateQuerySort(string? sort)
        {
            if (string.IsNullOrWhiteSpace(sort))
                return string.Empty;

            var queryItems = new List<string>();
            var sortItems = sort.Split(',').ToList();

            foreach (var item in sortItems)
            {
                if (item.Length < 2)
                    continue;

                var typeSortChar = item[0].ToString();
                var column = item[1..];

                // Kiểm tra cột có trong table database không
                if (!_helper.IsPropertyInClass<Employee>(column))
                    continue;

                string typeSort;
                if (typeSortChar == "+")
                    typeSort = "ASC";
                else if (typeSortChar == "-")
                    typeSort = "DESC";
                else continue;

                string baseQuery = $"{column} {typeSort}";
                queryItems.Add(baseQuery);
            }
            var query = queryItems.Count > 0 ? $"ORDER BY {string.Join(", ", queryItems)} " : string.Empty;
            return query;
        }
        /// <summary>
        /// Tạo các câu truy vấn lọc
        /// </summary>
        /// <param name="filter">Chuỗi query lọc</param>
        /// <example>filter=FullName=Linh,EmployeeCode=NV-0123 => WHERE (FullName LIKE CONCAT("%", 'Linh', "%"))  
        /// => WHERE (FullName LIKE CONCAT("%", 'Linh', "%")) WHERE (EmployeeCode LIKE CONCAT("%", 'NV0123', "%")) </example>
        /// <returns>Các câu truy vấn lọc</returns>
        private string GenerateQueryFilter(string? filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
                return string.Empty;

            var query = string.Empty;
            var filterItems = filter.Split(',').ToList();

            foreach (var item in filterItems)
            {
                if (item.Length < 2) continue;

                var queryItem = string.Empty;

                // Tách column và giá trị
                var column = string.Empty;
                var value = string.Empty;
                var compareOperator = string.Empty;

                var compareOperators = new List<string>() { "=", "<", ">" };
                int compareIndex = -1;

                foreach (var compare in compareOperators)
                {
                    compareIndex = item.IndexOf(compare);
                    if (compareIndex >= 0)
                    {
                        compareOperator = compare;
                        break;
                    }
                }

                if (compareIndex >= 0)
                {
                    column = item[..compareIndex];
                    value = item[(compareIndex + 1)..];

                    if (!_helper.IsPropertyInClass<Employee>(column))
                        continue;

                    // Trường hợp value rỗng
                    if (string.IsNullOrEmpty(value))
                    {
                        queryItem = $"AND ({column} IS NULL OR {column} LIKE '') \r\n";
                    }
                    else
                    {
                        // Phân loại so sánh
                        switch (compareOperator)
                        {
                            case "=":
                                queryItem = $"AND ({column} LIKE '{value}') \r\n";
                                break;
                            case ">":
                                queryItem = $"AND ({column} > '{value}') \r\n";
                                break;
                            case "<":
                                queryItem = $"AND ({column} < '{value}') \r\n";
                                break;
                            default:
                                break;
                        }
                    }

                    query += queryItem;
                }
            }
            return query;
        }
        #endregion
    }
}

