using AZT_Backend.Controllers;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using UseCase;


var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// ── 1. DATABASE ───────────────────────────────────────────────
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// ── 2. MVC & CORS ─────────────────────────────────────────────
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:3000", "http://localhost", "http://localhost:80") // Vite and React defaults and Docker
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // needed if using cookies later
    });
});

// ── 3. JWT SERVICE ────────────────────────────────────────────
builder.Services.AddSingleton<JwtService>();

// ── 4. JWT AUTHENTICATION ─────────────────────────────────────
// FIX: dùng JwtBearerDefaults.AuthenticationScheme thay vì "Bearer"
// FIX: thêm AddAuthorization()
// FIX: bỏ AddTransient<ContactListManager> trùng lặp
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        var config = builder.Configuration;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config["Jwt:Issuer"],
            ValidAudience = config["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(config["Jwt:SecretKey"]!))
        };

        // Đọc JWT từ HttpOnly Cookie "jwt_token"
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = ctx =>
            {
                ctx.Token = ctx.Request.Cookies["jwt_token"];
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

// ── 5. REPOSITORIES ───────────────────────────────────────────
builder.Services.AddScoped<IContactRepository, PostgresContactRepository>();
builder.Services.AddScoped<IUserRepository, PostgresUserRepository>();
builder.Services.AddScoped<IFieldRepository, PostgresFieldRepository>();
builder.Services.AddScoped<IFieldTransRepository, PostgresFieldTransRepository>();
builder.Services.AddScoped<IServiceRepository, PostgresServiceRepository>();
builder.Services.AddScoped<IServiceTransRepository, PostgresServiceTransRepository>();
builder.Services.AddScoped<IProjectRepository, PostgresProjectRepository>();
builder.Services.AddScoped<IProjectTransRepository, PostgresProjectTransRepository>();
builder.Services.AddScoped<IFeatureRepository, PostgresFeatureRepository>();
builder.Services.AddScoped<IBlockRepository, PostgresBlockRepository>();
builder.Services.AddScoped<IBlockTransRepository, PostgresBlockTransRepository>();

// ── 6. USE CASE MANAGERS ──────────────────────────────────────
builder.Services.AddScoped<ContactListManager>();
builder.Services.AddScoped<UserListManager>();
builder.Services.AddScoped<FieldListManager>();
builder.Services.AddScoped<FieldTransListManager>();
builder.Services.AddScoped<ServiceListManager>();
builder.Services.AddScoped<ServiceTransListManager>();
builder.Services.AddScoped<ProjectListManager>();
builder.Services.AddScoped<ProjectTransListManager>();
builder.Services.AddScoped<FeatureListManager>();
builder.Services.AddScoped<BlockListManager>();
builder.Services.AddScoped<BlockTransListManager>();

// ── BUILD ─────────────────────────────────────────────────────
var app = builder.Build();

// Auto migrate
// Auto migrate & Seed Data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // 1. Chạy cập nhật Database
    context.Database.Migrate();

    // 2. Tạo tài khoản Admin mặc định
    // LƯU Ý: Đổi 'Users', 'Username', 'Password' cho đúng với tên bảng và thuộc tính trong Model của bạn nhé
    var adminExists = context.Users.Any(u => u.Username == "admin");
    if (!adminExists)
    {
        var adminUser = new User
        {
            Username = "admin",
            // BẮT BUỘC PHẢI CÓ DÒNG NÀY ĐỂ MÃ HÓA:
            Password = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
            Role = "admin",
            Status = "active"
        };

        context.Users.Add(adminUser);
        context.SaveChanges();
    }

    if (!context.Fields.Any())
    {
        // === 1. SEED FIELDS ===
        var fDlp = new Entity.Field { Uid = "bao-mat-du-lieu", Status = "active", Image = "https://images.unsplash.com/photo-1563986768609-322da13575f3?w=800&auto=format&fit=crop" };
        fDlp.FieldTrans.Add(new Entity.FieldTrans { LangCode = "vi", Name = "Bảo Mật & Phòng Chống Thất Thoát Dữ Liệu", Description = "Bảo vệ tài sản số quan trọng nhất của doanh nghiệp." });
        context.Fields.Add(fDlp);

        var fAssessment = new Entity.Field { Uid = "danh-gia-an-ninh", Status = "active", Image = "https://images.unsplash.com/photo-1550751827-4bd374c3f58b?w=800&auto=format&fit=crop" };
        fAssessment.FieldTrans.Add(new Entity.FieldTrans { LangCode = "vi", Name = "Đánh Giá An Ninh & Dò Quét Lỗ Hổng", Description = "Chủ động phát hiện và khắc phục các điểm yếu bảo mật trong hệ thống." });
        context.Fields.Add(fAssessment);

        var fAppSec = new Entity.Field { Uid = "bao-mat-ung-dung", Status = "active", Image = "https://images.unsplash.com/photo-1555949963-aa79dcee981c?w=800&auto=format&fit=crop" };
        fAppSec.FieldTrans.Add(new Entity.FieldTrans { LangCode = "vi", Name = "Bảo Mật Ứng Dụng Web & API", Description = "Bảo vệ các website và ứng dụng trực tuyến khỏi hacker, botnet." });
        context.Fields.Add(fAppSec);

        var fPam = new Entity.Field { Uid = "quan-ly-truy-cap", Status = "active", Image = "https://images.unsplash.com/photo-1518770660439-4636190af475?w=800&auto=format&fit=crop" };
        fPam.FieldTrans.Add(new Entity.FieldTrans { LangCode = "vi", Name = "Quản Lý Định Danh & Truy Cập Đặc Quyền", Description = "Giám sát gắt gao các tài khoản quản trị mạng." });
        context.Fields.Add(fPam);

        var fSiem = new Entity.Field { Uid = "giam-sat-su-co", Status = "active", Image = "https://images.unsplash.com/photo-1551288049-bebda4e38f71?w=800&auto=format&fit=crop" };
        fSiem.FieldTrans.Add(new Entity.FieldTrans { LangCode = "vi", Name = "Giám Sát & Phản Ứng Sự Cố An Ninh", Description = "Trung tâm điều hành an ninh mạng — thu thập, phân tích và xử lý sự cố." });
        context.Fields.Add(fSiem);

        var fEdr = new Entity.Field { Uid = "bao-mat-endpoint", Status = "active", Image = "https://images.unsplash.com/photo-1563986768609-322da13575f3?w=800&auto=format&fit=crop" };
        fEdr.FieldTrans.Add(new Entity.FieldTrans { LangCode = "vi", Name = "Bảo Mật Thiết Bị Đầu Cuối", Description = "Phát hiện và ngăn chặn mối đe dọa trên workstation, laptop và server." });
        context.Fields.Add(fEdr);

        context.SaveChanges();

        // === 2. SEED PRODUCTS ===
        var pDlp = new Entity.Project { FieldId = fDlp.FieldId, Status = "active", Image = "https://images.unsplash.com/photo-1558494949-ef010cbdcc31?w=800&auto=format&fit=crop" };
        pDlp.ProjectTrans.Add(new Entity.ProjectTrans { LangCode = "vi", Name = "ICT DLP", Description = "Giải pháp phòng chống thất thoát dữ liệu toàn diện." });
        pDlp.Features.Add(new Entity.Feature { LangCode = "vi", Content = "Endpoint Security", FeatureType = "feature" });
        pDlp.Features.Add(new Entity.Feature { LangCode = "vi", Content = "Network Control", FeatureType = "feature" });
        pDlp.Features.Add(new Entity.Feature { LangCode = "vi", Content = "10K+", FeatureType = "spec", Label = "Endpoint Agents" });
        pDlp.Features.Add(new Entity.Feature { LangCode = "vi", Content = "500+", FeatureType = "spec", Label = "Policy Rules" });
        context.Projects.Add(pDlp);

        var pIas = new Entity.Project { FieldId = fAssessment.FieldId, Status = "active", Image = "https://images.unsplash.com/photo-1550751827-4bd374c3f58b?w=800&auto=format&fit=crop" };
        pIas.ProjectTrans.Add(new Entity.ProjectTrans { LangCode = "vi", Name = "Hệ Thống IAS", Description = "Hệ thống phần mềm tự động dò quét thông minh." });
        pIas.Features.Add(new Entity.Feature { LangCode = "vi", Content = "Dò quét diện rộng", FeatureType = "feature" });
        pIas.Features.Add(new Entity.Feature { LangCode = "vi", Content = "Trí tuệ nhân tạo (AI)", FeatureType = "feature" });
        pIas.Features.Add(new Entity.Feature { LangCode = "vi", Content = "200K", FeatureType = "spec", Label = "Website/24h" });
        pIas.Features.Add(new Entity.Feature { LangCode = "vi", Content = "280K+", FeatureType = "spec", Label = "CVE Database" });
        context.Projects.Add(pIas);

        var pWaf = new Entity.Project { FieldId = fAppSec.FieldId, Status = "active", Image = "https://images.unsplash.com/photo-1518770660439-4636190af475?w=800&auto=format&fit=crop" };
        pWaf.ProjectTrans.Add(new Entity.ProjectTrans { LangCode = "vi", Name = "ICT WAF", Description = "Tường lửa ứng dụng web chuyên biệt." });
        pWaf.Features.Add(new Entity.Feature { LangCode = "vi", Content = "Bảo vệ phòng chống Web Exploits phổ biến", FeatureType = "feature" });
        pWaf.Features.Add(new Entity.Feature { LangCode = "vi", Content = "10Gbps", FeatureType = "spec", Label = "Throughput" });
        context.Projects.Add(pWaf);

        var pPam = new Entity.Project { FieldId = fPam.FieldId, Status = "active", Image = "https://images.unsplash.com/photo-1633265486064-086b219458ec?w=800&auto=format&fit=crop" };
        pPam.ProjectTrans.Add(new Entity.ProjectTrans { LangCode = "vi", Name = "ICT PAM", Description = "Bộ giải pháp kiểm soát và quản lý truy cập đặc quyền." });
        pPam.Features.Add(new Entity.Feature { LangCode = "vi", Content = "Quản lý vòng đời mật khẩu đặc quyền tự động", FeatureType = "feature" });
        pPam.Features.Add(new Entity.Feature { LangCode = "vi", Content = "50K+", FeatureType = "spec", Label = "Managed Accounts" });
        context.Projects.Add(pPam);

        var pSiem = new Entity.Project { FieldId = fSiem.FieldId, Status = "active", Image = "https://images.unsplash.com/photo-1551288049-bebda4e38f71?w=800&auto=format&fit=crop" };
        pSiem.ProjectTrans.Add(new Entity.ProjectTrans { LangCode = "vi", Name = "ICT SIEM", Description = "Nền tảng quản lý sự kiện an ninh." });
        pSiem.Features.Add(new Entity.Feature { LangCode = "vi", Content = "Thu thập và tương quan log tập trung", FeatureType = "feature" });
        pSiem.Features.Add(new Entity.Feature { LangCode = "vi", Content = "50K+", FeatureType = "spec", Label = "Events/Sec" });
        context.Projects.Add(pSiem);

        var pEdr = new Entity.Project { FieldId = fEdr.FieldId, Status = "active", Image = "https://images.unsplash.com/photo-1563986768609-322da13575f3?w=800&auto=format&fit=crop" };
        pEdr.ProjectTrans.Add(new Entity.ProjectTrans { LangCode = "vi", Name = "ICT EDR", Description = "Giải pháp phát hiện và phản ứng mối đe dọa trên thiết bị đầu cuối." });
        pEdr.Features.Add(new Entity.Feature { LangCode = "vi", Content = "Behavioral Analysis & ML-based Detection", FeatureType = "feature" });
        pEdr.Features.Add(new Entity.Feature { LangCode = "vi", Content = "100K+", FeatureType = "spec", Label = "Endpoints" });
        context.Projects.Add(pEdr);

        context.SaveChanges();
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();    // FIX: dùng UseStaticFiles thay vì MapStaticAssets
app.UseRouting();

// Thêm UseCors TRƯỚC UseAuthentication
app.UseCors("AllowFrontend");

app.UseAuthentication(); // FIX: PHẢI có trước UseAuthorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();