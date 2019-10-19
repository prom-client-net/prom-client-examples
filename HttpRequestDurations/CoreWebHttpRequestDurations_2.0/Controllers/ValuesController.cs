using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebHttpRequestDurations.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
      
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] {"value1", "value2"};
        }

        [HttpGet("{id}")]
        public IEnumerable<string> Get(int id)
        {
            return new string[]
            {
                $"value1_{id}", $"value2_{id}"
            };
        }
        

        [HttpGet("long")]
        public IEnumerable<string> GetLong()
        {
            Thread.Sleep(2000);
            return new string[] {"long", "long"};
        }
        
        [HttpGet("500")]
        public IEnumerable<string> Get500()
        {
            throw new Exception("500");
        }


        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            Thread.Sleep(500);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
