using FinCoreErp.DTO.Asset;
using FinCoreErp.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinCoreErp.Controllers
{
    public class AssetController : Controller
    {
        private readonly IAssetListService assetListService;
        private readonly IAssetRegisterService assetRegisterService;

        public AssetController(
            IAssetListService assetListService,
            IAssetRegisterService assetRegisterService)
        {
            this.assetListService = assetListService;
            this.assetRegisterService = assetRegisterService;
        }


        // Asset Details
        public async Task<IActionResult> Details(int id)
        {
            var asset = await assetListService.GetAssetDetailsAsync(id);

            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }


        // Edit Asset - GET
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var asset = await assetListService.GetAssetForEditAsync(id);

            if (asset == null)
            {
                return NotFound();
            }

            // Vendor Dropdown
            var vendors = await assetRegisterService.GetVendors();

            ViewBag.Vendors = new SelectList(
                vendors,
                "VendorId",
                "VendorCode",
                asset.VendorId
            );


            // Department Dropdown
            var departments = await assetRegisterService.GetDepartments();

            ViewBag.Departments = new SelectList(
                departments,
                "DepartmentId",
                "DepartmentName",
                asset.DepartmentId
            );


            // CAPEX Request Dropdown
            var capexRequests = await assetRegisterService.GetCapexRequests();

            ViewBag.CapexRequests = new SelectList(
                capexRequests,
                "CapexRequestId",
                "Title",
                asset.CapexRequestId
            );


            // Purchase Order Dropdown
            var purchaseOrders = await assetRegisterService.GetPurchaseOrders();

            ViewBag.PurchaseOrders = new SelectList(
                purchaseOrders,
                "POId",
                "POCode",
                asset.PurchaseOrderId
            );


            // GRN Dropdown
            var grns = await assetRegisterService.GetGRNs();

            ViewBag.GRNs = new SelectList(
                grns,
                "GRNId",
                "GRNCode",
                asset.GRNId
            );


            // Status - Hardcoded
            var status = new[]
            {
                new { Value = "Active", Text = "Active" },
                new { Value = "Inactive", Text = "Inactive" }
            };

            ViewBag.Status = new SelectList(
                status,
                "Value",
                "Text",
                asset.Status
            );


            return View(asset);
        }


        // Edit Asset - POST
        [HttpPost]
        public async Task<IActionResult> Edit(AssetEditDto dto)
        {
            if (!ModelState.IsValid)
            {
                // Vendor Dropdown
                var vendors = await assetRegisterService.GetVendors();

                ViewBag.Vendors = new SelectList(
                    vendors,
                    "VendorId",
                    "VendorCode",
                    dto.VendorId
                );


                // Department Dropdown
                var departments = await assetRegisterService.GetDepartments();

                ViewBag.Departments = new SelectList(
                    departments,
                    "DepartmentId",
                    "DepartmentName",
                    dto.DepartmentId
                );


                // CAPEX Request Dropdown
                var capexRequests = await assetRegisterService.GetCapexRequests();

                ViewBag.CapexRequests = new SelectList(
                    capexRequests,
                    "CapexRequestId",
                    "Title",
                    dto.CapexRequestId
                );


                // Purchase Order Dropdown
                var purchaseOrders = await assetRegisterService.GetPurchaseOrders();

                ViewBag.PurchaseOrders = new SelectList(
                    purchaseOrders,
                    "POId",
                    "POCode",
                    dto.PurchaseOrderId
                );


                // GRN Dropdown
                var grns = await assetRegisterService.GetGRNs();

                ViewBag.GRNs = new SelectList(
                    grns,
                    "GRNId",
                    "GRNCode",
                    dto.GRNId
                );


                // Status - Hardcoded
                var status = new[]
                {
                    new { Value = "Active", Text = "Active" },
                    new { Value = "Inactive", Text = "Inactive" }
                };

                ViewBag.Status = new SelectList(
                    status,
                    "Value",
                    "Text",
                    dto.Status
                );


                return View(dto);
            }


            var result = await assetListService.UpdateAssetAsync(dto);

            if (result)
            {
                return RedirectToAction("Index", "AssetList");
            }

            return NotFound();
        }
        // Delete Asset
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await assetListService.DeleteAssetAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "AssetList");
        }
    }
}