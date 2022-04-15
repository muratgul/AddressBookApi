using AddressBookApi.DataAccess;
using AddressBookApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AddressBookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class KategorilerController : ControllerBase
    {
        private readonly DataContext _context;

        public KategorilerController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Kategori>>> Get()
        {
            var result = await _context.Kategoriler.ToListAsync();

            if(result.Count == 0) return NotFound("Data not found");

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Kategori>> Get(int id)
        {
            var result = await _context.Kategoriler.FindAsync(id);

            if (result == null) return NotFound("Data not found");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Kategori>>> Add(Kategori veri)
        {
            _context.Kategoriler.Add(veri);
            await _context.SaveChangesAsync();
            var result = await _context.Kategoriler.ToListAsync();
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<Kategori>> Update(Kategori data)
        {
            var veri = await _context.Kategoriler.FindAsync(data.Id);
            if (veri == null) return NotFound("Data not found");

            veri.KategoriAdi = data.KategoriAdi;

            await _context.SaveChangesAsync();

            var result = await _context.Kategoriler.ToListAsync();

            return Ok(result);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Kategori>> Delete(int id)
        {
            var veri = await _context.Kategoriler.FindAsync(id);
            if (veri == null) return NotFound("Data not found");

            _context.Kategoriler.Remove(veri);
            await _context.SaveChangesAsync();

            var result = await _context.Kategoriler.ToListAsync();

            return Ok(result);
        }
    }
}
