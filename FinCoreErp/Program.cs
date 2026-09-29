using FinCoreErp.Data;
using FinCoreErp.Repository;
using FinCoreErp.Repository.Budget;
using FinCoreErp.Service;
using FinCoreErp.Service.Budget;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(
    builder.Configuration.GetConnectionString("dbconn")
    ));


builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAssetListService, AssetListService>();
builder.Services.AddScoped<IAssetRegisterService, AssetRegisterService>();
builder.Services.AddScoped<IAssetDisposalService, AssetDisposalService>();
builder.Services.AddScoped<IBudgetCategoryService, BudgetCategoryService>();
builder.Services.AddScoped<IBudgetService, BudgetService>();
builder.Services.AddScoped<IBudgetLineService, BudgetLineService>();
builder.Services.AddScoped<IVendorDocumentRepository, VendorDocumentService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IVendorRepository, VendorService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Register}/{id?}")
    .WithStaticAssets();


app.Run();
