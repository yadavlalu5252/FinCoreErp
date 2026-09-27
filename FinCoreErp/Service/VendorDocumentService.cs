using FinCoreErp.Data;
using FinCoreErp.Models;
using FinCoreErp.Repository;
using Microsoft.EntityFrameworkCore;

namespace FinCoreErp.Service
{
    public class VendorDocumentService : IVendorDocumentRepository
    {
        AppDbContext db;

        public VendorDocumentService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Document>> GetVendorDocuments(int vendorId)
        {
            return await db.Documents.Include(d => d.DocumentType).Where(d => d.EntityId == vendorId).ToListAsync();
        }

        public async Task AddDocument(Document document)
        {
            await db.Documents.AddAsync(document);
            await db.SaveChangesAsync();
        }

        public async Task DeleteDocument(int id)
        {
            var document = await db.Documents.FindAsync(id);
            if (document != null)
            {
                db.Documents.Remove(document);
                await db.SaveChangesAsync();
            }
        }
    }
}