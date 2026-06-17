using LeitorDeNotas.ClearArch.IoC;
using LeitorDeNotas.ClearArch.WebApp.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLeitorDeNotasServices();

builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<BatchHub>("/batchHub");

app.Run();
