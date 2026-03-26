using AZT_Backend.Models;
using Entity;
using Microsoft.AspNetCore.Mvc;
using UseCase;

namespace AZT_Backend.Controllers
{
    [Route("api/public")]
    [ApiController]
    public class PublicApiController : ControllerBase
    {
        private readonly FieldListManager _fieldManager;
        private readonly ProjectListManager _projectManager;
        private readonly ServiceListManager _serviceManager;
        private readonly ContactListManager _contactManager;

        public PublicApiController(
            FieldListManager fieldManager,
            ProjectListManager projectManager,
            ServiceListManager serviceManager,
            ContactListManager contactManager)
        {
            _fieldManager = fieldManager;
            _projectManager = projectManager;
            _serviceManager = serviceManager;
            _contactManager = contactManager;
        }

        // GET: api/public/fields
        [HttpGet("fields")]
        public IActionResult GetFields()
        {
            var fields = _fieldManager.GetActiveFields();
            return Ok(fields);
        }

        // GET: api/public/fields/{id}
        [HttpGet("fields/{id}")]
        public IActionResult GetField(int id)
        {
            var field = _fieldManager.GetFieldByID(id);
            if (field == null) return NotFound(new { message = "Lĩnh vực không tồn tại" });
            return Ok(field);
        }

        // GET: api/public/projects
        [HttpGet("projects")]
        public IActionResult GetProjects()
        {
            var projects = _projectManager.GetAllProjects();
            return Ok(projects);
        }

        // GET: api/public/projects/{id}
        [HttpGet("projects/{id}")]
        public IActionResult GetProject(int id)
        {
            var project = _projectManager.GetProjectByID(id);
            if (project == null) return NotFound(new { message = "Dự án không tồn tại" });
            return Ok(project);
        }

        // GET: api/public/services
        [HttpGet("services")]
        public IActionResult GetServices()
        {
            var services = _serviceManager.GetAllServices();
            return Ok(services);
        }

        // GET: api/public/services/{id}
        [HttpGet("services/{id}")]
        public IActionResult GetService(int id)
        {
            var service = _serviceManager.GetServiceByID(id);
            if (service == null) return NotFound(new { message = "Dịch vụ không tồn tại" });
            return Ok(service);
        }

        // POST: api/public/contact
        [HttpPost("contact")]
        public IActionResult SubmitContact([FromBody] Contact model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            try
            {
                model.CreatedAt = DateTime.UtcNow;
                _contactManager.AddContact(model);
                return Ok(new { message = "Gửi liên hệ thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
