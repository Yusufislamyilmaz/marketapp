// Program.cs
using System;
using MarketLibrary;

namespace MarketConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Market market = new Market();
            market.LoadFromCsv("../../../urunler.csv");

            Console.WriteLine("Market Uygulamasına Hoş Geldiniz!");
            while (true)
            {
                Console.WriteLine("\nMenü:");
                Console.WriteLine("1. Ürün Ekle");
                Console.WriteLine("2. Ürünleri Görüntüle");
                Console.WriteLine("3. Ürün Sat");
                Console.WriteLine("4. Ürün Güncelle");
                Console.WriteLine("5. Çıkış");

                Console.Write("Bir seçenek seçin: ");
                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        UrunEkle(market);
                        break;

                    case "2":
                        UrunleriGoruntule(market);
                        break;

                    case "3":
                        UrunSat(market);
                        break;

                    case "4":
                        UrunGuncelle(market);
                        break;

                    case "5":
                        market.SaveToCsv("../../../urunler.csv");
                        Console.WriteLine("Uygulamadan çıkılıyor. Hoşça kalın!");
                        return;

                    default:
                        Console.WriteLine("Geçersiz seçenek. Lütfen tekrar deneyin.");
                        break;
                }
            }
        }

        static void UrunEkle(Market market)
        {
            Console.WriteLine("\nÜrün Türünü Seçin:");
            Console.WriteLine("1. Temel Gıda");
            Console.WriteLine("2. İçecek");
            Console.WriteLine("3. Atıştırmalık");

            string turSecim;
            while (true)
            {
                turSecim = Console.ReadLine();
                if (turSecim == "1" || turSecim == "2" || turSecim == "3")
                {
                    break;
                }
                Console.WriteLine("Geçersiz ürün türü seçimi. Lütfen tekrar deneyin.");
            }

            Console.Write("Ürün adını girin: ");
            string ad = Console.ReadLine();

            decimal fiyat;
            while (true)
            {
                Console.Write("Ürün fiyatını girin: ");
                if (decimal.TryParse(Console.ReadLine(), out fiyat) && fiyat >= 0)
                {
                    break;
                }
                Console.WriteLine("Geçersiz fiyat girdiniz. Lütfen tekrar deneyin.");
            }

            int stok;
            while (true)
            {
                Console.Write("Ürün stok miktarını girin: ");
                if (int.TryParse(Console.ReadLine(), out stok) && stok >= 0)
                {
                    break;
                }
                Console.WriteLine("Geçersiz stok miktarı girdiniz. Lütfen tekrar deneyin.");
            }

            int barkod;
            while (true)
            {
                Console.Write("Ürün barkod numarasını girin: ");
                if (int.TryParse(Console.ReadLine(), out barkod) && barkod > 0)
                {
                    break;
                }
                Console.WriteLine("Geçersiz barkod numarası girdiniz. Lütfen tekrar deneyin.");
            }

            switch (turSecim)
            {
                case "1":
                    market.UrunEkle(new TemelGida(ad, fiyat, stok, barkod));
                    break;
                case "2":
                    market.UrunEkle(new Icecek(ad, fiyat, stok, barkod));
                    break;
                case "3":
                    market.UrunEkle(new Atistirmalik(ad, fiyat, stok, barkod));
                    break;
            }

            market.SaveToCsv("../../../urunler.csv");
            Console.WriteLine("Ürün başarıyla eklendi.");
        }

        static void UrunleriGoruntule(Market market)
        {
            Console.WriteLine("\nMevcut Ürünler:");
            foreach (var urun in market.UrunleriGetir())
            {
                Console.WriteLine(urun.UrunBilgisi());
            }
        }

        static void UrunSat(Market market)
        {
            Console.WriteLine("\nMevcut Ürünler:");
            UrunleriGoruntule(market);

            int barkod;
            while (true)
            {
                Console.Write("\nSatılacak ürünün barkod numarasını girin: ");
                if (int.TryParse(Console.ReadLine(), out barkod))
                {
                    if (market.UrunleriGetir().Exists(u => u.Barkod == barkod))
                    {
                        break;
                    }
                    Console.WriteLine("Girilen barkod numarası listede bulunamadı. Lütfen tekrar deneyin.");
                }
                else
                {
                    Console.WriteLine("Geçersiz barkod girdiniz. Lütfen tekrar deneyin.");
                }
            }

            while (true)
            {
                Console.Write("Satılacak miktarı girin: ");
                if (int.TryParse(Console.ReadLine(), out int miktar) && miktar > 0)
                {
                    var urun = market.UrunleriGetir().Find(u => u.Barkod == barkod) as Urun;
                    if (urun != null && urun.Stok >= miktar)
                    {
                        if (market.UrunSat(barkod, miktar))
                        {
                            market.SaveToCsv("../../../urunler.csv");
                            Console.WriteLine("Ürün başarıyla satıldı.");
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Yetersiz stok. Mevcut stok: {urun?.Stok ?? 0}. Lütfen tekrar deneyin.");
                    }
                }
                else
                {
                    Console.WriteLine("Geçersiz miktar girdiniz. Lütfen tekrar deneyin.");
                }
            }
        }

        static void UrunGuncelle(Market market)
        {
            UrunleriGoruntule(market);

            Console.Write("\nGüncellenecek ürünün barkod numarasını girin (Çıkmak için '0' girin): ");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int barkod))
                {
                    if (barkod == 0)
                    {
                        Console.WriteLine("Güncelleme işlemi iptal edildi.");
                        return;
                    }

                    if (market.UrunleriGetir().Exists(u => u.Barkod == barkod))
                    {
                        var urun = market.UrunleriGetir().Find(u => u.Barkod == barkod) as Urun;

                        Console.Write("Yeni ürün adını girin: ");
                        while (true)
                        {
                            string yeniAd = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(yeniAd))
                            {
                                urun.Ad = yeniAd;
                                break;
                            }
                            Console.WriteLine("Geçersiz ad. Lütfen tekrar deneyin.");
                        }

                        Console.Write("Yeni ürün fiyatını girin: ");
                        while (true)
                        {
                            if (decimal.TryParse(Console.ReadLine(), out decimal yeniFiyat) && yeniFiyat >= 0)
                            {
                                urun.Fiyat = yeniFiyat;
                                break;
                            }
                            Console.WriteLine("Geçersiz fiyat. Lütfen tekrar deneyin.");
                        }

                        Console.Write("Yeni stok miktarını girin: ");
                        while (true)
                        {
                            if (int.TryParse(Console.ReadLine(), out int yeniStok) && yeniStok >= 0)
                            {
                                urun.Stok = yeniStok;
                                break;
                            }
                            Console.WriteLine("Geçersiz stok miktarı. Lütfen tekrar deneyin.");
                        }

                        market.SaveToCsv("../../../urunler.csv");
                        Console.WriteLine("Ürün başarıyla güncellendi.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Girilen barkod numarası bulunamadı. Lütfen tekrar deneyin.");
                    }
                }
                else
                {
                    Console.WriteLine("Geçersiz barkod girdiniz. Lütfen tekrar deneyin.");
                }
            }
        }
    }
}
