using Dapper;
using System.Reflection;

namespace MISA.AMIS.CTM.Domain
{
    /// <summary>
    /// Giao diện các phương thức trợ giúp tầng infrastructure
    /// </summary>
    /// Created by: nlnhat (20/07/2023)
    public class InfrastructureHelper : IInfrastructureHelper
    {
        /// <summary>
        /// Check xem 1 property có trong 1 class hay không
        /// </summary>
        /// <typeparam name="T">Class check</typeparam>
        /// <param name="propertyName">Tên property</param>
        /// <returns>True nếu có, false nếu không</returns>
        /// Created by: nlnhat (20/07/2023)
        public bool IsPropertyInClass<T>(string propertyName)
        {
            var type = typeof(T);
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                if (propertyName.Contains(property.Name))
                {
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// Tạo param từ các properties trong class (entity)
        /// </summary>
        /// <typeparam name="TEntity">Thực thể</typeparam>
        /// <param name="entity">Đối tượng</param>
        /// <returns>Param được add các tham số từ properties của class</returns>
        /// Created by: nlnhat (20/07/2023)
        public DynamicParameters GetParamFromEntity<TEntity>(TEntity? entity)
        {
            var param = new DynamicParameters();

            var entityType = typeof(TEntity);
            var properties = entityType.GetProperties();

            foreach (var property in properties)
            {
                var propertyName = "p_" + property.Name;
                var propertyValue = property.GetValue(entity);
                param.Add(propertyName, propertyValue);
            }

            return param;
        }
    }
}
