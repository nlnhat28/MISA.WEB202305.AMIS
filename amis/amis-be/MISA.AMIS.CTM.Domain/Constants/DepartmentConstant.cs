namespace MISA.AMIS.CTM.Domain
{
    /// <summary>
    /// Hằng số liên quan đến phòng ban
    /// </summary>
    /// Created by: nlnhat (19/07/2023)
    public static class DepartmentConstant
    {
        /// <summary>
        /// Tiền tố mã phòng ban
        /// </summary>
        public const string PrefixCode = "PB-";
        /// <summary>
        /// Chênh lệch cho phép giữa code mới và code lớn nhất hiện tại
        /// </summary>
        public const int OffsetCode = 10;
        /// <summary>
        /// Độ dài tối thiểu của mã
        /// </summary>
        public const int MinLengthCode = 2;
        /// <summary>
        /// Tên thực thể
        /// </summary>
        public const string EntityName = "Department";
    }
}
