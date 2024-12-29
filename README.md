MARKET UYGULAMASI PROJE DÖKÜMANI

YUSUF İSLAM YILMAZ - 220306019


Bu doküman, bizim Market Uygulaması'nın nasıl çalıştığını ve neler yaptığını anlatıyor. Uygulama C# dilinde yazıldı ve iki bölümden oluşuyor:
1)	MarketConsoleApp: Kullanıcının konsoldan işlemler yapabildiği ana bölüm.

2)	MarketLibrary: Uygulamanın arka planda çalışan kısımları, yani iş mantığı ve sınıflar.

I. UYGULAMANIN ÖZELLİKLERİ

1.	Ürün Ekleme
Kullanıcı, markete şu türde ürünler ekleyebiliyor:

•	Temel Gıda

•	İçecek

•	Atıştırmalık

Her ürün için aşağıdaki bilgiler isteniyor:

•	Ürün Adı

•	Fiyat

•	Stok Miktarı

•	Barkod Numarası (Benzersiz olmalı)

Ürün Ekleme Özellikleri

•	Girilen bilgiler anında kontrol edilir ve hatalı bir girdi olduğunda kullanıcıdan doğru değer girmesi isteniyor.

•	Girilen veriler urunler.csv dosyasına kaydoluyor. Her işlemden sonra dosya kaydediliyor. Program tekrar başlatıldığında kayıtlı veriler tekrar yükleniyor.


2.	Ürünleri Görüntüleme

Marketin içindeki tüm ürünler liste halinde görüntüleniyor. Listeleme sırasında şunlar gösteriliyor:

•	Türü (örneğin, Temel Gıda, İçecek)

•	Barkod Numarası

•	Adı

•	Fiyatı

•	Stok Miktarı


3.	Ürün Satışı


•	Kullanıcı, satmak istediği ürünün barkod numarasını ve miktarını giriyor.

•	Eğer stok yeterliyse satış başarılı oluyor ve stok düşüyor.

•	Yetersiz stok veya yanlış barkod numarası girildiğinde hata mesajı veriliyor ve kullanıcıdan tekrar doğru veri girmesi isteniyor.


4.	Ürün Güncelleme


•	Kullanıcı, güncellemek istediği ürünün barkod numarasını giriyor.

•	Eğer ki kullanıcı vazgeçerse 0’a basıp ana menüye geri dönebiliyor.

•	Ürünün ismi, fiyatı ve stok miktarı tekrar girilerek bilgiler güncellenebiliyor.

•	Yanlış veri girildiğinde hata mesajı veriliyor ve kullanıcıdan tekrar doğru veri girmesi isteniyor.


5.	Çıkış

Kullanıcı istediği zaman uygulamadan çıkış yapabiliyor ve veriler urunler.csv dosyasına kaydoluyor.



II. OOP (NESNE TABANLI PROGRAMLAMA) PRENSİPLERİ

1.	Encapsulation (Kapsüllüme)

•	Ürün sınıfındaki özellikler (Ad, Fiyat, Stok, Barkod) private olarak tanımlandı.

•	Bu özelliklere sadece getter ve setter metodlarıyla erişiliyor. Böylece yanlış veya geçersiz veri girişi engelleniyor (örneğin, negatif fiyat).


2.	Abstraction (Soyutlama)

•	Urun sınıfı, diğer ürün türlerinin ortak özelliklerini barındıran bir "şablon" gibi çalışıyor.

•	Urun sınıfına UrunBilgisi adında bir soyut metot eklendi. Bu metot, türetilen sınıflar tarafından kendi ihtiyaçlarına göre dolduruluyor.



3.	Polymorphism (Çok Biçimlilik)

•	UrunBilgisi metodu sayesinde, her ürün türü (örneğin, Temel Gıda, İçecek) kendine özel bilgileri farklı şekilde gösterebiliyor.


4.	Inheritance (Kalıtım)

•	TemelGida, Icecek, Atistirmalik sınıfları, Urun sınıfından türetilmiştir. Böylece bu sınıflar Urun sınıfındaki özellikleri ve metodları kullanabiliyor.



III. DEPENDENCY INVERSİON PRENSİPLERİ	

•	IUrun adında bir arayüz (interface) eklendi. Tüm ürün sınıfları bu arayüzü kullanıyor.

•	Market sınıfı, doğrudan Urun sınıfına bağlı değil. Bunun yerine IUrun arayüzüne bağlı. Bu sayede yeni bir ürün türü eklemek gerektiğinde, Market sınıfını değiştirmemize gerek kalmıyor.



IV. UYGULAMA NASIL ÇALIŞIYOR

1.	Uygulama başladığında bir menü ekrana geliyor.


2.	Kullanıcı menüden şu işlemlerden birini seçebiliyor:

  	1	Ürün Ekle

  	2	Ürünleri Görüntüle

  	3	Ürün Sat

  	4	Ürün Güncelle

  	5	Çıkış


4.	Kullanıcı ürün eklemek isterse, ürün türünü ve özelliklerini seçiyor. Bu ürün markete ekleniyor.


5.	Kullanıcı ürünleri görüntülemek isterse, marketteki tüm ürünlerin bilgileri listeleniyor.


6.	Kullanıcı ürün satışı yapmak isterse, barkod numarasını ve satılacak miktarı giriyor. Eğer her şey uygunsa satış yapılıyor.


7.	Kullanıcı ürün bilgilerini güncellemek isterse barkod numarasını girerek istediği güncellemeleri yapabiliyor. Eğer ki hata ile bu menüye girilmişse 0’a basarak ana menüye geri dönebiliyor.


8.	Kullanıcı çıkış seçeneğini seçtiğinde veriler kaydediliyor ve program kapanıyor.



V. SONUÇ

Bu program OOP prensipleri ve Dependency Inversiont prensiplerine uyularak yazılan, basit bir market yönetimi uygulaması. Uygulama hem düzenli hem de ileride kolayca geliştirilebilecek şekilde yazıldı. 


