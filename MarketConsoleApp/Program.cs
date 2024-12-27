using System;
using MarketLibrary;

namespace MarketConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Market market = new Market();

            Console.WriteLine("Market Uygulamasına Hoş Geldiniz!");
            while (true)
            {
                Console.WriteLine("\nMenü:");
                Console.WriteLine("1. Ürün Ekle");
                Console.WriteLine("2. Ürünleri Görüntüle");
                Console.WriteLine("3. Ürün Sat");
                Console.WriteLine("4. Çıkış");

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
            string turSecim = Console.ReadLine();

            Console.Write("Ürün adını girin: ");
            string ad = Console.ReadLine();

            Console.Write("Ürün fiyatını girin: ");
            decimal fiyat = decimal.Parse(Console.ReadLine());

            Console.Write("Ürün stok miktarını girin: ");
            int stok = int.Parse(Console.ReadLine());

            Console.Write("Ürün barkod numarasını girin: ");
            int barkod = int.Parse(Console.ReadLine());

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
                default:
                    Console.WriteLine("Geçersiz ürün türü seçimi.");
                    break;
            }

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

            Console.Write("\nSatılacak ürünün barkod numarasını girin: ");
            int barkod = int.Parse(Console.ReadLine());

            Console.Write("Satılacak miktarı girin: ");
            int miktar = int.Parse(Console.ReadLine());

            if (market.UrunSat(barkod, miktar))
            {
                Console.WriteLine("Ürün başarıyla satıldı.");
            }
            else
            {
                Console.WriteLine("Ürün satılamadı. Stok veya barkod numarasını kontrol edin.");
            }
        }
    }
}
