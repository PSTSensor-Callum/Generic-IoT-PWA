using Generic_IoT_PWA.Data.Extensions;
using Generic_IoT_PWA.Models.Abstracts.Dtos;
using Generic_IoT_PWA.Models.Abstracts.Entities;

namespace Generic_IoT_PWA.Data.Helpers
{
    public record UpdateResult<T>(T Entity, Dictionary<string, PropertyResult> Results) where T : EditableEntity;

    public static class UpdateHelper
    {
        public static string IncorrectPropertyNameMessage(string className, string propertyName) =>
            $"Class '{className}', does not have a property named '{propertyName}'.";

        public static string TypeMismatchMessage(string className, string propertyName, string expectedType) =>
            $"Property '{propertyName}' in Class '{className}' expects a value that can be casted to Type '{expectedType}'.";

        public static string DedicatedUpdateMessage(string className, string parameterName, List<string> routes)
        {
            string baseString = $"{className}: '{parameterName}' can be changed using the following endpoints;";

            return routes.Count > 1
                // commas between every route, except last, which is separated with an and.
                ? $"{baseString} {string.Join(", ", routes.Take(routes.Count - 1).Select(x => $"'{x}'"))} and '{routes.Last()}'."
                : $"{baseString} '{routes.First()}'.";
        }

        public static string UnchangeablePropertyMessage(string className, string propertyName) =>
            $"Property '{propertyName}' in Class '{className}' cannot be changed.";


        public static UpdateResult<T> ProcessUpdate<T>(T entity, KeyValuePair<string, string> change, Dictionary<string, PropertyResult> results,
            List<string> unchangeableProperties) where T : EditableEntity =>
            ProcessUpdate(entity, change, results, null, unchangeableProperties);

        public static UpdateResult<T> ProcessUpdate<T>(T entity, KeyValuePair<string, string> change, Dictionary<string, PropertyResult> results,
            Dictionary<string, List<string>>? dedicatedEndpoints = null, List<string>? unchangeableProperties = null)
            where T : EditableEntity
        {
            string className = typeof(T).Name;
            string propertyName = change.Key;

            // Check to see if a property is deemed unchangeable
            if (propertyName != nameof(EditableEntity.Id) && unchangeableProperties != null && unchangeableProperties.Contains(propertyName))
            {
                results.Add(propertyName, new(false, UnchangeablePropertyMessage(className, propertyName)));
                return new(entity, results);
            }

            // Check to see if a dedicated update endpoint exists
            if (dedicatedEndpoints != null && dedicatedEndpoints.ContainsKey(propertyName))
            {
                results.Add(propertyName, new(false, DedicatedUpdateMessage(className, propertyName, dedicatedEndpoints[propertyName])));
                return new(entity, results);
            }

            // check to see if property exists in class
            var propertyInfo = typeof(T).GetProperty(propertyName);
            if (propertyInfo == null)
            {
                // make sure the first letter is pascal case and try one more time before failing
                propertyName = propertyName.ToPascal();
                propertyInfo = typeof(T).GetProperty(propertyName);

                if (propertyInfo == null)
                {
                    results.Add(propertyName, new(false, IncorrectPropertyNameMessage(className, propertyName)));
                    return new(entity, results);
                }
            }

            Type propertyType = propertyInfo.PropertyType;
            object? value = null;

            // try to convert value to expected type
            try
            {
                if (propertyType == typeof(string)) value = change.Value;

                else if (propertyType == typeof(bool)) value = bool.Parse(change.Value);
                else if (propertyType == typeof(bool?)) value = change.Value != null ? bool.Parse(change.Value) : null;

                else if (propertyType == typeof(int)) value = int.Parse(change.Value);
                else if (propertyType == typeof(int?)) value = change.Value != null ? int.Parse(change.Value) : null;

                else if (propertyType == typeof(double)) value = double.Parse(change.Value);
                else if (propertyType == typeof(double?)) value = change.Value != null ? double.Parse(change.Value) : null;

                else if (propertyType == typeof(float)) value = float.Parse(change.Value);
                else if (propertyType == typeof(long)) value = change.Value != null ? long.Parse(change.Value) : null;

                else if (propertyType == typeof(decimal)) value = decimal.Parse(change.Value);
                else if (propertyType == typeof(decimal?)) value = change.Value != null ? decimal.Parse(change.Value) : null;

                else if (propertyType == typeof(DateTime)) value = DateTime.Parse(change.Value).ToUniversalTime();
                else if (propertyType == typeof(DateTime?)) value = change.Value != null ? DateTime.Parse(change.Value).ToUniversalTime() : null;

                else if (propertyType.IsEnum && propertyType.IsNullable()) value = change.Value != null ? Enum.Parse(propertyType, (string)change.Value) : null;
                else if (propertyType.IsEnum && change.Value != null) value = Enum.Parse(propertyType, (string)change.Value);

            }
            catch (Exception)
            {
                results.Add(propertyName, new(false, TypeMismatchMessage(className, propertyName, propertyType.ToString())));
                return new(entity, results);
            }

            propertyInfo.SetValue(entity, value);
            results.Add(propertyName, new(true));

            return new(entity, results);
        }
    }
}
