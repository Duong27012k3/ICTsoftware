using System.Diagnostics;
using AZT_Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace AZT_Backend.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserListManager _userManager;
        private readonly JwtService _jwtService;
        
        public AccountController(
            UserListManager userManager,
            JwtService jwtService) : base(jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            
        }

        // GET /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            if (IsLoggedIn) return RedirectToAction("Index", "Dashboard");
            return View();
        }

        // POST /Account/Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin.";
                return View();
            }

            try
            {
                // UseCase xác thực, bcrypt verify
                var user = _userManager.Login(username, password);

                // Tạo JWT token
                var token = _jwtService.GenerateToken(user);

                // Lưu vào HttpOnly Cookie (browser không đọc được bằng JS)
                Response.Cookies.Append("jwt_token", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,        // true khi deploy HTTPS
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddHours(8)
                });

                TempData["Success"] = $"Chào mừng, {user.Username}!";
                return RedirectToAction("Index", "Dashboard");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            if (IsLoggedIn) return RedirectToAction("Index", "Dashboard");
            return View();
        }

        // POST /Account/Register
        [HttpPost]
        public IActionResult Register(string username, string password, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(username) || username.Length < 3)
            {
                ViewBag.Error = "Username must be at least 3 characters.";
                return View();
            }
            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            {
                ViewBag.Error = "Password must be at least 6 characters.";
                return View();
            }
            if (password != confirmPassword)
            {
                ViewBag.Error = "Passwords do not match.";
                return View();
            }

            try
            {
                var newUser = new User
                {
                    Username = username.Trim(),
                    Password = string.Empty, // AddUser tự hash
                    Role = "user",       // luôn là "user"
                    Status = "active"
                };

                _userManager.AddUser(newUser, password);

                TempData["Success"] = $"Account '{username}' created! Please sign in.";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                // Bắt lỗi username trùng từ UseCase
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET /Account/Logout
        public IActionResult Logout()
        {
            // Xóa cookie chứa JWT
            Response.Cookies.Delete("jwt_token");
            TempData["Success"] = "Đã đăng xuất thành công.";
            return RedirectToAction("Login");
        }
        
    }
}