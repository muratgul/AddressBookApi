using Microsoft.AspNetCore.Http;

namespace AddressBookApi.Entities
{
    public class DosyaUpload
    {
        public IFormFile files { get; set; }
    }
}
