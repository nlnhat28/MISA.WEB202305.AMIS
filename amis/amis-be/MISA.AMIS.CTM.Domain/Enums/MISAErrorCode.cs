namespace MISA.AMIS.CTM.Domain
{
    /// <summary>
    /// Mã lỗi nội bộ
    /// </summary>
    /// Created by: nlnhat (17/07/2023)
    public enum MISAErrorCode
    {
        #region Mã lỗi chung
        /// <summary>
        /// Không tìm thấy dữ liệu
        /// </summary>
        NotFound = 1000,
        /// <summary>
        /// Dữ liệu không hợp lệ
        /// </summary>
        InvalidData = 1001,
        /// <summary>
        /// Server error
        /// </summary>
        ConflictData = 1002,
        /// <summary>
        /// Server error
        /// </summary>
        ServerError = 1003,
        /// <summary>
        /// Server error
        /// </summary>
        InvalidPageSize = 1004,
        /// <summary>
        /// Lỗi excel
        /// </summary>
        ExcelError = 1005,
        /// <summary>
        /// Tham số không hợp lệ
        /// </summary>
        InvalidParameter = 1006,
        /// <summary>
        /// Thiếu tham số
        /// </summary>
        Parameterless = 1007,
        /// <summary>
        /// Không thể hoàn thành tác vụ
        /// </summary>
        IncompleteTask = 1008,
        /// <summary>
        /// Không xoá được hết
        /// </summary>
        IncompleteDelete = 1009,
        #endregion

        #region Mã lỗi phân hệ nhân viên
        /// <summary>
        /// Mã nhân viên sai định dạng
        /// </summary>
        EmployeeCodeWrongFormat = 2001,
        /// <summary>
        /// Trùng mã nhân viên
        /// </summary>
        EmployeeCodeDuplicated = 2002,
        /// <summary>
        /// Trùng mã nhân viên
        /// </summary>
        EmployeeCodeOutOfRange = 2003,
        /// <summary>
        /// Không tìm thấy nhân viên
        /// </summary>
        EmployeeNotFound = 2004,
        /// <summary>
        /// Lỗi xuất file excel
        /// </summary>
        EmployeeExportError = 2005,
        #endregion

        #region Mã lỗi phân hệ phòng ban
        /// <summary>
        /// Không tìm thấy phòng ban
        /// </summary>
        DepartmentNotFound = 3004,
        #endregion
    }
}
