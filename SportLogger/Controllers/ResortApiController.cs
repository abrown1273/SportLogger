using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportLogger.Data;
using SportLogger.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportLogger.Controllers
{
    [Route("api/[controller]")]
    public class ResortApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResortApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: api/SkiDayApi
        /// </summary>
        [HttpGet]
        public IEnumerable<ResortReference> GetResort()
        {
            return _context.ResortReference;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
