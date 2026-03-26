using Microsoft.AspNetCore.Mvc;

namespace AZT_Backend.Controllers
{
    //contact controller
    public class ContactController : BaseController
    {
        private readonly ContactListManager _contactManager;

        public ContactController(
            ContactListManager contactManager,
            JwtService jwtService) : base(jwtService)
        {
            _contactManager = contactManager;
        }

        // GET /Contact
        public IActionResult Index()
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();

            var contacts = _contactManager.GetAllContacts()
                .OrderByDescending(c => c.CreatedAt);
            return View(contacts);
        }

        // GET /Contact/Details/5
        public IActionResult Details(int id)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();

            var contact = _contactManager.GetContactByID(id);
            if (contact == null) return NotFound();
            return View(contact);
        }

        // POST /Contact/Delete/5
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();

            try
            {
                _contactManager.DeleteContact(id);
                TempData["Success"] = "Xóa liên hệ thành công!";
            }
            catch (Exception ex) { TempData["Error"] = ex.Message; }

            return RedirectToAction(nameof(Index));
        }
    }
    public class UserController : BaseController
    {
        private readonly UserListManager _userManager;

        public UserController(
            UserListManager userManager,
            JwtService jwtService) : base(jwtService)
        {
            _userManager = userManager;
        }

        // GET /User
        public IActionResult Index()
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();

            return View(_userManager.GetAllUsers());
        }

        // GET /User/Create
        public IActionResult Create()
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();
            return View(new User());
        }

        // POST /User/Create
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(User model, string password, string confirmPassword)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();

            if (password != confirmPassword)
                ModelState.AddModelError("", "Mật khẩu xác nhận không khớp.");

            if (!ModelState.IsValid) return View(model);

            try
            {
                _userManager.AddUser(model, password);
                TempData["Success"] = "Tạo tài khoản thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        // GET /User/Edit/5
        public IActionResult Edit(int id)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();

            var user = _userManager.GetUserByID(id);
            if (user == null) return NotFound();

            user.Password = ""; // Không hiển thị hash ra view
            return View(user);
        }

        // POST /User/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(int id, User model, string? newPassword)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();

            model.UserId = id;
            try
            {
                _userManager.UpdateUser(model, newPassword);
                TempData["Success"] = "Cập nhật tài khoản thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        // GET /User/Delete/5
        public IActionResult Delete(int id)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();

            var user = _userManager.GetUserByID(id);
            if (user == null) return NotFound();
            return View(user);
        }

        // POST /User/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();

            try
            {
                _userManager.DeleteUser(id, CurrentUserId!.Value);
                TempData["Success"] = "Xóa tài khoản thành công!";
            }
            catch (Exception ex) { TempData["Error"] = ex.Message; }

            return RedirectToAction(nameof(Index));
        }
    }

}
