namespace Generic_IoT_PWA.Services.Database
{
    public interface IDataServiceSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string DeviceCollectionName { get; set; }
        string TemperatureCollectionName { get; set; }
        string ElectrochemicalCollectionName { get; set; }

    }
}
