namespace WebApiVer01.Controllers
{
    
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
      
       // [ResponseCache(Duration = 15)]
        //[Authorize]
        //[Route("ObtenerValores")]
        [HttpGet(Name = "ObtenerValores")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2", "utut" };
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "ObtenerValor")]
        public ActionResult<string> Get(int id)
        {
            id++;
            var b = id * 2;
            return "value " + b.ToString();
        }

        // POST api/values
     
        //[Route("CrearValor")]
        [HttpPost(Name = "CrearValor")]
        public string Post([FromBody] string value)
        {
            return value;
        }

        // PUT api/values/5
        [HttpPut("{id}", Name = "ActualizarValor")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}", Name = "BorrarValor")]
        public void Delete(int id)
        {
        }
    }
}