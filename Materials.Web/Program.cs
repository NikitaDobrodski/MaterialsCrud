using Materials.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------
// Services
// ---------------------------
builder.Services.AddRazorPages();

builder.Services.AddDbContext<MaterialsDbContext>(options =>
    options.UseSqlite("Data Source=materials.db"));

var app = builder.Build();

// ---------------------------
// Middleware pipeline
// ---------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ---------------------------
// Инициализация БД + сидинг словарей
// ---------------------------
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MaterialsDbContext>();
    await DbSeeder.SeedAsync(db);
}

// ---------------------------
app.MapRazorPages();

app.Run();