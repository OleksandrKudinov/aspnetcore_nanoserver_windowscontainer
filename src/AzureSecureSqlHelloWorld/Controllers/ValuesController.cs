using System.Collections.Generic;
using System.Linq;
using AzureSecureSqlHelloWorld.DB;
using AzureSecureSqlHelloWorld.Models;
using Microsoft.AspNetCore.Mvc;

namespace AzureSecureSqlHelloWorld.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        public ValuesController(MyAppContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return _context.Students.ToArray();
        }

        [HttpGet("{id}")]
        public Student Get(int id)
        {
            return _context.Students.FirstOrDefault(x=>x.Id == id);
        }

        [HttpPost]
        public void Post([FromBody]Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var student = _context.Students.FirstOrDefault(x => x.Id == id);
            if (student != null)
            {

                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }

        private readonly MyAppContext _context;
    }
}
