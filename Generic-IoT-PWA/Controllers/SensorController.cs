using Generic_IoT_PWA.Data;
using Generic_IoT_PWA.Data.Extensions;
using Generic_IoT_PWA.Data.Helpers;
using Generic_IoT_PWA.Models;
using Generic_IoT_PWA.Models.Devices.DemoSensor;
using Generic_IoT_PWA.Services.Database;
using Generic_IoT_PWA.Settings;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Diagnostics;

namespace Generic_IoT_PWA.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly IPaginationSettings _paginationSettings;

        public SensorController(IDataService dataService, IPaginationSettings paginationSettings)
        {
            _dataService = dataService;
            _paginationSettings = paginationSettings;
        }
        /// <summary>
        /// Returns all Devices in a list of Device objects
        /// </summary>
        /// <param name="includeInactives">Determines whether or not inactive Device are returned as part of the list</param>
        /// <returns>Returns a list of Device objects</returns>
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Device>>> GetAllAsync([Query] bool includeInactives = false)
        {
            var devices = await _dataService.GetAllDevicesAsync(includeInactives);

            return devices.OrderBy(x => x.Name).ToList();
        }
        /// <summary>
        /// Returns a single Device as a device object
        /// </summary>
        /// <param name="id">Id of the device you want to return</param>
        /// <returns>A device object</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Device>> GetAsync(Guid id) => await _dataService.GetDeviceAsync(id);
    }
}
