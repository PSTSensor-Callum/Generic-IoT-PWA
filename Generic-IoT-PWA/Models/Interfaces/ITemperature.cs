namespace Generic_IoT_PWA.Models.Interfaces
{
    public interface ITemperature
    {
        public Dictionary<DateTime, double>? Measurements { get; set; }
        public double Sum { get; set; }
    }
}
