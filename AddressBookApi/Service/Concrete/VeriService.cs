using AddressBookApi.DataAccess;
using AddressBookApi.Entities;
using AddressBookApi.Helpers;
using Microsoft.EntityFrameworkCore;

namespace AddressBookApi.Service.Concrete
{
    public class VeriService : IVeriService
    {
        private readonly DataContext _context;

        public VeriService(DataContext context)
        {
            _context = context;
        }
        public void Add(Veri veri)
        {
            _context.Veriler.Add(veri);
            _context.SaveChanges();
        }

        public string Delete(int id)
        {
            var veri = _context.Veriler.FirstOrDefault(x => x.Id == id);
            if(veri != null)
            {
                _context.Veriler.Remove(veri);
                _context.SaveChanges();
            }

            return "Deleted";
        }

        public async Task<Veri> GetById(int id)
        {
            return await _context.Veriler.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PagedList<Veri>> GetVeriler(VeriParameters veriParameters)
        {
            var veri = await _context.Veriler.ToListAsync();

            return PagedList<Veri>.ToPagedList(veri.ToList(), veriParameters.PageNumber, veriParameters.PageSize);
        }

        public void Update(Veri veri)
        {
            _context.Veriler.Update(veri);
            _context.SaveChanges();
        }
    }
}
