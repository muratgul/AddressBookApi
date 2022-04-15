using AddressBookApi.DataAccess;
using AddressBookApi.Entities;
using AddressBookApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AddressBookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VerilerController : ControllerBase
    {
        IVeriService _veriService = null; 

        private readonly DataContext _context;

        public VerilerController(IVeriService veriService)
        {
            _veriService = veriService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Veri>>> Get([FromQuery] VeriParameters veriParameters)
        {
            var result = await _veriService.GetVeriler(veriParameters);

            var metadata = new
            {
                Success = true,
                result.TotalCount,
                result.PageSize,
                result.PageNumber,
                result.HasNext,
                result.HasPrevious,
                data = result
            };

            return Ok(metadata);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Veri>> Get(int id)
        {

            var result = await _veriService.GetById(id);
            if(result == null) return NotFound("Data not found");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Veri>>> Add(Veri veri)
        {
            _veriService.Add(veri);
            return Ok("Data is added");
        }

        [HttpPut]
        public async Task<ActionResult<Veri>> Update(Veri data)
        {

            var veri = await _veriService.GetById(data.Id);
            if (veri == null) return NotFound("Data not found");

            //veri.KategoriId = data.KategoriId;
            //veri.Firma = data.Firma;
            //veri.Ad = data.Ad;
            //veri.Soyad = data.Soyad;
            //veri.Adres1 = data.Adres1;
            //veri.Adres2 = data.Adres2;
            //veri.Email1 = data.Email1;
            //veri.Email2 = data.Email2;
            //veri.Ulke = data.Ulke;
            //veri.Ilce = data.Ilce;
            //veri.Sehir = data.Sehir;
            //veri.Gsm1 = data.Gsm1;
            //veri.Gsm2 = data.Gsm2;
            //veri.Web = data.Web;

            _veriService.Update(data);
            return Ok(veri);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Veri>> Delete(int id)
        {

            var veri = _veriService.GetById(id);
            

            if (veri == null) 
            {
                return NotFound("Data not found");
            } 

            _veriService.Delete(id);

            return Ok("Data is deleted");
        }
    }
}
