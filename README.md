# NetCoreAPI
## cấu trúc thư mục của dự án .Net MVC :
MVCMOVIE : Tên project
Controllers : Thư mục chứa các controller (code để xử lý yêu cầu từ View gửi về)
Models : Chứa các lớp đại diện cho CSDL của ứng dụng
Views : Thư mục chứa các thành phần hiển thị giao diện người dùng
wwwroot : Thư mục Chứa các file của dự án (HTML, CSS, JS)
appsettings.json và Program.cs : File chứa code cấu hình dự án
## định tuyến (Route) trong .Net MVC: 
MVC sẽ gọi bộ điều khiển (Controller) và các hành động bên trong (Action) thông qua URL
Logic định tuyến MVC sử dụng dạng: /[Controller]/[Action]/[Parameters]
Định tuyến được cấu hình trong file Program.cs: 
      app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
## namespace trong C#:
Namespace trong C# là cơ chế dùng để nhóm các lớp (class), interface và các thành phần liên quan vào cùng một không gian tên nhằm tránh trùng tên và giúp tổ chức mã nguồn rõ ràng hơn. Mỗi namespace thường đại diện cho một module hoặc một tầng chức năng trong dự án, ví dụ như DemoMvcApp.Controllers hay DemoMvcApp.Models. Việc sử dụng namespace giúp lập trình viên dễ quản lý dự án lớn, tăng tính tái sử dụng mã và hạn chế xung đột tên giữa các class có cùng tên ở những thư viện khác nhau.
## Tìm hiểu về Controller:
Tên của Controller bắt buộc phải có phần hậu tố Controller: Ví dụ StudentController, PersonController
Nằm trong thư mục Controllers
Nhiệm vụ của Controller:
Xử lý các yêu cầu của người dùng gửi tới từ View.
Truy xuất dữ liệu trong cơ sở dữ liệu.
Gọi các mẫu View và trả về dữ liệu
## Tìm hiểu về View : 
Có phần mở rộng là “.cshtml”
Nằm trong thư mục Views/Controler_Name (tương ứng với HelloWorldController sẽ có thư mục HelloWorld trong thư mục Views)
Nhiệm vụ của View: Cung cấp giao diện người dùng (HTML) bằng C#






