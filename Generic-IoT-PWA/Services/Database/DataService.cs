using Blazor.Bluetooth;
using Generic_IoT_PWA.Models.Devices.DemoSensor;
using Generic_IoT_PWA.Models.Sensors;
using MongoDB.Driver;

namespace Generic_IoT_PWA.Services.Database
{
    public class DataService : IDataService
    {
        private readonly IMongoCollection<Device> _devices;
        private readonly IMongoCollection<TemperatureSensor> _temperatureSensors;

        public DataService(IDataServiceSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _devices = database.GetCollection<Device>(settings.DeviceCollectionName);
            _temperatureSensors = database.GetCollection<TemperatureSensor>(settings.TemperatureCollectionName);
        }



        //Devices
        public async Task<List<Device>> GetAllDevicesAsync(bool includeInactive = false) =>
            includeInactive ? await (await _devices.FindAsync(x => true)).ToListAsync()
                            : await (await _devices.FindAsync(x => x.Active)).ToListAsync();
        public async Task<Device> GetDeviceAsync(Guid id) => await (await _devices.FindAsync(x => x.Id == id)).FirstOrDefaultAsync();
        public async Task<List<Device>> GetDevicesAsync(List<Guid> ids) => await (await _devices.FindAsync(Builders<Device>.Filter.In(x => x.Id, ids))).ToListAsync();
        public async Task CreateDeviceAsync(Device device) => await _devices.InsertOneAsync(device);
        public async Task ReplaceDeviceAsync(Device device) => await _devices.FindOneAndReplaceAsync(x => x.Id == device.Id, device);

        //Temperature sensors
        public async Task<List<TemperatureSensor>> GetAllTemperatureSensorsAsync(bool includeInactive = false) =>
            includeInactive ? await (await _temperatureSensors.FindAsync(x => true)).ToListAsync()
                            : await (await _temperatureSensors.FindAsync(x => x.Active)).ToListAsync();
        public async Task<TemperatureSensor> GetTemperatureSensorAsync(Guid id) => await (await _temperatureSensors.FindAsync(x => x.Id == id)).FirstOrDefaultAsync();
        public async Task<List<TemperatureSensor>> GetTemperatureSensorsAsync(List<Guid> ids) => await (await _temperatureSensors.FindAsync(Builders<TemperatureSensor>.Filter.In(x => x.Id, ids))).ToListAsync();
        public async Task CreateTemperatureSensorAsync(TemperatureSensor temperatureSensor) => await _temperatureSensors.InsertOneAsync(temperatureSensor);
        public async Task ReplaceTemperatureSensorAsync(TemperatureSensor temperatureSensor) => await _temperatureSensors.FindOneAndReplaceAsync(x => x.Id == temperatureSensor.Id, temperatureSensor)
    }
}