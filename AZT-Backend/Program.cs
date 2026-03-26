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
        var field = new Entity.Field { Uid = "test-field", Status = "active" };
        field.FieldTrans.Add(new Entity.FieldTrans { LangCode = "vi", Name = "Test Field", Description = "Test" });
        context.Fields.Add(field);
        context.SaveChanges();

        var project = new Entity.Project { FieldId = field.FieldId, Status = "active" };
        project.ProjectTrans.Add(new Entity.ProjectTrans { LangCode = "vi", Name = "Test Project", Description = "Test Description" });
        project.Features.Add(new Entity.Feature { LangCode = "vi", Content = "Fast!", FeatureType = "feature" });
        project.Features.Add(new Entity.Feature { LangCode = "vi", Content = "Secure!", FeatureType = "benefit" });
        project.Features.Add(new Entity.Feature { LangCode = "vi", Content = "99.9%", FeatureType = "spec", Label = "Uptime" });
        
        var block = new Entity.Block { OwnerType = "project", BlockType = "text", BlockOrder = 1 };
        block.BlockTrans.Add(new Entity.BlockTrans { LangCode = "vi", Title = "Detail 1", Content = "Detail test" });
        project.Blocks.Add(block);

        context.Projects.Add(project);
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