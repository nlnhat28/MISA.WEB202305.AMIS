﻿using MISA.AMIS.CTM.Domain;
using System.Data.Common;

namespace MISA.AMIS.CTM.Infrastructure.UnitOfWork
{
    /// <summary>
    /// Triển khai interface IUnitOfWork, quản lý kết nối và giao dịch với cơ sở dữ liệu
    /// </summary>
    /// Created by: nlnhat (18/07/2023)
    public sealed class UnitOfWork : IUnitOfWork
    {
        #region Fields
        /// <summary>
        /// Đối tượng kết nối với cơ sở dữ liệu
        /// </summary>
        /// Created by: nlnhat (18/07/2023)
        private readonly DbConnection _connection;
        /// <summary>
        /// Đối tượng giao dịch với cơ sở dữ liệu
        /// </summary>
        /// Created by: nlnhat (18/07/2023)
        private DbTransaction? _transaction = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Hàm tạo lớp UnitOfWork
        /// </summary>
        /// <param name="connection">Kết nối đến database</param>
        /// Created by: nlnhat (18/07/2023)
        public UnitOfWork(DbConnection connection)
        {
            _connection = connection;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Override connection interface IUnitOfWork
        /// </summary>
        /// Created by: nlnhat (18/07/2023)
        public DbConnection Connection => _connection;
        /// <summary>
        /// Override transaction interface IUnitOfWork
        /// </summary>
        /// Created by: nlnhat (18/07/2023)
        public DbTransaction? Transaction => _transaction;
        #endregion

        #region Methods
        /// <summary>
        /// Bắt đầu giao dịch
        /// </summary>
        /// Created by: nlnhat (18/07/2023)
        public void BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
        }
        /// <summary>
        /// Bắt đầu giao dịch bất đồng bộ
        /// </summary>
        /// Created by: nlnhat (18/07/2023)
        public async Task BeginTransactionAsync()
        {
            _transaction = await _connection.BeginTransactionAsync();
        }
        /// <summary>
        /// Xác nhận giao dịch
        /// </summary>
        /// Created by: nlnhat (18/07/2023)
        public void Commit()
        {
            _transaction?.Commit();
            Dispose();
        }
        /// <summary>
        /// Xác nhận giao dịch bất đồng bộ
        /// </summary>
        /// Created by: nlnhat (18/07/2023)
        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }
            await DisposeAsync();
        }
        /// <summary>
        /// Hủy bỏ giao dịch
        /// </summary>
        /// Created by: nlnhat (18/07/2023)
        public void Rollback()
        {
            _transaction?.Rollback();
            Dispose();
        }
        /// <summary>
        /// Hủy bỏ giao dịch bất đồng bộ
        /// </summary>
        /// Created by: nlnhat (18/07/2023)
        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
            await DisposeAsync();
        }
        /// <summary>
        /// Hàm giải phóng tài nguyên
        /// </summary>
        /// Created by: nlnhat (18/07/2023)
        public void Dispose()
        {
            _transaction?.Dispose();
            _transaction = null;
        }
        /// <summary>
        /// Hàm giải phóng tài nguyên bất đồng bộ
        /// </summary>
        /// Created by: nlnhat (18/07/2023)
        public async ValueTask DisposeAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
            }
            _transaction = null;
        }
        #endregion
    }
}
