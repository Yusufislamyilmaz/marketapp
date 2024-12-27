using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketLibrary
{
    // IUrun arayüzü (Dependency Inversion için)
    public interface IUrun
    {
        int Barkod { get; }
        string UrunBilgisi();
    }

    // Soyut Ürün sınıfı (Abstraction için)
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
            set => barkod = value <= 0 ? new Random().Next(100000, 999999) : value; // Barkod sıfır veya negatif olamaz, rastgele bir değer atandı
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

    // Türetilmiş sınıflar
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

            // Başlangıçta ürünler ekliyoruz
            UrunEkle(new TemelGida("Ekmek", 7.5m, 20, 0101));
            UrunEkle(new TemelGida("Yumurta 15'li", 90.0m, 15, 0102));

            UrunEkle(new Icecek("Su", 5m, 100, 0201));
            UrunEkle(new Icecek("Limonata", 18.5m, 50, 0202));

            UrunEkle(new Atistirmalik("Cips", 27.5m, 40, 0301));
            UrunEkle(new Atistirmalik("Çikolata", 12.5m, 75, 0302));
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
    }
}
