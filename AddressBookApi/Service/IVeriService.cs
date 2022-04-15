using AddressBookApi.Entities;
using AddressBookApi.Helpers;

namespace AddressBookApi.Service
{
    public interface IVeriService
    {
        Task< PagedList<Veri>> GetVeriler(VeriParameters veriParameters);
        Task<Veri> GetById(int id);
        void Add(Veri veri);
        string Delete(int id);
        void Update(Veri veri);

    }
}
