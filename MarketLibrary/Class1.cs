// Class1.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketLibrary
{
    public interface IUrun
    {
        int Barkod { get; }
        string UrunBilgisi();
    }

    public abstract class Urun : IUrun
    {
        private string ad;
        private decimal fiyat;
        private int stok;
        private int barkod;

        public string Ad
        {
            get => ad;
            set => ad = string.IsNullOrWhiteSpace(value) ? "Bilinmiyor" : value;
        }

        public decimal Fiyat
        {
            get => fiyat;
            set => fiyat = value < 0 ? 0 : value;
        }

        public int Stok
        {
            get => stok;
            set => stok = value < 0 ? 0 : value;
        }

        public int Barkod
        {
            get => barkod;
            set => barkod = value <= 0 ? new Random().Next(100000, 999999) : value;
        }

        public Urun(string ad, decimal fiyat, int stok, int barkod)
        {
            Ad = ad;
            Fiyat = fiyat;
            Stok = stok;
            Barkod = barkod;
        }

        public abstract string UrunBilgisi();
    }

    public class TemelGida : Urun
    {
        public TemelGida(string ad, decimal fiyat, int stok, int barkod) : base(ad, fiyat, stok, barkod) { }

        public override string UrunBilgisi()
        {
            return $"[Temel Gıda] Barkod: {Barkod}, Ürün: {Ad}, Fiyat: {Fiyat} TL, Stok: {Stok} Adet";
        }
    }

    public class Icecek : Urun
    {
        public Icecek(string ad, decimal fiyat, int stok, int barkod) : base(ad, fiyat, stok, barkod) { }

        public override string UrunBilgisi()
        {
            return $"[İçecek] Barkod: {Barkod}, Ürün: {Ad}, Fiyat: {Fiyat} TL, Stok: {Stok} Adet";
        }
    }

    public class Atistirmalik : Urun
    {
        public Atistirmalik(string ad, decimal fiyat, int stok, int barkod) : base(ad, fiyat, stok, barkod) { }

        public override string UrunBilgisi()
        {
            return $"[Atıştırmalık] Barkod: {Barkod}, Ürün: {Ad}, Fiyat: {Fiyat} TL, Stok: {Stok} Adet";
        }
    }

    public class Market
    {
        private List<IUrun> urunler;

        public Market()
        {
            urunler = new List<IUrun>();
        }

        public void UrunEkle(IUrun urun)
        {
            urunler.Add(urun);
        }

        public List<IUrun> UrunleriGetir()
        {
            return urunler;
        }

        public bool UrunSat(int barkod, int miktar)
        {
            var urun = urunler.FirstOrDefault(u => u.Barkod == barkod);
            if (urun is Urun urunDetay && urunDetay.Stok >= miktar)
            {
                urunDetay.Stok -= miktar;
                return true;
            }
            return false;
        }

        public bool UrunGuncelle(int barkod)
        {
            var urun = urunler.FirstOrDefault(u => u.Barkod == barkod) as Urun;
            if (urun != null)
            {
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

                return true;
            }
            return false;
        }

        public void SaveToCsv(string filePath)
        {
            var lines = new List<string> { "Tür,Ad,Fiyat,Stok,Barkod" };
            foreach (var urun in urunler)
            {
                if (urun is TemelGida gida)
                {
                    lines.Add($"TemelGıda,{gida.Ad},{gida.Fiyat},{gida.Stok},{gida.Barkod}");
                }
                else if (urun is Icecek icecek)
                {
                    lines.Add($"İçecek,{icecek.Ad},{icecek.Fiyat},{icecek.Stok},{icecek.Barkod}");
                }
                else if (urun is Atistirmalik atistirmalik)
                {
                    lines.Add($"Atıştırmalık,{atistirmalik.Ad},{atistirmalik.Fiyat},{atistirmalik.Stok},{atistirmalik.Barkod}");
                }
            }
            System.IO.File.WriteAllLines(filePath, lines);
        }

        public void LoadFromCsv(string filePath)
        {
            if (!System.IO.File.Exists(filePath)) return;

            var lines = System.IO.File.ReadAllLines(filePath);
            foreach (var line in lines.Skip(1)) // İlk satır başlık
            {
                var parts = line.Split(',');
                var tur = parts[0];
                var ad = parts[1];
                var fiyat = decimal.Parse(parts[2]);
                var stok = int.Parse(parts[3]);
                var barkod = int.Parse(parts[4]);

                if (tur == "TemelGıda")
                {
                    UrunEkle(new TemelGida(ad, fiyat, stok, barkod));
                }
                else if (tur == "İçecek")
                {
                    UrunEkle(new Icecek(ad, fiyat, stok, barkod));
                }
                else if (tur == "Atıştırmalık")
                {
                    UrunEkle(new Atistirmalik(ad, fiyat, stok, barkod));
                }
            }
        }
    }
}
