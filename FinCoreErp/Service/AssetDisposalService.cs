using FinCoreErp.Data;
using FinCoreErp.DTO.Asset;
using FinCoreErp.Repository;
using Microsoft.EntityFrameworkCore;

namespace FinCoreErp.Service
{
    public class AssetDisposalService : IAssetDisposalService
    {
        private readonly AppDbContext db;

        public AssetDisposalService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> DisposeAssetAsync(AssetDisposalDto dto)
        {
            var asset = await db.Assets
                .FirstOrDefaultAsync(x => x.AssetId == dto.AssetId);

            if (asset == null)
            {
                return false;
            }

            asset.Status = "Disposed";
            asset.ModifiedAt = DateTime.Now;

            await db.SaveChangesAsync();

            return true;
        }
    }
}