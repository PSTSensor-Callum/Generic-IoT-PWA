using Generic_IoT_PWA.Data;
using Generic_IoT_PWA.Data.Extensions;
using Generic_IoT_PWA.Data.Helpers;
using Generic_IoT_PWA.Models;
using Generic_IoT_PWA.Models.Abstracts.Dtos;
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


        /// <summary>
        /// Creates a new Generic Sensor
        /// </summary>
        /// <remarks>
        /// Takes a Generic Sensor CreateDto and creates a new Generic Sensor
        /// **Active** - Boolean, whether or not the Generic Sensor will be Active on Creation (Usually True) \
        /// **Temperature** - The most recent Temperature reading of the sensor
        /// **Humidity** - The most recent Humidity reading of the sensor
        /// **ElectrochemicalName** - The name of the electrochemical that the sensor detects
        /// **ElectrochemicalDescription** - The Description of the electrochemical
        /// **ElectrochemicalReading** - The most recent electrochemical reading of the sensor
        /// **X** - The most recent X coordinate of the sensor
        /// **Y** - The most recent Y coordinate of the sensor
        /// **Z** - The most recent Z coordinate of the sensor
        /// Sample Request:
        /// 
        ///     POST
        ///     {
        ///         "active": true,
        ///         "Temperature": 22,
        ///         "Humidity": 58,
        ///         "ElectrochemicalName": "A Electrochemical device",
        ///         "ElectrochemicalDescription": "Uses Galvanic cell",
        ///         "ElectrochemicalReading": 1042,
        ///         "X": 125,
        ///         "Y": 297,
        ///         "Z": 20
        ///     }
        /// </remarks>
        /// <param name="genericSensorCreateDto">Generic Sensor you want to create</param>
        /// <returns>The created Generic Sensor as a DTO</returns>
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<GenericSensorDto>> CreateAsync([FromBody] GenericSensorCreateDto genericSensorCreateDto)
        {
            GenericSensor genericSensor = new(genericSensorCreateDto);

            await _dbContext.GenericSensors.AddAsync(genericSensor);
            await _dbContext.SaveChangesAsync();

            return Ok(genericSensor.ToDto());
        }

        /// <summary>
        /// Disables an existing Generic Sensor
        /// </summary>
        /// <param name="id">The Generic Sensor you want to disable</param>
        /// <returns>The disabled Generic Sensor as a DTO</returns>
        [HttpDelete("{id}/delete")]
        public async Task<ActionResult<GenericSensorDto>> DeleteAsync(Guid id)
        {
            GenericSensor? genericSensor = await _dbContext.GenericSensors.FindAsync(id);
            if (genericSensor == null) return ActionResultHelper.DoesNotExist(this, nameof(genericSensor), id);

            genericSensor.Active = false;

            return Ok(genericSensor.ToDto());
        }

        private static readonly List<string> _unchangeableProperties = new()
        {
            nameof(GenericSensor.Id)
        };

        /// <summary>
        /// Updates an existing Generic Sensor
        /// </summary>
        /// <remarks>
        /// Takes an Id and a Dictionary containing the updated information \
        /// **Id** - The Id of the Generic Sensor being updated \
        /// **string** - The key of the Property you want to change \
        /// **object** - The object you want the values changed to 
        /// 
        /// Sample request: 
        /// 
        ///     POST
        ///     {
        ///         "Temperature": 32,
        ///         "X": 50
        ///     }        
        /// </remarks>
        /// <param name="id">The Id of the Generic Sensor being Updated</param>
        /// <param name="propertyChanges">A Dictionary of Changes to be made to the Generic Sensor</param>
        /// <returns>A update response DTO of the new Generic Sensor and the results</returns>
        [HttpPut("{id}/update")]
        public async Task<ActionResult<UpdateResponseDto<GenericSensorDto>>> UpdateAsync(Guid id, [FromBody] Dictionary<string, string> propertyChanges)
        {

            GenericSensor? genericSensor = await _dbContext.GenericSensors.FindAsync(id);
            if (genericSensor == null) return ActionResultHelper.DoesNotExist(this, nameof(genericSensor), id);

            GenericSensor updateGenericSensor = genericSensor;

            Dictionary<string, PropertyResult> results = new();

            foreach (KeyValuePair<string, string> change in propertyChanges)
            {
                string? originalValue = genericSensor.SerialiseProperty(change.Key.ToPascal());

                UpdateResult<GenericSensor> updateResult = UpdateHelper.ProcessUpdate(genericSensor, change, results, _unchangeableProperties);
                updateGenericSensor = updateResult.Entity;
                results = updateResult.Results;
            }
            _dbContext.Entry(genericSensor).CurrentValues.SetValues(updateGenericSensor);
            await _dbContext.SaveChangesAsync();

            return new UpdateResponseDto<GenericSensorDto>(updateGenericSensor.ToDto(), results);
        }
    }
}
