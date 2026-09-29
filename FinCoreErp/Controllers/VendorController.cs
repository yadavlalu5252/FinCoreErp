using FinCoreErp.Data;
using FinCoreErp.Filter;
using FinCoreErp.Models;
using FinCoreErp.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinCoreErp.Controllers
{
    [SessionAuthorize]
    [RoleAuthorize("Administrator")]
    public class VendorController : Controller
    {
        IVendorRepository vendorService;
        AppDbContext db;

        public VendorController(IVendorRepository vendorService, AppDbContext db)
        {
            this.vendorService = vendorService;
            this.db = db;
        }

        public async Task<IActionResult> Index()
        {
            var data = await vendorService.GetAllVendors();
            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await db.VendorCategories.Where(x => x.IsActive == 1).ToListAsync();
            var companies = await db.Companies.ToListAsync();
            ViewBag.VendorCategories = new SelectList(categories,"VendorCategoryId","CategoryName");
            ViewBag.Companies = new SelectList(companies, "CompanyId", "CompanyCode");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Vendor v)
        {
            v.CreatedBy = 1;
            v.ModifiedBy = 1;
            v.CreatedAt = DateTime.Now;
            v.ModifiedAt = DateTime.Now;
            await vendorService.AddVendor(v);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var vendor = await vendorService.GetVendorById(id);
            var categories = await db.VendorCategories.Where(x => x.IsActive == 1).ToListAsync();
            var companies = await db.Companies.ToListAsync();
            ViewBag.VendorCategories = new SelectList( categories, "VendorCategoryId", "CategoryName", vendor.VendorCategoryId);
            ViewBag.Companies = new SelectList(companies,"CompanyId","CompanyCode",vendor.CompanyId);
            return View(vendor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Vendor v)
        {
            await vendorService.UpdateVendor(v);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var vendor = await vendorService.GetVendorById(id);
            return View(vendor);
        }
    }
}