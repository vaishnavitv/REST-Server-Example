using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace REST_Server.Controllers
{
    [Route("api/[controller]")]
    public class TODOController : Controller
    {
        static Dictionary<int, string> TODOList = new Dictionary<int, string>();
        static TODOController()
        {
            TODOList.Add(1, "Make tea");
            TODOList.Add(2, "Do Laundry");
        }

        // GET api/TODO
        [HttpGet]
        public IEnumerable<string> Get()
        {
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
            TODOList.Add(id, value);
            return id;
        }

        // PUT api/TODO/5
        [HttpPut("{id}")]
        public int Put(int id, [FromBody]string value)
        {
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
