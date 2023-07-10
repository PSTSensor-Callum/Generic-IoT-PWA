namespace Generic_IoT_PWA.Models.Interfaces
{
    public interface IElectrochemical
    {
        //Might be worth changing to model of its own so it can be a list?
        public string ElectrochemicalName { get; set; }
        public string ElectrochemicalDescription { get; set; }
        public int ElectrochemicalReading { get; set; }
    }
}
