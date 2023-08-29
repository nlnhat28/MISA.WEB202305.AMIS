using Dapper;

namespace MISA.AMIS.CTM.Domain
{
    /// <summary>
    /// Giao diện các phương thức trợ giúp tầng infrastructure
    /// </summary>
    /// Created by: nlnhat (20/07/2023)
    public interface IInfrastructureHelper
    {
        /// <summary>
        /// Check xem 1 property có trong 1 class hay không
        /// </summary>
        /// <typeparam name="T">Class check</typeparam>
        /// <param name="propertyName">Tên property</param>
        /// <returns>True nếu có, false nếu không</returns>
        /// Created by: nlnhat (20/07/2023)
        bool IsPropertyInClass<T>(string propertyName);
        /// <summary>
        /// Tạo param từ các properties trong class (entity)
        /// </summary>
        /// <typeparam name="TEntity">Thực thể</typeparam>
        /// <returns>Param được add các tham số từ properties của class</returns>
        /// Created by: nlnhat (20/07/2023)
        public DynamicParameters GetParamFromEntity<TEntity>(TEntity? entity);
    }
}
