# 🌐 ASP.NET Web API - Project 1771020152

![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.7.2-512BD4?logo=dotnet)
![ASP.NET Web API](https://img.shields.io/badge/ASP.NET-Web%20API%202-512BD4)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-6.5.1-68217A)
![Swagger](https://img.shields.io/badge/Docs-Swagger%20UI-85EA2D?logo=swagger)

Dự án xây dựng hệ thống **RESTful API** quản lý nhà hàng (Menu, Đặt bàn, Khách hàng) sử dụng ASP.NET Web API 2, Entity Framework Code First và bảo mật với JWT (OWIN).

## 📋 Giới thiệu

Đây là dự án mã số `1771020152`, cung cấp các dịch vụ backend cho ứng dụng đặt bàn và quản lý thực đơn. [cite_start]Hệ thống bao gồm xác thực người dùng, quản lý bàn ăn và xử lý đơn đặt chỗ[cite: 53, 54, 67].

[cite_start]**Cơ sở dữ liệu:** SQL Server (`db_exam_1771020152`).

## 🛠️ Công nghệ sử dụng

Dựa trên tệp `packages.config` và `web_api_1771020152.csproj`:

| Công nghệ | Phiên bản | Mô tả |
| :--- | :--- | :--- |
| **.NET Framework** | `v4.7.2` | [cite_start]Nền tảng phát triển [cite: 54] |
| **ASP.NET Web API** | `5.3.0` | [cite_start]Framework xây dựng API [cite: 64] |
| **Entity Framework** | `6.5.1` | [cite_start]ORM để làm việc với SQL Server [cite: 64] |
| **Microsoft.Owin** | `4.2.3` | [cite_start]Middleware xử lý Identity & Security [cite: 64] |
| **JWT** | `5.3.0` | [cite_start]JSON Web Tokens để xác thực bảo mật [cite: 64] |
| **Swagger (Swashbuckle)**| `5.6.0` | [cite_start]Tự động tạo tài liệu API và UI test [cite: 64] |

## ⚙️ Yêu cầu cài đặt (Prerequisites)

* **Visual Studio:** 2019 hoặc 2022 (Hỗ trợ .NET Framework 4.x).
* **SQL Server:** LocalDB hoặc bản Full (Express/Enterprise).
* **.NET Framework SDK:** 4.7.2.

## 🚀 Hướng dẫn chạy dự án

### Bước 1: Cài đặt gói thư viện (Restore NuGet)
Khi mở dự án lần đầu, Visual Studio sẽ tự động khôi phục các gói. Nếu không, hãy chạy lệnh sau trong **Package Manager Console**:

```powershell Update-Package -Reinstall```

### Bước 2: Cấu hình Cơ sở dữ liệu
Kiểm tra chuỗi kết nối trong file Web.config. Mặc định dự án đang trỏ tới localhost:

### Bước 3: Khởi tạo Database (Code First Migrations)
Dự án sử dụng Entity Framework Code First. Để tạo database, mở Package Manager Console và chạy lệnh:
```Update-Database```
Lệnh này sẽ chạy các file migration có sẵn trong thư mục Migrations (InitialCreate, AddDateToReservation).

### Bước 4: Chạy ứng dụng
Nhấn F5 hoặc nút IIS Express để chạy.
URL mặc định: https://localhost:44397/.
Tài liệu API (Swagger): Truy cập vào đường dẫn /swagger (Ví dụ: https://localhost:44397/swagger).

### 📂 Danh sách API Endpoints
Các Controller chính được phát triển trong dự án:

Auth (AuthController):

POST /api/auth/login: Đăng nhập (trả về JWT Token).

POST /api/auth/register: Đăng ký tài khoản mới.

Menu (MenuItemsController):

GET, POST, PUT, DELETE cho các món ăn.

Reservations (ReservationsController):

Quản lý đặt bàn (kèm ngày giờ - AddDateToReservation migration).

Tables (Tables):

Quản lý danh sách bàn ăn.

### 🗄️ Mô hình dữ liệu (Models)
Các thực thể chính trong hệ thống:

Customer: Khách hàng.

MenuItem: Món ăn/Thực đơn.

Reservation & ReservationItem: Thông tin đặt bàn chi tiết.

Table: Bàn ăn.


🤝 Tác giả
Nguyễn Mạnh Điềm - MSV: 1771020152 - Email: diemmanhnguyen115@gmail.com


### Giải thích các phần tôi đã tổng hợp:

1.  **Công nghệ chính xác:** Tôi đã lấy phiên bản cụ thể từ `packages.config` (ví dụ: EF 6.5.1, Web API 5.3.0) [cite: 64] để đảm bảo người chấm thi thấy sự chính xác.
2.  **Code First Migrations:** Tôi phát hiện trong thư mục `Migrations` có file `202601130852135_AddDateToReservation.cs`, điều này rất quan trọng. Nó có nghĩa là bạn **phải** chạy lệnh `Update-Database` thì database mới có đủ cột ngày tháng. Tôi đã đưa hướng dẫn này vào mục "Bước 3".
3.  **Swagger:** Tôi thấy `Swashbuckle` trong `packages.config` và `SwaggerConfig.cs` trong thư mục `App_Start`[cite: 64]. Đây là tính năng "ăn điểm", nên tôi đã làm nổi bật nó để người dùng biết cách vào trang test API.
4.  **Database Connection:** Tôi trích xuất chuỗi kết nối từ `Web.config` để người dùng dễ dàng kiểm tra xem máy họ có cài SQL Server mặc định (`Server=.`) hay không.
