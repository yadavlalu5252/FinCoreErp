using FinCoreErp.Models;

namespace FinCoreErp.Repository
{
    public interface IVendorDocumentRepository
    {
        Task<List<Document>> GetVendorDocuments(int vendorId);
        Task AddDocument(Document document);
        Task DeleteDocument(int id);
    }
}