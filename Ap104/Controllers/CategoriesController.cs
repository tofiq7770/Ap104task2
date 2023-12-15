using Ap104.DAL;
using Ap104.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ap104.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _db;
        public CategoriesController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {
            List<Category> categories = await _db.Categories.Skip((page - 1) * take).Take(take).ToListAsync();

            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            Category category = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category is null) return StatusCode(StatusCodes.Status404NotFound);

            return StatusCode(StatusCodes.Status200OK, category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            await _db.Categories.AddAsync(category);

            await _db.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, category);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, string name)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            Category existed = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (existed is null) return StatusCode(StatusCodes.Status404NotFound);

            existed.Name = name;
            await _db.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            Category existed = await _db.Categories.FirstAsync(x => x.Id == id);

            if (existed is null) return StatusCode(StatusCodes.Status404NotFound);

            _db.Categories.Remove(existed);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
