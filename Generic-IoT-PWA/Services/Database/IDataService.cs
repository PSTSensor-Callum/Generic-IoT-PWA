using Generic_IoT_PWA.Models.Devices.DemoSensor;
using Generic_IoT_PWA.Models.Sensors;

namespace Generic_IoT_PWA.Services.Database
{
    // Interface to use the NoSQL database service
    public interface IDataService
    {
        Task<List<Device>> GetAllDevicesAsync(bool includeInactive = false);
        Task<Device> GetDeviceAsync(Guid id);
        Task<List<Device>> GetDevicesAsync(List<Guid> ids);
        Task CreateDeviceAsync(Device device);
        Task ReplaceDeviceAsync(Device device);

        Task<List<TemperatureSensor>> GetAllTemperatureSensorsAsync(bool includeInactive = false);
        Task<TemperatureSensor> GetTemperatureSensorAsync(Guid id);
        Task<List<TemperatureSensor>> GetTemperatureSensorsAsync(List<Guid> ids);
        Task CreateTemperatureSensorAsync(TemperatureSensor temperatureSensor);
        Task ReplaceTemperatureSensorAsync(TemperatureSensor temperatureSensor);
    }
}
