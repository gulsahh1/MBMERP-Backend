# MBM ERP Backend

MBM(Mini-Business-Management) ERP sistemi için geliştirilmiş backend projesidir.  
.NET 8 tabanlı, katmanlı mimari ile geliştirilmiş olup REST API + real-time (SignalR) + JWT authentication destekler.

---
## ⚠️ Proje Durumu & Gelecek Güncellemeler

Bu proje aktif olarak geliştirilmeye devam etmektedir.

Mevcut sürüm **Versiyon 1 (MVP)** olup, yalnızca temel işlevleri içermektedir.  
Sistem, ilerleyen süreçte kapsamlı güncellemeler alacaktır.

### 🚀 Versiyon 2’de Planlananlar
- Yapay zekâ (AI) entegrasyonu  
- Akıllı öneri ve otomasyon sistemleri  
- Performans ve mimari iyileştirmeler  
- UI/UX geliştirmeleri  
- Yeni modül ve özellik eklemeleri  

### 🧠 Not
Versiyon 2 ile birlikte projeye **AI destekli özellikler** eklenecektir. Bu özellikler mevcut sürümde bulunmamaktadır.

## 🚀 Kullanılan Teknolojiler

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- SQL Server
- Repository Pattern
- Dependency Injection (Built-in .NET DI)
- RESTful API
- SignalR (Real-time communication)
- JWT Authentication (Token-based security)
- FluentValidation (Request validation)
- Swagger (API documentation)
- CORS Policy

---

## 🧱 Mimari Yapı

Proje katmanlı mimari prensiplerine göre geliştirilmiştir:

- **API Layer** → Controller katmanı
- **Application Layer** → Business logic
- **Infrastructure Layer** → Database işlemleri
- **Domain Layer** → Entity modelleri
- **SignalR Layer** → Real-time notification hub'ları
- **Validation Layer** → Request doğrulama kuralları

---

## ⚙️ Özellikler

- CRUD operasyonları
- JWT tabanlı authentication
- Role-based authorization (opsiyonel genişletilebilir)
- SignalR ile anlık bildirim sistemi
- FluentValidation ile request doğrulama
- Modüler ve ölçeklenebilir yapı
- RESTful API + real-time hibrit mimari
- EF Core Migration desteği

---

## 🔐 Environment Variables

Proje aşağıdaki environment değişkenlerini kullanır:

- `ConnectionStrings:DefaultConnection`
- `ASPNETCORE_ENVIRONMENT`
- `JWT:Key`
- `JWT:Issuer`
- `JWT:Audience`

---

## ▶️ Projeyi Çalıştırma

```bash
dotnet restore
dotnet build
dotnet run
