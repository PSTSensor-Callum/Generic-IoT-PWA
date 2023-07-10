namespace Generic_IoT_PWA.Settings
{
    public class PaginationSettings : IPaginationSettings
    {
        public int DefaultPageSize { get; set; } = 20;
    }

    public interface IPaginationSettings
    {
        int DefaultPageSize { get; set; }
    }
}
