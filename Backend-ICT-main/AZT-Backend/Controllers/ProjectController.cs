using AZT_Backend.Controllers;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UseCase;

namespace AZT_Backend.Controllers
{
    public class ProjectController : BaseController
    {
        private readonly ProjectListManager _projectManager;
        private readonly ProjectTransListManager _projectTransManager;
        private readonly FeatureListManager _featureManager;
        private readonly FieldListManager _fieldManager;
        private readonly IWebHostEnvironment _env;

        public ProjectController(
            JwtService jwtService, // <-- Fix for CS7036: Add required parameter for base constructor
            ProjectListManager projectManager,
            ProjectTransListManager projectTransManager,
            FeatureListManager featureManager,
            FieldListManager fieldManager,
            IWebHostEnvironment env)
            : base(jwtService) // <-- Fix for CS7036: Call base constructor with jwtService
        {
            _projectManager = projectManager;
            _projectTransManager = projectTransManager;
            _featureManager = featureManager;
            _fieldManager = fieldManager;
            _env = env;
        }

        public IActionResult Index()
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();
            return View(_projectManager.GetAllProjects());
        }

        public IActionResult Create()
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();
            LoadFieldDropdown();
            return View(new Project());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project model,
            IFormFile? imageFile, IFormFile? catalogueFile,
            string? viName, string? viShortDescription,
            string? enName, string? enShortDescription)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();

            if (!ModelState.IsValid) { LoadFieldDropdown(model.FieldId); return View(model); }
            try
            {
                if (imageFile != null) model.Image = await FileUploadHelper.SaveImageAsync(imageFile, _env);
                if (catalogueFile != null) model.CatalogueUrl = await FileUploadHelper.SaveCatalogueAsync(catalogueFile, _env);

                _projectManager.AddProject(model);

                if (!string.IsNullOrWhiteSpace(viName))
                    _projectTransManager.AddTrans(new ProjectTrans
                    {
                        ProjectId = model.ProjectId,
                        LangCode = "vi",
                        Name = viName,
                        ShortDescription = viShortDescription
                    });
                if (!string.IsNullOrWhiteSpace(enName))
                    _projectTransManager.AddTrans(new ProjectTrans
                    {
                        ProjectId = model.ProjectId,
                        LangCode = "en",
                        Name = enName,
                        ShortDescription = enShortDescription
                    });

                TempData["Success"] = "Tạo dự án thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                LoadFieldDropdown(model.FieldId);
                return View(model);
            }
        }

        public IActionResult Edit(int id)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();
            var project = _projectManager.GetProjectByID(id);
            if (project == null) return NotFound();
            LoadFieldDropdown(project.FieldId);
            return View(project);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Project model,
            IFormFile? imageFile, IFormFile? catalogueFile)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();

            model.ProjectId = id;
            if (!ModelState.IsValid) { LoadProjectRelations(model, id); return View(model); }
            try
            {
                var existing = _projectManager.GetProjectByID(id);
                if (imageFile != null)
                {
                    FileUploadHelper.DeleteFile(existing?.Image, _env);
                    model.Image = await FileUploadHelper.SaveImageAsync(imageFile, _env);
                }
                if (catalogueFile != null)
                {
                    FileUploadHelper.DeleteFile(existing?.CatalogueUrl, _env);
                    model.CatalogueUrl = await FileUploadHelper.SaveCatalogueAsync(catalogueFile, _env);
                }
                _projectManager.UpdateProject(model);
                TempData["Success"] = "Cập nhật dự án thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                LoadProjectRelations(model, id);
                return View(model);
            }
        }

        public IActionResult Delete(int id)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();
            var p = _projectManager.GetProjectByID(id);
            if (p == null) return NotFound();
            return View(p);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();
            try
            {
                var p = _projectManager.GetProjectByID(id);
                FileUploadHelper.DeleteFile(p?.Image, _env);
                FileUploadHelper.DeleteFile(p?.CatalogueUrl, _env);
                _projectManager.DeleteProject(id);
                TempData["Success"] = "Xóa dự án thành công!";
            }
            catch (Exception ex) { TempData["Error"] = ex.Message; }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddTrans(ProjectTrans model)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();
            try { _projectTransManager.AddTrans(model); TempData["Success"] = "Thêm bản dịch thành công!"; }
            catch (Exception ex) { TempData["Error"] = ex.Message; }
            return RedirectToAction(nameof(Edit), new { id = model.ProjectId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteTrans(int transId, int projectId)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();
            try { _projectTransManager.DeleteTrans(transId); TempData["Success"] = "Xóa bản dịch thành công!"; }
            catch (Exception ex) { TempData["Error"] = ex.Message; }
            return RedirectToAction(nameof(Edit), new { id = projectId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddFeature(Feature model)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();
            try { _featureManager.AddFeature(model); TempData["Success"] = "Thêm tính năng thành công!"; }
            catch (Exception ex) { TempData["Error"] = ex.Message; }
            return RedirectToAction(nameof(Edit), new { id = model.ProjectId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteFeature(int featureId, int projectId)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();
            try { _featureManager.DeleteFeature(featureId); TempData["Success"] = "Xóa tính năng thành công!"; }
            catch (Exception ex) { TempData["Error"] = ex.Message; }
            return RedirectToAction(nameof(Edit), new { id = projectId });
        }

        private void LoadFieldDropdown(int selectedId = 0)
        {
            var fields = _fieldManager.GetActiveFields().ToList();
            ViewBag.FieldList = new SelectList(
                fields.Select(f => new {
                    f.FieldId,
                    Name = f.FieldTrans.FirstOrDefault(t => t.LangCode == "en")?.Name
                        ?? f.FieldTrans.FirstOrDefault()?.Name
                        ?? f.Uid
                }), "FieldId", "Name", selectedId);
        }

        private void LoadProjectRelations(Project model, int id)
        {
            model.ProjectTrans = _projectTransManager.GetTransByProjectID(id).ToList();
            model.Features = _featureManager.GetFeaturesByProject(id).ToList();
            LoadFieldDropdown(model.FieldId);
        }
    }
}