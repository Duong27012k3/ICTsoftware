using Microsoft.AspNetCore.Mvc;

namespace AZT_Backend.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly FieldListManager _fieldManager;
        private readonly ServiceListManager _serviceManager;
        private readonly ProjectListManager _projectManager;
        private readonly ContactListManager _contactManager;
        private readonly UserListManager _userManager;

        public DashboardController(
            FieldListManager fieldManager,
            ServiceListManager serviceManager,
            ProjectListManager projectManager,
            ContactListManager contactManager,
            UserListManager userManager,
            JwtService jwtService) : base(jwtService)
        {
            _fieldManager = fieldManager;
            _serviceManager = serviceManager;
            _projectManager = projectManager;
            _contactManager = contactManager;
            _userManager = userManager;
        }

        // GET /Dashboard
        public IActionResult Index()
        {
            if (!IsLoggedIn) return RequireLogin();

            ViewBag.TotalFields = _fieldManager.GetAllFields().Count();
            ViewBag.TotalServices = _serviceManager.GetAllServices().Count();
            ViewBag.TotalProjects = _projectManager.GetAllProjects().Count();
            ViewBag.TotalContacts = _contactManager.GetAllContacts().Count();
            ViewBag.TotalUsers = _userManager.GetAllUsers().Count();

            ViewBag.RecentContacts = _contactManager.GetAllContacts()
                .OrderByDescending(c => c.CreatedAt)
                .Take(5)
                .ToList();

            return View();
        }
    }
}
