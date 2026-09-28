using FinCoreErp.DTO.Asset;

namespace FinCoreErp.Repository
{
    public interface IAssetListService
    {

        Task<List<AssetListDto>> GetAssetListAsync();
        Task<AssetDetailsDto> GetAssetDetailsAsync(int id);
        Task<AssetEditDto> GetAssetForEditAsync(int id);
        Task<bool> DeleteAssetAsync(int id);
        Task<bool> UpdateAssetAsync(AssetEditDto dto);
    }
}