using Generic_IoT_PWA.Models.Abstracts.Dtos;

namespace Generic_IoT_PWA.Models.GenericSensor
{
    public class GenericSensorDto : Dto
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

        public GenericSensorDto() { }

        public GenericSensorDto(GenericSensor genericSensor) : base(genericSensor.Id, genericSensor.Active)
        {
            Temperature = genericSensor.Temperature;
            Humidity = genericSensor.Humidity;
            Distance = genericSensor.Distance;
            ElectrochemicalName = genericSensor.ElectrochemicalName;
            ElectrochemicalDescription = genericSensor.ElectrochemicalDescription;
            ElectrochemicalReading = genericSensor.ElectrochemicalReading;
            X = genericSensor.X;
            Y = genericSensor.Y;
            Z = genericSensor.Z;
        }
    }
}
