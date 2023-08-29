namespace MISA.AMIS.CTM.Api
{
    public static class Calculator
    {
        /// <summary>
        /// Tính tổng của chuỗi
        /// </summary>
        /// <param name="text">Chuỗi các giá trị phân tách nhau bởi dấu "," (dấu phẩy)</param>
        /// <example>Chuỗi rỗng "" trả về 0; chuỗi "1" trả về 1; chuỗi "1,2,3" trả về 6; chuỗi "1, 2, 3" trả về 6</example>
        /// <returns>Tổng của chuỗi. Nếu giá trị từng toán tử trong phép tính bị âm, thì thông báo lỗi. Ví dụ "1,-2,-3" thì thông báo "Không chấp nhận toán tử âm: -2, -3"</returns>
        /// Created by: nlnhat (13/07/2023)
        public static int Add(string text)
        {
            int sum = 0;
            var negativeNumbers = new List<int>();
            var invalidItems = new List<string>();

            List<string> items = text.Split(",").ToList();
            foreach (string item in items)
            {
                if (string.IsNullOrWhiteSpace(item))
                {
                    continue;
                };
                if (int.TryParse(item, out int number))
                {
                    if (number < 0)
                    {
                        negativeNumbers.Add(number);
                    }
                    else
                    {
                        sum += number;
                    }
                }
                else
                {
                    invalidItems.Add(item.Replace(" ", ""));
                }
            };
            if (invalidItems.Count > 0)
            {
                throw new Exception($"Chuỗi không hợp lệ: {string.Join(" ", invalidItems)}");
            }
            if (negativeNumbers.Count > 0)
            {
                throw new Exception($"Không chấp nhận toán tử âm: {string.Join(", ", negativeNumbers)}");
            }
            return sum;
        }
        /// <summary>
        /// Tính tổng 2 số
        /// </summary>
        /// <param name="x">Số thứ nhất</param>
        /// <param name="y">Số thứ hai</param>
        /// <returns>Tổng 2 số</returns>
        /// Created by: nlnhat (13/07/2023)
        public static long Add(int x, int y)
        {
            return x + (long)y;
        }
        /// <summary>
        /// Tính hiệu 2 số
        /// </summary>
        /// <param name="x">Số bị trừ</param>
        /// <param name="y">Số trừ</param>
        /// <returns>Hiệu 2 số</returns>
        /// Created by: nlnhat (13/07/2023)
        public static long Sub(int x, int y)
        {
            return x - (long)y;
        }
        /// <summary>
        /// Tính tích 2 số
        /// </summary>
        /// <param name="x">Số thứ nhất</param>
        /// <param name="y">Số thứ hai</param>
        /// <returns>Tích 2 số</returns>
        /// Created by: nlnhat (13/07/2023)
        public static long Mul(int x, int y)
        {
            return x * (long)y;
        }
        /// <summary>
        /// Tính thương 2 số
        /// </summary>
        /// <param name="x">Số bị chia</param>
        /// <param name="y">Số chia</param>
        /// <returns>Thương 2 số</returns>
        /// Created by: nlnhat (13/07/2023)
        public static double Div(int x, int y)
        {
            if (y == 0)
            {
                throw new Exception("Không chia được cho 0");
            }
            return x / (double)y;
        }
    }
}