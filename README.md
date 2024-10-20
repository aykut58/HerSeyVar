# Proje Adı 
Projemin adı HerSeyVar. Basit düzeyde bir e ticaret sitesidir.

## Giriş
Bu projede kategori ve ürünler olmak üzere 2 adet tablo yer almaktadır. Bu tablolar arasında bire çok ilişkisi kurarak hazırlanmıştır. Kullanıcının kategori ekleyip bu kategori içirisinde ürünler ekleyip bu ürünleri görüntüleyebildiği bir çalışma mantığı vardır.

## Kurulum
Projemde Asp.Net kullandım. (versiyon 8.0.101)
Veritabanı olarak MSSQL kullandım.

Git kuru bir bilgisayar ile komut satırından projeyi indirmek istediğiniz dosya dizinne gelerek git clone https://github.com/aykut58/HerSeyVar komutu ile projeyi imdirebilirsiniz.

Git kurulu değilse zip olarak indirebilirsiniz.

Proje dosyasın içerisinde yer alan appsettings.json dosyasında "ConnectionStrings" yer almaktadır. Bu ifade Asp.Net uygulamasının SQL Server ile nasıl iletişim kuaracağını hangi veritabanı kullanacağını ifade eder o yüzden bu değerini kendiinze göre değiştirmeniz gerekmetedir.

Projede kullanılan paketlerin doğru bir şekilde çalışması adına terminal üzerinde dotnet restore komutunu çalıştırabilirsiniz.

Sonrasında dotnet ef database update komutu ile projenizin veritabanı bağlantısını tamamlayabilirsiniz.

dotnet watch run komutu ile projenizi çalıştırabilirsiniz.
