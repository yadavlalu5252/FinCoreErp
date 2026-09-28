using FinCoreErp.DTO.Asset;

namespace FinCoreErp.Repository
{
    public interface IAssetDisposalService
    {
        Task<bool> DisposeAssetAsync(AssetDisposalDto dto);
    }
}