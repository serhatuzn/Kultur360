namespace Kultur360.Models
{
    public class TarihiYer
    {
        public int Id { get; set; }
        public string Isim { get; set; } = "";
        public string Aciklama { get; set; } = "";
        public string Sehir { get; set; } = "";
        public DateTime Tarih { get; set; }
        public string FotografUrl { get; set; } = "";
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string Kategori { get; set; } = "";
    }
}
