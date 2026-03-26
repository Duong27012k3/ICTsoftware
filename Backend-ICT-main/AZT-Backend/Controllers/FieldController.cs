using AZT_Backend.Controllers;
using Entity;
using Microsoft.AspNetCore.Mvc;
using UseCase;


namespace AZT_Backend.Controllers
{
    public class FieldController : BaseController
    {
        private readonly FieldListManager _fieldManager;
        private readonly FieldTransListManager _fieldTransManager;
        private readonly IWebHostEnvironment _env;

        public FieldController(
            JwtService jwtService,
            FieldListManager fieldManager,
            FieldTransListManager fieldTransManager,
            IWebHostEnvironment env)
            : base(jwtService)
        {
            _fieldManager = fieldManager;
            _fieldTransManager = fieldTransManager;
            _env = env;
        }

        public IActionResult Index()
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();
            return View(_fieldManager.GetAllFields());
        }

        public IActionResult Create()
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();
            return View(new Field());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Field model,
            IFormFile? imageFile,
            string? viName, string? viDescription,
            string? enName, string? enDescription)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();

            if (!ModelState.IsValid) return View(model);
            try
            {
                // Upload ảnh nếu có
                if (imageFile != null)
                    model.Image = await FileUploadHelper.SaveImageAsync(imageFile, _env);

                _fieldManager.AddField(model);

                // Tự động thêm bản dịch VI nếu có nhập tên
                if (!string.IsNullOrWhiteSpace(viName))
                    _fieldTransManager.AddTrans(new FieldTrans
                    {
                        FieldId = model.FieldId,
                        LangCode = "vi",
                        Name = viName,
                        Description = viDescription
                    });
                if (!string.IsNullOrWhiteSpace(enName))
                    _fieldTransManager.AddTrans(new FieldTrans
                    {
                        FieldId = model.FieldId,
                        LangCode = "en",
                        Name = enName,
                        Description = enDescription
                    });

                TempData["Success"] = "Tạo lĩnh vực thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        public IActionResult Edit(int id)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();
            var field = _fieldManager.GetFieldByID(id);
            if (field == null) return NotFound();
            return View(field);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Field model, IFormFile? imageFile)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();
            model.FieldId = id;
            if (!ModelState.IsValid)
            {
                model.FieldTrans = _fieldTransManager.GetTransByFieldID(id).ToList();
                return View(model);
            }
            try
            {
                if (imageFile != null)
                {
                    var old = _fieldManager.GetFieldByID(id)?.Image;
                    FileUploadHelper.DeleteFile(old, _env);
                    model.Image = await FileUploadHelper.SaveImageAsync(imageFile, _env);
                }
                _fieldManager.UpdateField(model);
                TempData["Success"] = "Cập nhật lĩnh vực thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                model.FieldTrans = _fieldTransManager.GetTransByFieldID(id).ToList();
                return View(model);
            }
        }

        public IActionResult Delete(int id)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();
            var field = _fieldManager.GetFieldByID(id);
            if (field == null) return NotFound();
            return View(field);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();
            try
            {
                var field = _fieldManager.GetFieldByID(id);
                FileUploadHelper.DeleteFile(field?.Image, _env);
                _fieldManager.DeleteField(id);
                TempData["Success"] = "Xóa lĩnh vực thành công!";
            }
            catch (Exception ex) { TempData["Error"] = ex.Message; }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddTrans(FieldTrans model)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();
            try { _fieldTransManager.AddTrans(model); TempData["Success"] = "Thêm bản dịch thành công!"; }
            catch (Exception ex) { TempData["Error"] = ex.Message; }
            return RedirectToAction(nameof(Edit), new { id = model.FieldId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteTrans(int transId, int fieldId)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();
            try { _fieldTransManager.DeleteTrans(transId); TempData["Success"] = "Xóa bản dịch thành công!"; }
            catch (Exception ex) { TempData["Error"] = ex.Message; }
            return RedirectToAction(nameof(Edit), new { id = fieldId });
        }
    }
}
