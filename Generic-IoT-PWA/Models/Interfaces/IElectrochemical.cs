namespace Generic_IoT_PWA.Models.Interfaces
{
    public interface IElectrochemical
    {
        public string ElectrochemicalName { get; set; }
        public string ElectrochemicalDescription { get; set; }
        public int ElectrochemicalReading { get; set; }
    }
}
