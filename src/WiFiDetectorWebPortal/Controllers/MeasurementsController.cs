using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lafe.WiFiDetector.DTOs;
using lafe.WiFiDetector.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace lafe.WiFiDetector.Controllers
{
    [Route("api/[controller]")]
    public class MeasurementsController : Controller
    {
        protected MeasurementContext MeasurementContext { get; set; }

        public MeasurementsController(MeasurementContext measurementContext)
        {
            MeasurementContext = measurementContext;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<MeasurementDto>> Get()
        {
            var result = await MeasurementContext.Measurements.OrderByDescending(m => m.Timestamp).Take(100).Select(m => new MeasurementDto
            {
                BSSID = m.BSSID,
                SSID = m.SSID,
                Timestamp = m.Timestamp,
                Id = m.Id,
                Encryption = m.Encryption,
                SignalStrength = m.SignalStrength,
                DeviceId = m.DeviceId,
                Channel = m.Channel,
                Hidden = m.Hidden
            }).ToListAsync();

            return result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]IEnumerable<MeasurementDto> measurements)
        {
            try
            {
                var measurementTable = MeasurementContext.Measurements;

                var timestamp = DateTime.UtcNow;

                var dbMeasurements = measurements.Select(measurement => new Measurement
                {
                    BSSID = measurement.BSSID,
                    SSID = measurement.SSID,
                    DeviceId = measurement.DeviceId,
                    Encryption = measurement.Encryption,
                    SignalStrength = measurement.SignalStrength,
                    Channel = measurement.Channel,
                    Hidden = measurement.Hidden
                }).ToList();

                measurementTable.AddRange(dbMeasurements);
                await MeasurementContext.SaveChangesAsync();
                return Created("Get", dbMeasurements);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
