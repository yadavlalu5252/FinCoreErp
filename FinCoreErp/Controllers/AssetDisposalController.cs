using FinCoreErp.DTO.Asset;
using FinCoreErp.Repository;
using FinCoreErp.Service;
using Microsoft.AspNetCore.Mvc;

namespace FinCoreErp.Controllers
{
    public class AssetDisposalController : Controller
    {
        private readonly IAssetDisposalService service;

        public AssetDisposalController(IAssetDisposalService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            var dto = new AssetDisposalDto
            {
                AssetId = id,
                DisposalDate = DateTime.Now
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Index(AssetDisposalDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var result = await service.DisposeAssetAsync(dto);

            if (result)
            {
                return RedirectToAction("Index", "AssetList");
            }

            return NotFound();
        }
    }
}