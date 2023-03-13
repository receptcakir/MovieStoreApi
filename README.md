## Movie Db Web API 

- Entity Framework Core – Code First
- MsSql
- Repository Pattern
- Fluent Validation
- Automapper
- JWT Authentication,Refresh Token
- Redis Cache 
- MassTransit + RabbitMQ
- Docker




# Introduction

Net 7, Sql Server, RabbitMQ, Redis ve Docker kullanılarak geliştirilmiş bir API projesidir.

Auth:
Asp net identity kütüphanesi ile JWT Refresh Token mekanızması geliştirilmiştir.


## Endpoint

Movies:
•	Film listesi: Sayfa sayfa tüm filmlerin alınabileceği bir endpoint. Sayfa büyüklüğü parametre olarak alınır.
•	Id ile film görüntüleme: Film bilgileri ile birlikte ortalama puan, kullanıcının verdiği puan ve eklediği notlar gösterilir.
•	Seçilen bir filme not ve puan ekleme: Not text olarak alınır. Puan ise 1-10 arası bir tam sayı olmak zorunda. 
•	Seçilen Film tavsiye etme: Verilen bir e-posta adresine mail atılır.

Auth:
•	UserRegister: Kullanıcı Kayıt için gerekli bilgiler istenir ve yeni kullanıcı oluşturulur.
•	Login: Sistemde kayıtlı kullanıcılar sisteme giriş bilgilerini gönderir. geriye token bilgileri döner. 
•	RefreshTokenLogin: login endpoint i ile refresh token değeri döner ve bu değerle birlikte sisteme login gerektürmeden yeniden giriş yapılır. 


REDIS

Movies  bilgilerini tutmak için kullanılmaktadır. sorgu yapıldığında değerleri ilk önce Redis Cache den getirmeye çalışır, eğer Cache de yoksa Db den çeker ve değeri Cache e Kaydeder.


RabbitMq

Film Önerileri Email yoluyla gönderilir ve Emailler için kuyruk yapısı kullanılmaktadır. Email gönderim isteği sıraya eklenmekte ve daha sonra bir method tarafından bu kuyruk dinlenip gönderim işlemi yapılmaktadır. Mail işlemlerinin Takibi için Başarılı/Başarısız işlemlem logları "wwwroot" altında bir klasöre kaydedilmektedir.


