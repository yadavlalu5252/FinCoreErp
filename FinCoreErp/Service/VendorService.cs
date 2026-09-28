using Microsoft.EntityFrameworkCore;
using FinCoreErp.Data;
using FinCoreErp.Models;
using FinCoreErp.Repository;

namespace FinCoreErp.Service
{
    public class VendorService : IVendorRepository
    {
        AppDbContext db;

        public VendorService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Vendor>> GetAllVendors()
        {
            return await db.Vendors.Include(v => v.VendorCategory).Include(v => v.Company).ToListAsync();
        }

        public async Task<Vendor> GetVendorById(int id)
        {
            return await db.Vendors.Include(v => v.VendorCategory).Include(v => v.Company).FirstOrDefaultAsync(v => v.VendorId == id);
        }

        public async Task AddVendor(Vendor vendor)
        {
            await db.Vendors.AddAsync(vendor);
            await db.SaveChangesAsync();
        }

        public async Task UpdateVendor(Vendor vendor)
        {
            var data = await db.Vendors.FirstOrDefaultAsync(v => v.VendorId == vendor.VendorId);

            if (data != null)
            {
                data.VendorCode = vendor.VendorCode;
                data.VendorCategoryId = vendor.VendorCategoryId;
                data.CompanyId = vendor.CompanyId;
                data.BankAccount = vendor.BankAccount;
                data.PAN = vendor.PAN;
                data.PerformanceScore = vendor.PerformanceScore;
                data.IsVerified = vendor.IsVerified;
                data.IsActive = vendor.IsActive;
                data.ModifiedBy = 1;
                data.ModifiedAt = DateTime.Now;
                await db.SaveChangesAsync();
            }
        }
    }
}