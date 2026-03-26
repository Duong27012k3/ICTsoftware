using AZT_Backend.Controllers;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UseCase;

namespace AZT_Backend.Controllers
{
    public class ServiceController : BaseController
    {
        private readonly ServiceListManager _serviceManager;
        private readonly ServiceTransListManager _serviceTransManager;
        private readonly FieldListManager _fieldManager;
        private readonly IWebHostEnvironment _env;

        // Fix for CS1520 and CS7036: Correct constructor name and add required parameter for base class
        public ServiceController(
            JwtService jwtService,
            ServiceListManager serviceManager,
            ServiceTransListManager serviceTransManager,
            FieldListManager fieldManager,
            IWebHostEnvironment env)
            : base(jwtService)
        {
            _serviceManager = serviceManager;
            _serviceTransManager = serviceTransManager;
            _fieldManager = fieldManager;
            _env = env; 
        }

        public IActionResult Index()
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();
            return View(_serviceManager.GetAllServices());
        }

        public IActionResult Create()
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();
            LoadFieldDropdown();
            return View(new Service());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service model,
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

                _serviceManager.AddService(model);

                if (!string.IsNullOrWhiteSpace(viName))
                    _serviceTransManager.AddTrans(new ServiceTrans
                    {
                        ServiceId = model.ServiceId,
                        LangCode = "vi",
                        Name = viName,
                        ShortDescription = viShortDescription
                    });
                if (!string.IsNullOrWhiteSpace(enName))
                    _serviceTransManager.AddTrans(new ServiceTrans
                    {
                        ServiceId = model.ServiceId,
                        LangCode = "en",
                        Name = enName,
                        ShortDescription = enShortDescription
                    });

                TempData["Success"] = "Tạo dịch vụ thành công!";
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
            var service = _serviceManager.GetServiceByID(id);
            if (service == null) return NotFound();
            LoadFieldDropdown(service.FieldId);
            return View(service);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Service model,
            IFormFile? imageFile, IFormFile? catalogueFile)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();

            model.ServiceId = id;
            if (!ModelState.IsValid)
            {
                model.ServiceTrans = _serviceTransManager.GetTransByServiceID(id).ToList();
                LoadFieldDropdown(model.FieldId);
                return View(model);
            }
            try
            {
                var existing = _serviceManager.GetServiceByID(id);
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
                _serviceManager.UpdateService(model);
                TempData["Success"] = "Cập nhật dịch vụ thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                model.ServiceTrans = _serviceTransManager.GetTransByServiceID(id).ToList();
                LoadFieldDropdown(model.FieldId);
                return View(model);
            }
        }

        public IActionResult Delete(int id)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();
            var s = _serviceManager.GetServiceByID(id);
            if (s == null) return NotFound();
            return View(s);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();
            try
            {
                var s = _serviceManager.GetServiceByID(id);
                FileUploadHelper.DeleteFile(s?.Image, _env);
                FileUploadHelper.DeleteFile(s?.CatalogueUrl, _env);
                _serviceManager.DeleteService(id);
                TempData["Success"] = "Xóa dịch vụ thành công!";
            }
            catch (Exception ex) { TempData["Error"] = ex.Message; }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddTrans(ServiceTrans model)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();
            try { _serviceTransManager.AddTrans(model); TempData["Success"] = "Thêm bản dịch thành công!"; }
            catch (Exception ex) { TempData["Error"] = ex.Message; }
            return RedirectToAction(nameof(Edit), new { id = model.ServiceId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteTrans(int transId, int serviceId)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();
            try { _serviceTransManager.DeleteTrans(transId); TempData["Success"] = "Xóa bản dịch thành công!"; }
            catch (Exception ex) { TempData["Error"] = ex.Message; }
            return RedirectToAction(nameof(Edit), new { id = serviceId });
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
    }
}