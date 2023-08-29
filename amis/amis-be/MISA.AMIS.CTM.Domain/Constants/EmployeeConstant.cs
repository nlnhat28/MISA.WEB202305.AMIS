namespace MISA.AMIS.CTM.Domain
{
    /// <summary>
    /// Hằng số liên quan đến nhân viên
    /// </summary>
    /// Created by: nlnhat (17/07/2023)
    public static class EmployeeConstant
    {
        /// <summary>
        /// Tiền tố mã nhân viên
        /// </summary>
        public const string PrefixCode = "NV-";
        /// <summary>
        /// Chênh lệch cho phép giữa code mới và code lớn nhất hiện tại
        /// </summary>
        public const int OffsetCode = 1000;
        /// <summary>
        /// Độ dài tối thiểu của mã
        /// </summary>
        public const int MinLengthCode = 4;
        /// <summary>
        /// Tên thực thể
        /// </summary>
        public const string EntityName = "Employee";
    }
}
