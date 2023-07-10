using Generic_IoT_PWA.Models.Abstracts.Entities;
using Generic_IoT_PWA.Models.Interfaces;

namespace Generic_IoT_PWA.Models.GenericSensor
{
    public class GenericSensor : EditableEntity, IDtoable<GenericSensorDto>, IAccelerometer, IDistance, IElectrochemical, IHumidity, ITemperature
    {
        public int Temperature { get; set; }
        public int Humidity { get; set; }
        public double Distance { get; set; }
        public string ElectrochemicalName { get; set; } = string.Empty;
        public string ElectrochemicalDescription { get; set; } = string.Empty;
        public int ElectrochemicalReading { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public GenericSensor(int temperature, int humidity, double distance, string electrochemicalName, string electrochemicalDescription, int electrochemicalReading, double x, double y, double z)
        {
            Temperature = temperature;
            Humidity = humidity;
            Distance = distance;
            ElectrochemicalName = electrochemicalName;
            ElectrochemicalDescription = electrochemicalDescription;
            ElectrochemicalReading = electrochemicalReading;
            X = x;
            Y = y;
            Z = z;
        }

        public GenericSensor(GenericSensorCreateDto genericSensorCreateDto) : base(genericSensorCreateDto.Active) 
        {
            Temperature = genericSensorCreateDto.Temperature;
            Humidity = genericSensorCreateDto.Humidity;
            Distance = genericSensorCreateDto.Distance;
            ElectrochemicalName = genericSensorCreateDto.ElectrochemicalName;
            ElectrochemicalDescription = genericSensorCreateDto.ElectrochemicalDescription;
            ElectrochemicalReading = genericSensorCreateDto.ElectrochemicalReading;
            X = genericSensorCreateDto.X;
            Y = genericSensorCreateDto.Y;
            Z = genericSensorCreateDto.Z;
        }

        public GenericSensorDto ToDto() => new(this);
    }
}
