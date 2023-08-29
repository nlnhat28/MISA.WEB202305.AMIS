﻿namespace MISA.AMIS.CTM.Domain
{
    /// <summary>
    /// Mô hình cơ sở của đối tượng khi lọc
    /// </summary>
    /// <typeparam name="TEntity">Thực thể</typeparam>
    /// Created by: nlnhat (18/07/2023)
    public class BaseFilterModel<TEntity>
    {
        #region Properties
        /// <summary>
        /// Tổng số bản ghi được lọc
        /// </summary>
        public int TotalRecord { get; set; }
        /// <summary>
        /// Tổng số bản ghi trong database
        /// </summary>
        public int AllRecord { get; set; }
        /// <summary>
        /// Số trang hiện tại
        /// </summary>
        public int PageNumber { get; set; }
        /// <summary>
        /// Danh sách đối tượng thoả mãn điều kiện lọc
        /// </summary>
        public IEnumerable<TEntity>? Data { get; set; }
        #endregion
    }
}