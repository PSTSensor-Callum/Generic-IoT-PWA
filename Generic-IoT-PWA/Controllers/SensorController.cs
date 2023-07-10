using Generic_IoT_PWA.Data;
using Generic_IoT_PWA.Data.Extensions;
using Generic_IoT_PWA.Data.Helpers;
using Generic_IoT_PWA.Models;
using Generic_IoT_PWA.Models.GenericSensor;
using Generic_IoT_PWA.Settings;
using Microsoft.AspNetCore.Mvc;

namespace Generic_IoT_PWA.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly PWADbContext _dbContext;
        private readonly IPaginationSettings _paginationSettings;

        public SensorController(PWADbContext dbContext, IPaginationSettings paginationSettings)
        {
            _dbContext = dbContext;
            _paginationSettings = paginationSettings;
        }
        /// <summary>
        /// Gets all the Generic Sensors
        /// </summary>
        /// <param name="includeInactive"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns>All Generic Sensors</returns>
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<Pagination<GenericSensor>>> GetAllAsync([FromQuery] bool includeInactive = false, 
            [FromQuery] int? pageSize = null, [FromQuery] int page = 1)
        {
            if (pageSize == null) pageSize = _paginationSettings.DefaultPageSize;

            IQueryable<GenericSensor> sensors = includeInactive == false
                ? _dbContext.GenericSensors.Where(m => m.Active)
                : _dbContext.GenericSensors;

            return Ok(sensors.GetPaginatedData<GenericSensor, GenericSensorDto>((int)pageSize, page, HttpContext.Request));

        }
        /// <summary>
        /// Gets a specific Generic Sensor
        /// </summary>
        /// <param name="id">The ID of the Generic Sensor you want to return</param>
        /// <returns>A Generic Sensor</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<GenericSensor>> GetGenericSensorAsync(Guid id)
        {
            GenericSensor? sensor = await _dbContext.GenericSensors.FindAsync(id);
            if (sensor == null) return ActionResultHelper.DoesNotExist(this, nameof(sensor), id);

            return Ok(sensor);
        }
    }
}
