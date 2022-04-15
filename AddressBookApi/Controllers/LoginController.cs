using AddressBookApi.DataAccess;
using AddressBookApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AddressBookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private DataContext _context;

        public LoginController(IConfiguration config, DataContext context)
        {
            _config = config;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] KullaniciGiris userLogin)
        {
            var user = Authenticate(userLogin);
            var kullanici = _context.Kullanicilar.FirstOrDefault(o => o.KullaniciAdi.ToLower() == userLogin.KullaniciAdi.ToLower() && o.Parola == userLogin.Parola);

            if (user != null)
            {
                var token = Generate(user);

                var datas = new
                {
                   userid = user.Id,
                   userName = user.KullaniciAdi,
                   userFirstName = user.Ad,
                   userLastName = user.Soyad,
                   userToken = token
                };


                return Ok(datas);
            }

            return NotFound("User not found");
        }

        private string Generate(Kullanici kullanici)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credantials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, kullanici.KullaniciAdi),
                new Claim(ClaimTypes.GivenName, kullanici.Ad),
                new Claim(ClaimTypes.Surname, kullanici.Soyad),
                new Claim(ClaimTypes.Role, kullanici.Grubu),

            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credantials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Kullanici Authenticate(KullaniciGiris kullaniciGiris)
        {
            var kullanici = _context.Kullanicilar.FirstOrDefault(o=>o.KullaniciAdi.ToLower() == kullaniciGiris.KullaniciAdi.ToLower() && o.Parola == kullaniciGiris.Parola);

            if(kullanici != null)
            {
                return kullanici;
            }

            return null;
        }
    }
}
