using Generic_IoT_PWA.Models.Abstracts.Entities;
using System.Text.Json;

namespace Generic_IoT_PWA.Data.Extensions
{
    public static class EntityExtensions
    {
        public static List<Guid> ToIdList(this IEnumerable<Entity> entities) => entities.Select(x => x.Id).ToList();

        public static string? SerialiseIds(this IEnumerable<Entity>? entities) => entities != null ? JsonSerializer.Serialize(entities.ToIdList()) : null;

        public static string? SerialiseProperty<T>(this T entity, string property) where T : Entity
        {
            var value = typeof(T).GetProperty(property)?.GetValue(entity);
            return value != null ? JsonSerializer.Serialize(value) : null;
        }
    }
}
