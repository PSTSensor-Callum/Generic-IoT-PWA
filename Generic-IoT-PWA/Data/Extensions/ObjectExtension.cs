namespace Generic_IoT_PWA.Data.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNullable(this Type type) => Nullable.GetUnderlyingType(type) != null;
    }
}
