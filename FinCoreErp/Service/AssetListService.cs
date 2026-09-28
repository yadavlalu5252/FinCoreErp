using FinCoreErp.Data;
using FinCoreErp.DTO.Asset;
using FinCoreErp.Repository;
using Microsoft.EntityFrameworkCore;

namespace FinCoreErp.Service
{
    public class AssetListService : IAssetListService
    {
        private readonly AppDbContext db;

        public AssetListService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> DeleteAssetAsync(int id)
        {
            var asset = await db.Assets
       .FirstOrDefaultAsync(x => x.AssetId == id);

            if (asset == null)
            {
                return false;
            }

            db.Assets.Remove(asset);

            await db.SaveChangesAsync();

            return true;
        }

        // Asset Details
        public async Task<AssetDetailsDto> GetAssetDetailsAsync(int id)
        {
            var asset = await db.Assets
                .Include(x => x.Vendor)
                .Include(x => x.Department)
                .Include(x => x.CapexRequest)
                .Include(x => x.PurchaseOrder)
                .Include(x => x.GRN)
                .Where(x => x.AssetId == id)
                .Select(x => new AssetDetailsDto
                {
                    AssetId = x.AssetId,

                    AssetCode = x.AssetCode,

                    AssetName = x.AssetName,

                    VendorCode = x.Vendor != null
                        ? x.Vendor.VendorCode
                        : "-",

                    DepartmentName = x.Department != null
                        ? x.Department.DepartmentName
                        : "-",

                    CapexRequestTitle = x.CapexRequest != null
                        ? x.CapexRequest.Title
                        : "-",

                    POCode = x.PurchaseOrder != null
                        ? x.PurchaseOrder.POCode
                        : "-",

                    GRNCode = x.GRN != null
                        ? x.GRN.GRNCode
                        : "-",

                    PurchaseDate = x.PurchaseDate,

                    PurchaseCost = x.PurchaseCost,

                    Status = x.Status,

                    CreatedAt = x.CreatedAt,

                    ModifiedAt = x.ModifiedAt
                })
                .FirstOrDefaultAsync();

            return asset;
        }


        // Get Asset For Edit
        public async Task<AssetEditDto> GetAssetForEditAsync(int id)
        {
            var asset = await db.Assets
                .Where(x => x.AssetId == id)
                .Select(x => new AssetEditDto
                {
                    AssetId = x.AssetId,

                    AssetCode = x.AssetCode,

                    AssetName = x.AssetName,

                    CapexRequestId = x.CapexRequestId,

                    PurchaseOrderId = x.PurchaseOrderId,

                    GRNId = x.GRNId,

                    VendorId = x.VendorId,

                    DepartmentId = x.DepartmentId,

                    PurchaseDate = x.PurchaseDate,

                    PurchaseCost = x.PurchaseCost,

                    Status = x.Status
                })
                .FirstOrDefaultAsync();

            return asset;
        }


        // Asset List
        public async Task<List<AssetListDto>> GetAssetListAsync()
        {
            var assets = await db.Assets
                .Include(x => x.Vendor)
                .Include(x => x.Department)
                .Select(x => new AssetListDto
                {
                    AssetId = x.AssetId,

                    AssetCode = x.AssetCode,

                    AssetName = x.AssetName,

                    VendorCode = x.Vendor != null
                        ? x.Vendor.VendorCode
                        : "-",

                    DepartmentName = x.Department != null
                        ? x.Department.DepartmentName
                        : "-",

                    PurchaseDate = x.PurchaseDate,

                    PurchaseCost = x.PurchaseCost,

                    Status = x.Status
                })
                .ToListAsync();

            return assets;
        }


        // Update Asset
        public async Task<bool> UpdateAssetAsync(AssetEditDto dto)
        {
            var asset = await db.Assets
                .FirstOrDefaultAsync(x => x.AssetId == dto.AssetId);

            if (asset == null)
            {
                return false;
            }

            asset.AssetCode = dto.AssetCode;

            asset.AssetName = dto.AssetName;

            asset.CapexRequestId = dto.CapexRequestId;

            asset.PurchaseOrderId = dto.PurchaseOrderId;

            asset.GRNId = dto.GRNId;

            asset.VendorId = dto.VendorId;

            asset.DepartmentId = dto.DepartmentId;

            asset.PurchaseDate = dto.PurchaseDate;

            asset.PurchaseCost = dto.PurchaseCost;

            asset.Status = dto.Status;

            asset.ModifiedAt = DateTime.Now;

            await db.SaveChangesAsync();

            return true;
        }
    }
}