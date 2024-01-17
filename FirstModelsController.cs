using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalsProject;
using FinalsProject.Models;
using System.Xml.Linq;

namespace FinalsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirstModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FirstModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FirstModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FirstModel>>> GetFirstModels()
        {
          if (_context.FirstModels == null)
          {
              return NotFound();
          }
            return await _context.FirstModels.ToListAsync();
        }

        // GET: api/FirstModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FirstModel>> GetFirstModel(int id)
        {
          if (_context.FirstModels == null)
          {
              return NotFound();
          }
            var firstModel = await _context.FirstModels.FindAsync(id);

            if (firstModel == null)
            {
                return NotFound();
            }

            return firstModel;
        }

        // PUT: api/FirstModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFirstModel(int id, FirstModel firstModel)
        {
            if (id != firstModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(firstModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FirstModelExists(id))
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

        // POST: api/FirstModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FirstModel>> PostFirstModel(FirstModel firstModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.FirstModels.Add(firstModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFirstModel", new { id = firstModel.Id }, firstModel);
        }

        // DELETE: api/FirstModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFirstModel(int id)
        {
            if (_context.FirstModels == null)
            {
                return NotFound();
            }
            var firstModel = await _context.FirstModels.FindAsync(id);
            if (firstModel == null)
            {
                return NotFound();
            }

            _context.FirstModels.Remove(firstModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/FirstModels/5
        [HttpGet("find/name/{name}")]
        public async Task<ActionResult<IEnumerable<FirstModel>>> FindName(string name)
        {
            if (_context.FirstModels == null)
            {
                return NotFound();
            }
            var firstModel = await _context.FirstModels.Where(m => m.Name == name).ToListAsync();

            if (firstModel == null)
            {
                return NotFound();
            }

            return firstModel;
        }

        // GET: api/FirstModels/5
        [HttpGet("find/email/{email}")]
        public async Task<ActionResult<IEnumerable<FirstModel>>> FindEmail(string email)
        {
            if (_context.FirstModels == null)
            {
                return NotFound();
            }
            var firstModel = await _context.FirstModels.Where(m => m.Email == email).ToListAsync();
            if (firstModel == null)
            {
                return NotFound();
            }

            return firstModel;
        }

        private bool FirstModelExists(int id)
        {
            return (_context.FirstModels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
