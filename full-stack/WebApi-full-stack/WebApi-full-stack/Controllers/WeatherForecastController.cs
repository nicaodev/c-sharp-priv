using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi_full_stack.Model;

namespace WebApi_full_stack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Evento>> Get()
        {
            return new Evento[]
            {
                new Evento()
                {
                     Id=1,
                     CargaHoraria=20,
                     Cidade = "Valparaíso de Goiás",
                     Data = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy"),
                     Nome = "Curso de Angular"
                }
            };
        }
    }
}
