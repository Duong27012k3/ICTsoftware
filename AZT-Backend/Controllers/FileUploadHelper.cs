namespace AZT_Backend.Controllers
{
    public static class FileUploadHelper
    {
        private static readonly string[] AllowedImages = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        private static readonly string[] AllowedCatalogue = { ".pdf" };
        private const long MaxImageSize = 5 * 1024 * 1024;
        private const long MaxCatalogueSize = 20 * 1024 * 1024;

        public static async Task<string?> SaveImageAsync(IFormFile? file, IWebHostEnvironment env)
        {
            if (file == null || file.Length == 0) return null;
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!AllowedImages.Contains(ext))
                throw new Exception($"Chỉ cho phép: {string.Join(", ", AllowedImages)}");
            if (file.Length > MaxImageSize)
                throw new Exception("Ảnh phải nhỏ hơn 5MB.");
            var folder = Path.Combine(env.WebRootPath, "uploads", "images");
            Directory.CreateDirectory(folder);
            var fileName = $"{Guid.NewGuid()}{ext}";
            using var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create);
            await file.CopyToAsync(stream);
            return $"/uploads/images/{fileName}";
        }

        public static async Task<string?> SaveCatalogueAsync(IFormFile? file, IWebHostEnvironment env)
        {
            if (file == null || file.Length == 0) return null;
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!AllowedCatalogue.Contains(ext))
                throw new Exception("Chỉ cho phép file PDF.");
            if (file.Length > MaxCatalogueSize)
                throw new Exception("File PDF phải nhỏ hơn 20MB.");
            var folder = Path.Combine(env.WebRootPath, "uploads", "catalogues");
            Directory.CreateDirectory(folder);
            var fileName = $"{Guid.NewGuid()}.pdf";
            using var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create);
            await file.CopyToAsync(stream);
            return $"/uploads/catalogues/{fileName}";
        }

        public static void DeleteFile(string? relativePath, IWebHostEnvironment env)
        {
            if (string.IsNullOrEmpty(relativePath)) return;
            var full = Path.Combine(env.WebRootPath, relativePath.TrimStart('/'));
            if (File.Exists(full)) File.Delete(full);
        }
    }
}
