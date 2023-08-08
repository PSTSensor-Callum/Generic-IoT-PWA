using Generic_IoT_PWA.Models.Abstracts.Entities;
using Generic_IoT_PWA.Models.Interfaces;
using Generic_IoT_PWA.Models.Sensors;

namespace Generic_IoT_PWA.Models.Devices.DemoSensor
{
    public class Device : EditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Device()
        {

        }
    }
}
