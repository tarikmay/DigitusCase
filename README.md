
# DigitusCase-Study
API - Kullanıcı kayıt/giriş/onay mekanizması ve admin raporu için örnek uçlar yazıldı.


## Örnek Kullanıcı
Yeni bir kullanıcı için kaydolma,kayıt onayı,giriş yapma(token alma),unutulan şifreyi değiştirme.
 Admin yetkisi için çevrimiçi kullanıcılar/kayıtlı kullanıcılar/gün bazlı kayıtlı kullanıcılar/Kayıt olduktan x gün kadar sürede kaydını onaylamayan kullanıcılar/x tarihte gelen requestler ve request tamamlanma süreleri(ms) 
#### Yeni Kullanıcı Kaydı

```http
  POST /api/User/Register
```
Bu API ucu aşağıdaki parametreler ile yeni kullanıcı kaydededer.Fakat giriş yapamaz.
| Parametre | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `username` | `string` |  |
| `password` | `string` |  |
| `email` | `string` |Onay kodu için mail gidecek   |
| `name` | `string` |  |
| `string` | `string` |  |



#### Kullanıcı Mail Onayı

```http
  POST ​/api​/User​/Confirm​/{key}
```
Bu API ucu aşağıdaki parametreler ile yeni kaydolan kullanıcının hesabını onaylar.
Onay kodu mail adresine gelen koddur.
| Parametre | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `key` | `string` | Mail adresine gelen kod |


#### Kullanıcı Girişi(Token alma)

```http
  POST ​/api/User/Login
```
Bu API ucu aşağıdaki parametreler ile giriş yapmanızı sağlar.Size istek yetkisi için bir token döner.
| Parametre | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `username` | `string` |  |
| `password` | `string` |  |


#### Şifremi Unuttum

```http
  POST ​​/api​/User​/ForgotPassword
```
Bu API ucu aşağıdaki parametreler ile yeni şifre oluşturabilmeniz için gerekli kodu mail adresinize gönderir.
**NOT:** Mail adresine gönderilen kodun geçerlilik süresi 60 saniye olarak ayarlanmıştır.
| Parametre | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `username` | `string` |  |
| `email` | `string` |  |




#### Yeni Şifre Talebi

```http
  POST /api/User/NewPassword
```
Bu API ucu aşağıdaki parametreler ile yeni şifre oluşturur.

| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `username` | `string` |  |
| `newPassword` | `string` | Oluşturmak istenilen yeni şifre |
| `passwordCode` | `string` | Mail adresine gönderilen şifre sıfırlama kodu |


#### Kullanıcının Kendi Bilgileri

```http
  GET /api/User/GetUser
```
Bu API ucu aşağıdaki parametreler ile giriş yapan kullanıcının kendi bilgilerini getirir.

#### HEADER
| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `Authorization` | `string` |Login işleminden dönen token (Bearer akjsbkajbhfkhabfahb...) |



#### Kayıtlı Tüm Kullanıcılar - Admin

```http
  GET /api/User/GetUser
```
Bu API ucu aşağıdaki parametreler ile kayıtlı tüm kullanıcıları getirir.

#### HEADER
| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `Authorization` | `string` |Login işleminden dönen token (Bearer akjsbkajbhfkhabfahb...) |


  #### Belirli Kullanıcı Getir - Admin

```http
  GET ​/api​/User​/{id}
```
Bu API ucu aşağıdaki parametreler ile gönderilen id için kullanıcıyı getirir getirir.

#### HEADER
| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `Authorization` | `string` |Login işleminden dönen token (Bearer akjsbkajbhfkhabfahb...) |

#### BODY
| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `id` | `string` |Bilgisi istenen kullanıcı id |



#### Çevrimiçi Kullanıcıları Getir - Admin

```http
  GET ​/api​/User​/OnlineUser
```
Bu API ucu aşağıdaki parametreler ile çevrimiçi olan kullanıcıları getirir.

#### HEADER
| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `Authorization` | `string` |Login işleminden dönen token (Bearer akjsbkajbhfkhabfahb...) |

#### Onaylı Tüm Kullanıcıları Getir - Admin

```http
  GET ​/api/User/RegisteredUsers
```
Bu API ucu aşağıdaki parametreler ile kaydı onaylayan tüm kullanıcıları getirir.

#### HEADER
| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `Authorization` | `string` |Login işleminden dönen token (Bearer akjsbkajbhfkhabfahb...) |


#### Son x Günde Hesabını Onaylayan Kullanıcıları Getir - Admin

```http
  GET ​​/api​/User​/RegisteredUsersByDay
```
Bu API ucu aşağıdaki parametreler ile son x gün içinde kaydı onaylayan tüm kullanıcıları getirir.

#### HEADER
| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `Authorization` | `string` |Login işleminden dönen token (Bearer akjsbkajbhfkhabfahb...) |

#### BODY
| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `day` | `int` |örneğin son 5 gün içinde hesabını onaylayan kullanıcılar |


#### Kayıt Oluduktan Sonra x Gün İçinde Hesabını Onaylamayan Kullanıcıları Getir - Admin

```http
  GET ​​​/api​/User​/GetNotConfirmedUsersByDay
```
Bu API ucu aşağıdaki parametreler ile kayıt olduktan sonra x gün içinde kaydı tamamlamayan tüm kullanıcıları getirir.

#### HEADER
| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `Authorization` | `string` |Login işleminden dönen token (Bearer akjsbkajbhfkhabfahb...) |

#### BODY
| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `day` | `int` |örneğin kaydolduktan sonra 5 gün içinde hesabını onaylayan kullanıcılar |

#### Belirli Tarihte Atılan Request ve Sürelerini Getir - Admin

```http
  GET ​​​/api/User/GetUsersRequestCompleteTimeByDate
```
Bu API ucu aşağıdaki parametreler ile gg.mm.yyyy tarihinde atılan tüm istekleri ve isteklerin dönüş sürelerini ms cinsinden  getirir.

#### HEADER
| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `Authorization` | `string` |Login işleminden dönen token (Bearer akjsbkajbhfkhabfahb...) |

#### BODY
| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `date` | `string` |Tarih formatı: ***gg.mm.yyyy*** şeklinde olmalıdır.|


#### Online Client Sayısını Getir - Admin

```http
  GET ​​​/api/User/GetOnlineCount
```
Bu API ucu aşağıdaki parametreler ile online CLIENT sayısını verir.

**NOT:** Client bağlantısı gereklidir.

#### HEADER
| Parametre | Tip     | Açıklama                       |
| :-------- | :------- | :-------------------------------- |
| `Authorization` | `string` |Login işleminden dönen token (Bearer akjsbkajbhfkhabfahb...) |


## Kullanılan Teknoloji ve Kütüphaneler
**Framework:** NET Core 6.0 ile ASPNET Web Api

**Database:** MongoDB

**Dokümantasyon:** Swagger

**Mapper:** AutoMapper 

**Auth:** JWTBearer

**WebSocket:** SignalR


## NOT
**-** Bu projede cache,attribute,claim,middleware örnekleri bulunmaktadır.

**-** Proje için db uzak sunucu bilgileri ve smtp ayarları üzerindedir.

**-** Bu proje için geçen süre 3 gündür.

