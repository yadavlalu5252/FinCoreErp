using FinCoreErp.Data;
using FinCoreErp.DTO.Asset;
using FinCoreErp.Models;
using FinCoreErp.Repository;
using Microsoft.EntityFrameworkCore;

namespace FinCoreErp.Service
{
    public class AssetRegisterService : IAssetRegisterService
    {
        private readonly AppDbContext db;

        public AssetRegisterService(AppDbContext db)
        {
            this.db = db;
        }


        // Vendor Dropdown
        public async Task<List<Vendor>> GetVendors()
        {
            var data = await db.Vendors
                .Where(x => x.IsActive == 1)
                .ToListAsync();

            return data;
        }


        // Department Dropdown
        public async Task<List<Department>> GetDepartments()
        {
            var data = await db.Departments
                .Where(x => x.IsActive == 1)
                .ToListAsync();

            return data;
        }


        // CAPEX Request Dropdown
        public async Task<List<CapexRequest>> GetCapexRequests()
        {
            var data = await db.CapexRequests
                .ToListAsync();

            return data;
        }


        // Purchase Order Dropdown
        public async Task<List<PurchaseOrder>> GetPurchaseOrders()
        {
            var data = await db.PurchaseOrders
                .Where(x => x.IsActive == 1)
                .ToListAsync();

            return data;
        }


        // GRN Dropdown
        public async Task<List<GRN>> GetGRNs()
        {
            var data = await db.GRNs
                .Where(x => x.IsActive == 1)
                .ToListAsync();

            return data;
        }


        // Register Asset
        public async Task<bool> RegisterAsset(AssetRegisterDto dto)
        {
            Asset asset = new Asset
            {
                AssetCode = dto.AssetCode,
                AssetName = dto.AssetName,

                CapexRequestId = dto.CapexRequestId,
                PurchaseOrderId = dto.PurchaseOrderId,
                GRNId = dto.GRNId,

                VendorId = dto.VendorId,
                DepartmentId = dto.DepartmentId,

                PurchaseDate = dto.PurchaseDate,
                PurchaseCost = dto.PurchaseCost,

                Status = dto.Status,

                CreatedAt = DateTime.Now
            };

            db.Assets.Add(asset);

            await db.SaveChangesAsync();

            return true;
        }
    }
}