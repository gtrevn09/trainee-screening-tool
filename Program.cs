using Microsoft.EntityFrameworkCore;
using TraineeScreeningTool.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Add services to the container.
builder.Services.AddControllersWithViews();

// 2. Register the SQLite Database Context
// This tells the app to use "trainees.db" as the local SQL Lite file
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=trainees.db"));

var app = builder.Build();

// 3. AUTOMATIC DATABASE CREATION
// This creates the .db file and tables automatically if they don't exist
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();