using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace AZT_Backend.Controllers
{
    public class BaseController : Controller
    {
        private readonly JwtService _jwtService;
        private ClaimsPrincipal? _cachedPrincipal;

        public BaseController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        // ── Đọc + validate JWT từ Cookie ─────────────────────
        private ClaimsPrincipal? GetPrincipal()
        {
            if (_cachedPrincipal != null) return _cachedPrincipal;

            var token = HttpContext.Request.Cookies["jwt_token"];
            if (string.IsNullOrEmpty(token)) return null;

            _cachedPrincipal = _jwtService.ValidateToken(token);
            return _cachedPrincipal;
        }

        // ── Helpers đọc Claims ───────────────────────────────
        protected int? CurrentUserId
            => int.TryParse(
                GetPrincipal()?.FindFirstValue(ClaimTypes.NameIdentifier),
                out var id) ? id : null;

        protected string CurrentUsername
            => GetPrincipal()?.FindFirstValue(ClaimTypes.Name) ?? "";

        protected string CurrentRole
            => GetPrincipal()?.FindFirstValue(ClaimTypes.Role) ?? "";

        protected bool IsLoggedIn => GetPrincipal() != null;
        protected bool IsAdmin => CurrentRole == "admin";

        // ── Guard helpers ────────────────────────────────────
        protected IActionResult RequireLogin()
        {
            TempData["Error"] = "Vui lòng đăng nhập để tiếp tục.";
            return RedirectToAction("Login", "Account");
        }

        protected IActionResult RequireAdmin()
        {
            TempData["Error"] = "Bạn không có quyền truy cập chức năng này.";
            return RedirectToAction("Index", "Dashboard");
        }

        // ── Set ViewBag cho mọi Action ───────────────────────
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.IsLoggedIn = IsLoggedIn;
            ViewBag.IsAdmin = IsAdmin;
            ViewBag.CurrentUsername = CurrentUsername;
            ViewBag.CurrentRole = CurrentRole;
            base.OnActionExecuting(context);
        }
    }
}
