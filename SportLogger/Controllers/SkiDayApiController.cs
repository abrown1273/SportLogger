using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportLogger.Data;
using SportLogger.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SportLogger.Controllers
{
    [Produces("application/json")]
    [Route("api/SkiDayApi")]
    public class SkiDayApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SkiDayApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: api/SkiDayApi
        /// </summary>
        [HttpGet]
        public IEnumerable<SkiDay> GetSkiDay()
        {
            //IEnumerable<SkiDay> query = _context.SkiDay.OrderByDescending(p => p.SkiDate);
            return _context.SkiDay;
        }

        /// <summary>
        /// GET: api/SkiDayApi/5
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSkiDay([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var skiDay = await _context.SkiDay.SingleOrDefaultAsync(m => m.Id == id);

            if (skiDay == null)
            {
                return NotFound();
            }

            return Ok(skiDay);
        }

        /// <summary>
        /// PUT: api/SkiDayApi/5
        /// </summary>
        /// <param name="id"></param>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkiDay([FromRoute] int id, [FromBody] SkiDay skiDay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != skiDay.Id)
            {
                return BadRequest();
            }

            _context.Entry(skiDay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkiDayExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// POST: api/SkiDayApi
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostSkiDay([FromBody] SkiDay skiDay)
        {
            if (SkiDateExists(skiDay.SkiDate))
            {
                var msg = string.Format("Ski date {0} already exists", skiDay.SkiDate.ToString("MM/dd/yyyy"));

                ModelState.AddModelError("SkiDate", msg);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SkiDay.Add(skiDay);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SkiDayExists(skiDay.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSkiDay", new { id = skiDay.Id }, skiDay);
        }

        /// <summary>
        /// DELETE: api/SkiDayApi/5
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkiDay([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var skiDay = await _context.SkiDay.SingleOrDefaultAsync(m => m.Id == id);
            if (skiDay == null)
            {
                return NotFound();
            }

            _context.SkiDay.Remove(skiDay);
            await _context.SaveChangesAsync();

            return Ok(skiDay);
        }

        private bool SkiDayExists(int id)
        {
            return _context.SkiDay.Any(e => e.Id == id);
        }

        private bool SkiDateExists(DateTime date)
        {
            return _context.SkiDay.Any(e => e.SkiDate == date);
        }
    }

    [Produces("application/json")]
    [Route("api/PagedSkiDayApi")]
    public class PagedSkiDayApiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly List<SkiDay> _data;

        public PagedSkiDayApiController(ApplicationDbContext context)
        {
            _context = context;
            _data = _context.SkiDay.OrderByDescending(p => p.SkiDate).ToList();
        }

        [HttpGet]
        [Route("{pageIndex:int}/{pageSize:int}")]
        public PagedResponse<SkiDay> Get(int pageIndex, int pageSize)
        {
            return new PagedResponse<SkiDay>(_data, pageIndex, pageSize);
        }
    }

    public class PagedResponse<T>
    {
        public PagedResponse(IEnumerable<T> data, int pageIndex, int pageSize)
        {
            Data = data.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            Total = data.Count();
        }

        public int Total { get; set; }
        public ICollection<T> Data { get; set; }
    }

}