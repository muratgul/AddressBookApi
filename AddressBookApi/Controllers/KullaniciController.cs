using AddressBookApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AddressBookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class KullaniciController : ControllerBase
    {
        [HttpGet("Admins")]
        public IActionResult AdminsEntpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Kullanıcı: {currentUser.Ad}, Rolü {currentUser.Grubu}");
        }

        private Kullanici GetCurrentUser()
        {
            var identiy = HttpContext.User.Identity as ClaimsIdentity;

            if (identiy != null)
            {
                var userClaims = identiy.Claims;

                return new Kullanici
                {
                    KullaniciAdi = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Ad = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
                    Soyad = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                    Grubu = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value,
                };
            }

            return null;            
        }
    }
}
