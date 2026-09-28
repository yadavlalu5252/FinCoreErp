using FinCoreErp.Models;

namespace FinCoreErp.Repository
{
    public interface IVendorRepository
    {
        Task<List<Vendor>> GetAllVendors();
        Task<Vendor> GetVendorById(int id);
        Task AddVendor(Vendor vendor);
        Task UpdateVendor(Vendor vendor);
    }
}