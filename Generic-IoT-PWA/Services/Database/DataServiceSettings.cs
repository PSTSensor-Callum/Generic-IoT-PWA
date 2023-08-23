using Generic_IoT_PWA.Services.Database;

namespace Generic_IoT_PWA.Services
{
    public class DataServiceSettings : IDataServiceSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string DeviceCollectionName { get; set; }
        public string TemperatureCollectionName { get; set; }
        public string ElectrochemicalCollectionName { get; set; }
    }
}
