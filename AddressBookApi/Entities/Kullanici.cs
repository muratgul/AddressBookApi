namespace AddressBookApi.Entities
{
    public class Kullanici
    {
        public int Id { get; set; }
        public string KullaniciAdi { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Grubu { get; set; }
        public string Parola { get; set; }
        public bool Aktif { get; set; }


    }
}
