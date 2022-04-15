using AddressBookApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace AddressBookApi.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Veri> Veriler { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
    }
}
