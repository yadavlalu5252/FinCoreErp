using FinCoreErp.DTO.Asset;
using FinCoreErp.Models;

namespace FinCoreErp.Repository
{
    public interface IAssetRegisterService
    {
        Task<bool> RegisterAsset(AssetRegisterDto dto);

        Task<List<Vendor>> GetVendors();

        Task<List<Department>> GetDepartments();

        Task<List<CapexRequest>> GetCapexRequests();

        Task<List<PurchaseOrder>> GetPurchaseOrders();

        Task<List<GRN>> GetGRNs();
    }
}