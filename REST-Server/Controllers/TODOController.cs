using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace REST_Server.Controllers
{
    [Route("api/[controller]")]
    public class TODOController : Controller
    {
        readonly ILogger logger;

        static Dictionary<int, string> TODOList = new Dictionary<int, string>();
        static TODOController()
        {
            TODOList.Add(1, "Make tea");
            TODOList.Add(2, "Do Laundry");
        }

        public TODOController(ILogger<TODOController> logger)
        {
            this.logger = logger;
        }

        // GET api/TODO
        [HttpGet]
        public IEnumerable<string> Get()
        {
            Console.Write(TODOList.Keys);
            return TODOList.Values;
        }

        // GET api/TODO/1
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return TODOList[id];
        }

        // POST api/TODO
        [HttpPost("{id}")]
        public int Post(int id, [FromBody]string value)
        {
            if (TODOList.ContainsKey(id))
                throw new ArgumentException(string.Format("Key: {0} already found.", id));
            TODOList.Add(id, value);
            return id;
        }

        // PUT api/TODO/5
        [HttpPut("{id}")]
        public int Put(int id, [FromBody]string value)
        {
            if (TODOList.ContainsKey(id))
                throw new KeyNotFoundException(string.Format("Key: {0} already found.", id));
            TODOList[id] = value;
            return id;
        }

        // DELETE api/TODO/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            TODOList.Remove(id);
        }
    }
}
