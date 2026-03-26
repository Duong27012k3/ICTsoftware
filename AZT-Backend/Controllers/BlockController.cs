using AZT_Backend.Controllers;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UseCase;


namespace AZT_Backend.Controllers
{
    public class BlockController : BaseController
    {
        private readonly BlockListManager _blockManager;
        private readonly BlockTransListManager _blockTransManager;
        private readonly ServiceListManager _serviceManager;
        private readonly ProjectListManager _projectManager;
        private readonly IWebHostEnvironment _env;

        public BlockController(
            JwtService jwtService,
            BlockListManager blockManager,
            BlockTransListManager blockTransManager,
            ServiceListManager serviceManager,
            ProjectListManager projectManager,
            IWebHostEnvironment env)
            : base(jwtService)
        {
            _blockManager = blockManager;
            _blockTransManager = blockTransManager;
            _serviceManager = serviceManager;
            _projectManager = projectManager;
            _env = env;
        }

        // GET /Block?ownerType=service&ownerId=3
        public IActionResult Index(string ownerType, int ownerId)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();

            ViewBag.OwnerType = ownerType;
            ViewBag.OwnerId = ownerId;
            ViewBag.OwnerName = GetOwnerName(ownerType, ownerId);
            return View(_blockManager.GetBlocksByOwnerType(ownerType, ownerId));
        }

        // GET /Block/Create?ownerType=service&ownerId=3
        public IActionResult Create(string ownerType, int ownerId)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();

            ViewBag.OwnerType = ownerType;
            ViewBag.OwnerId = ownerId;
            ViewBag.OwnerName = GetOwnerName(ownerType, ownerId);
            LoadBlockTypeDropdown();
            return View(new Block { OwnerType = ownerType, OwnerId = ownerId });
        }

        // POST /Block/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Block model, IFormFile? imageFile)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();

            if (!ModelState.IsValid)
            {
                ViewBag.OwnerType = model.OwnerType;
                ViewBag.OwnerId = model.OwnerId;
                ViewBag.OwnerName = GetOwnerName(model.OwnerType, model.OwnerId);
                LoadBlockTypeDropdown(model.BlockType);
                return View(model);
            }
            try
            {
                if (imageFile != null)
                    model.ImageUrl = await FileUploadHelper.SaveImageAsync(imageFile, _env);

                _blockManager.AddBlock(model);
                TempData["Success"] = "Thêm block thành công!";
                return RedirectToAction(nameof(Index),
                    new { ownerType = model.OwnerType, ownerId = model.OwnerId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.OwnerType = model.OwnerType;
                ViewBag.OwnerId = model.OwnerId;
                ViewBag.OwnerName = GetOwnerName(model.OwnerType, model.OwnerId);
                LoadBlockTypeDropdown(model.BlockType);
                return View(model);
            }
        }

        // GET /Block/Edit/5
        public IActionResult Edit(int id)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();

            var block = _blockManager.GetBlockByID(id);
            if (block == null) return NotFound();

            ViewBag.OwnerType = block.OwnerType;
            ViewBag.OwnerId = block.OwnerId;
            ViewBag.OwnerName = GetOwnerName(block.OwnerType, block.OwnerId);
            LoadBlockTypeDropdown(block.BlockType);
            return View(block);
        }

        // POST /Block/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Block model, IFormFile? imageFile)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();

            model.BlockId = id;
            if (!ModelState.IsValid)
            {
                model.BlockTrans = _blockTransManager.GetTransByBlockID(id).ToList();
                ViewBag.OwnerType = model.OwnerType;
                ViewBag.OwnerId = model.OwnerId;
                ViewBag.OwnerName = GetOwnerName(model.OwnerType, model.OwnerId);
                LoadBlockTypeDropdown(model.BlockType);
                return View(model);
            }
            try
            {
                if (imageFile != null)
                {
                    // Xóa ảnh cũ trước khi lưu ảnh mới
                    var old = _blockManager.GetBlockByID(id)?.ImageUrl;
                    FileUploadHelper.DeleteFile(old, _env);
                    model.ImageUrl = await FileUploadHelper.SaveImageAsync(imageFile, _env);
                }

                _blockManager.UpdateBlock(model);
                TempData["Success"] = "Cập nhật block thành công!";
                return RedirectToAction(nameof(Index),
                    new { ownerType = model.OwnerType, ownerId = model.OwnerId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                model.BlockTrans = _blockTransManager.GetTransByBlockID(id).ToList();
                LoadBlockTypeDropdown(model.BlockType);
                return View(model);
            }
        }

        // POST /Block/Delete
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(int id, string ownerType, int ownerId)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();
            try
            {
                // Xóa ảnh trên server trước khi xóa record
                var block = _blockManager.GetBlockByID(id);
                FileUploadHelper.DeleteFile(block?.ImageUrl, _env);
                _blockManager.DeleteBlock(id);
                TempData["Success"] = "Xóa block thành công!";
            }
            catch (Exception ex) { TempData["Error"] = ex.Message; }
            return RedirectToAction(nameof(Index), new { ownerType, ownerId });
        }

        // POST /Block/MoveUp
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult MoveUp(int id, string ownerType, int ownerId)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();
            _blockManager.MoveBlockUp(id);
            return RedirectToAction(nameof(Index), new { ownerType, ownerId });
        }

        // POST /Block/MoveDown
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult MoveDown(int id, string ownerType, int ownerId)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();
            _blockManager.MoveBlockDown(id);
            return RedirectToAction(nameof(Index), new { ownerType, ownerId });
        }

        // POST /Block/AddTrans
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddTrans(BlockTrans model)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();
            try { _blockTransManager.AddTrans(model); TempData["Success"] = "Thêm nội dung thành công!"; }
            catch (Exception ex) { TempData["Error"] = ex.Message; }
            return RedirectToAction(nameof(Edit), new { id = model.BlockId });
        }

        // POST /Block/DeleteTrans
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult DeleteTrans(int transId, int blockId)
        {
            if (!IsLoggedIn) return RequireLogin();
            if (!IsAdmin) return RequireAdmin();
            try { _blockTransManager.DeleteTrans(transId); TempData["Success"] = "Xóa nội dung thành công!"; }
            catch (Exception ex) { TempData["Error"] = ex.Message; }
            return RedirectToAction(nameof(Edit), new { id = blockId });
        }

        // ── HELPERS ──────────────────────────────────────────
        private string GetOwnerName(string ownerType, int ownerId)
        {
            if (ownerType == "service")
            {
                var s = _serviceManager.GetServiceByID(ownerId);
                return s?.ServiceTrans.FirstOrDefault(t => t.LangCode == "en")?.Name
                       ?? $"Service #{ownerId}";
            }
            var p = _projectManager.GetProjectByID(ownerId);
            return p?.ProjectTrans.FirstOrDefault(t => t.LangCode == "en")?.Name
                   ?? $"Project #{ownerId}";
        }

        private void LoadBlockTypeDropdown(string selected = "")
        {
            ViewBag.BlockTypeList = new SelectList(new[]
            {
                new { V = "banner",  T = "Banner" },
                new { V = "text",    T = "Text" },
                new { V = "image",   T = "Image" },
                new { V = "video",   T = "Video" },
                new { V = "gallery", T = "Gallery" },
                new { V = "cta",     T = "CTA Button" },
            }, "V", "T", selected);
        }
    }
}
