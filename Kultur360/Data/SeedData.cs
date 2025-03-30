using Kultur360.Models;
using Microsoft.EntityFrameworkCore;

namespace Kultur360.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Kultur360DbContext(
                serviceProvider.GetRequiredService<DbContextOptions<Kultur360DbContext>>()))
            {
                Console.WriteLine("🌱 Seed işlemi başladı...");

                if (context.TarihiYerler.Any())
                {
                    Console.WriteLine("⚠️ Mevcut veriler siliniyor...");
                    context.TarihiYerler.RemoveRange(context.TarihiYerler);
                    context.SaveChanges();
                }

                Console.WriteLine("📦 Yeni seed verileri ekleniyor...");

                context.TarihiYerler.AddRange(
                    new TarihiYer
                    {
                        Isim = "Topkapı Sarayı",
                        Aciklama = "Osmanlı döneminde padişahların yaşadığı, boğaz manzaralı saray.",
                        Sehir = "İstanbul",
                        Tarih = new DateTime(1478, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                        FotografUrl = "/images/topkapi-sarayi.jpg",
                        Latitude = 41.013611,
                        Longitude = 28.984167,
                        Kategori = "Tarihi Yer"
                    },
                    new TarihiYer
                    {
                        Isim = "Ayasofya",
                        Aciklama = "Bizans ve Osmanlı dönemlerinin simge yapısı, cami ve müze olarak kullanılmıştır.",
                        Sehir = "İstanbul",
                        Tarih = new DateTime(537, 12, 27, 0, 0, 0, DateTimeKind.Utc),
                        FotografUrl = "/images/ayasofya.jpg",
                        Latitude = 41.0086,
                        Longitude = 28.9802,
                        Kategori = "Tarihi Yer"
                    },
                    new TarihiYer
                    {
                        Isim = "Nusr-Et Steakhouse",
                        Aciklama = "Salt Bae ile dünyaca ün kazanmış et restoranı.",
                        Sehir = "İstanbul",
                        Tarih = DateTime.UtcNow,
                        FotografUrl = "/images/nusret.jpg",
                        Latitude = 41.0551,
                        Longitude = 29.0235,
                        Kategori = "Restoran"
                    },
                    new TarihiYer
                    {
                        Isim = "Kebapçı Halil Usta",
                        Aciklama = "Gaziantep mutfağının vazgeçilmezlerinden, kömür ateşinde kebaplar.",
                        Sehir = "Gaziantep",
                        Tarih = DateTime.UtcNow,
                        FotografUrl = "/images/halilusta.jpg",
                        Latitude = 37.062,
                        Longitude = 37.379,
                        Kategori = "Restoran"
                    },
                    new TarihiYer
                    {
                        Isim = "Rahmi M. Koç Müzesi",
                        Aciklama = "Ulaşım, endüstri ve iletişim temalı interaktif müze.",
                        Sehir = "İstanbul",
                        Tarih = new DateTime(1994, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                        FotografUrl = "/images/kocmuzesi.jpg",
                        Latitude = 41.0438,
                        Longitude = 28.9421,
                        Kategori = "Müze"
                    },
                    new TarihiYer
                    {
                        Isim = "Mevlana Müzesi",
                        Aciklama = "Mevlana Celaleddin Rumi'nin türbesi ve sema kültürünün merkezi.",
                        Sehir = "Konya",
                        Tarih = new DateTime(1273, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                        FotografUrl = "/images/mevlana.jpg",
                        Latitude = 37.8715,
                        Longitude = 32.5042,
                        Kategori = "Müze"
                    },
                    new TarihiYer
                    {
                        Isim = "Mado Kafe",
                        Aciklama = "Kahve, tatlı ve sohbetin merkezi olan nostaljik bir kafe.",
                        Sehir = "Ankara",
                        Tarih = DateTime.UtcNow,
                        FotografUrl = "/images/mado.jpg",
                        Latitude = 39.9205,
                        Longitude = 32.8541,
                        Kategori = "Kafe"
                    },
                    new TarihiYer
                    {
                        Isim = "Atatürk Orman Çiftliği Parkı",
                        Aciklama = "Doğayla iç içe vakit geçirebileceğiniz büyük yeşil alan.",
                        Sehir = "Ankara",
                        Tarih = new DateTime(1925, 4, 1, 0, 0, 0, DateTimeKind.Utc),
                        FotografUrl = "/images/aoc.jpg",
                        Latitude = 39.9526,
                        Longitude = 32.8003,
                        Kategori = "Park"
                    },
                    new TarihiYer
                    {
                        Isim = "Arter Sanat Galerisi",
                        Aciklama = "Modern ve çağdaş sanat sergilerine ev sahipliği yapan mekan.",
                        Sehir = "İstanbul",
                        Tarih = new DateTime(2010, 5, 8, 0, 0, 0, DateTimeKind.Utc),
                        FotografUrl = "/images/arter.jpg",
                        Latitude = 41.0323,
                        Longitude = 28.9775,
                        Kategori = "Sanat Galerisi"
                    }
                );

                try
                {
                    context.SaveChanges();
                    Console.WriteLine("✅ Seed verileri başarıyla kaydedildi.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ SEED VERİ KAYDETME HATASI: " + ex.Message);
                }
            }
        }
    }
}
