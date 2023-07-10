using Generic_IoT_PWA.Models.Abstracts.Dtos;

namespace Generic_IoT_PWA.Models.GenericSensor
{
    public class GenericSensorCreateDto : CreateDto
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

        public GenericSensorCreateDto() { }

        public GenericSensorCreateDto(int temperature, int humidity, double distance, string electrochemicalName, string electrochemicalDescription, int electrochemicalReading, double x, double y, double z)
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
    }
}
