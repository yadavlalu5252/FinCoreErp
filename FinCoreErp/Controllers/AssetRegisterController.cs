using FinCoreErp.DTO.Asset;
using FinCoreErp.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinCoreErp.Controllers
{
    public class AssetRegisterController : Controller
    {
        private readonly IAssetRegisterService service;

        public AssetRegisterController(IAssetRegisterService service)
        {
            this.service = service;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Vendor
            var vendors = await service.GetVendors();

            ViewBag.Vendors = new SelectList(
                vendors,
                "VendorId",
                "VendorCode"
            );


            // Department
            var departments = await service.GetDepartments();

            ViewBag.Departments = new SelectList(
                departments,
                "DepartmentId",
                "DepartmentName"
            );


            // CAPEX Request
            var capexRequests = await service.GetCapexRequests();

            ViewBag.CapexRequests = new SelectList(
                capexRequests,
                "CapexRequestId",
                "Title"
            );


            // Purchase Order
            var purchaseOrders = await service.GetPurchaseOrders();

            ViewBag.PurchaseOrders = new SelectList(
                purchaseOrders,
                "POId",
                "POCode"
            );


            // GRN
            var grns = await service.GetGRNs();

            ViewBag.GRNs = new SelectList(
                grns,
                "GRNId",
                "GRNCode"
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
                "Text"
            );


            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(AssetRegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                // Vendor
                var vendors = await service.GetVendors();

                ViewBag.Vendors = new SelectList(
                    vendors,
                    "VendorId",
                    "VendorCode",
                    dto.VendorId
                );


                // Department
                var departments = await service.GetDepartments();

                ViewBag.Departments = new SelectList(
                    departments,
                    "DepartmentId",
                    "DepartmentName",
                    dto.DepartmentId
                );


                // CAPEX Request
                var capexRequests = await service.GetCapexRequests();

                ViewBag.CapexRequests = new SelectList(
                    capexRequests,
                    "CapexRequestId",
                    "Title",
                    dto.CapexRequestId
                );


                // Purchase Order
                var purchaseOrders = await service.GetPurchaseOrders();

                ViewBag.PurchaseOrders = new SelectList(
                    purchaseOrders,
                    "POId",
                    "POCode",
                    dto.PurchaseOrderId
                );


                // GRN
                var grns = await service.GetGRNs();

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


            var result = await service.RegisterAsset(dto);

            if (result)
            {
                return RedirectToAction("Index", "AssetList");
            }


            return View(dto);
        }
    }
}