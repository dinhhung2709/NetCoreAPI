using Microsoft.EntityFrameworkCore;
using FirstWebMVC.Data;
using FirstWebMVC.Models;
using OfficeOpenXml;

var builder = WebApplication.CreateBuilder(args);

// =========================
// EPPlus LICENSE (BẮT BUỘC)
// =========================
ExcelPackage.License.SetNonCommercialPersonal("Student App");

// MVC
builder.Services.AddControllersWithViews();


// =========================
// KẾT NỐI SQL SERVER (BUỔI 10)
// =========================
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

var app = builder.Build();


// =========================
// SEED DATA (Faculty)
// =========================
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (!context.Faculties.Any())
    {
        context.Faculties.AddRange(
            new Faculty { FacultyName = "Công nghệ thông tin" },
            new Faculty { FacultyName = "Kinh tế" },
            new Faculty { FacultyName = "Xây dựng" },
            new Faculty { FacultyName = "Cơ khí" }
        );

        context.SaveChanges();
    }
}


// =========================
// PIPELINE CONFIG
// =========================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();