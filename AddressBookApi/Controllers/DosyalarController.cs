using AddressBookApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AddressBookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DosyalarController : ControllerBase
    {
        public static IWebHostEnvironment _envoironment;

        public DosyalarController(IWebHostEnvironment envoironment)
        {
            _envoironment = envoironment;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post([FromForm] DosyaUpload objFile)
        {
            try
            {
                if (objFile.files.Length > 0)
                {
                    if (!Directory.Exists(_envoironment.WebRootPath + "\\Uploads\\"))
                    {
                        Directory.CreateDirectory(_envoironment.WebRootPath + "\\Uploads\\");
                    }

                    var newFileName = DateTimeOffset.Now.ToUnixTimeSeconds() + "_" + objFile.files.FileName;

                    using (FileStream fileStream = System.IO.File.Create(_envoironment.WebRootPath + "\\Uploads\\" + newFileName))
                    {
                        objFile.files.CopyTo(fileStream);
                        fileStream.Flush();
                        return Ok("File is uploaded");
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
        }
    }
}
