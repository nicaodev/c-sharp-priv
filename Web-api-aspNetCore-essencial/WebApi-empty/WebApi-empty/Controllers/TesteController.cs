using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_empty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TesteController : ControllerBase
    {
        /*Definir no LaunchSettings.json
         a  "launchUrl": "api/teste" dentro de profiles
        */

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Teste 1 ", "Teste 2 " };
        }
        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            //Por enquanto não vai ter utilidade
        }
        // PUT api/values
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            //Por enquanto não vai ter utilidade
        }
        // DELETE api/values
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            //Por enquanto não vai ter utilidade
        }

        [HttpGet("pessoas")]
        public ActionResult<IEnumerable<Pessoa>> GetPessoas()
        {
            return new[]
            {
                new Pessoa { Nome= "Nicolas"},
                new Pessoa { Nome= "Alexandre"},
                new Pessoa { Nome= "da Silva Pereira"}
            };
        }
    }

    public class Pessoa
    {
        public string Nome { get; set; }
    }
}
