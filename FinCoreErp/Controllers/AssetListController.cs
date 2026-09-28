using FinCoreErp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FinCoreErp.Controllers
{
    public class AssetListController : Controller
    {
        private readonly IAssetListService assetListService;

        public AssetListController(IAssetListService assetListService)
        {
            this.assetListService = assetListService;
        }

        public async Task<IActionResult> Index()
        {
            var assets = await assetListService.GetAssetListAsync();

            return View(assets);
        }
    }
}