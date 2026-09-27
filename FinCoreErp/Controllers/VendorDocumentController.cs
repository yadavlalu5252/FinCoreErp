using FinCoreErp.Data;
using FinCoreErp.Models;
using FinCoreErp.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinCoreErp.Controllers
{
    public class VendorDocumentController : Controller
    {
        IVendorDocumentRepository documentService;
        AppDbContext db;

        public VendorDocumentController(IVendorDocumentRepository documentService,AppDbContext db)
        {
            this.documentService = documentService;
            this.db = db;
        }

        public async Task<IActionResult> Index(int vendorId)
        {
            var documents = await documentService.GetVendorDocuments(vendorId);
            ViewBag.VendorId = vendorId;
            return View(documents);
        }

        public async Task<IActionResult> Create(int vendorId)
        {
            var documentTypes = await db.DocumentTypes.Where(x => x.IsActive == 1).ToListAsync();
            ViewBag.DocumentTypes = new SelectList(documentTypes,"DocumentTypeId","DocumentCategory");
            ViewBag.VendorId = vendorId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Document document,IFormFile file,int vendorId)
        {
            if (file != null)
            {
                string folder = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","uploads","vendors");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                string fileName = Guid.NewGuid().ToString()+ Path.GetExtension(file.FileName);
                string filePath = Path.Combine(folder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                document.EntityId = vendorId;
                document.FileName = file.FileName;
                document.FileType = file.ContentType;
                document.FilePath = "/uploads/vendors/" + fileName;
                document.MasterTypeId = await db.MasterTypes.Where(x => x.MasterTypeName == "Vendor").Select(x => x.MasterTypeId).FirstOrDefaultAsync();
                document.UserId = 1;
                document.CreatedAt = DateTime.Now;
                document.ModifiedAt = DateTime.Now;
                await documentService.AddDocument(document);
                return RedirectToAction("Index", new { vendorId = vendorId });
            }
            return View(document);
        }

        public async Task<IActionResult> Delete(int id, int vendorId)
        {
            var document = await db.Documents.FindAsync(id);
            if (document != null)
            {
                if (!string.IsNullOrEmpty(document.FilePath))
                {
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot",document.FilePath.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
                await documentService.DeleteDocument(id);
            }
            return RedirectToAction("Index", new { vendorId = vendorId });
        }
    }
}