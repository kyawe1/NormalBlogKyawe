using BlogProject.Data;
using BlogProject.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BlogProject.Services.Interfaces;
using BlogProject.Services.Worker;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DefaultDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultDbContext"));
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IPictureStore, BlogPictureStore>();

builder.Services.AddDefaultIdentity<ApplicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<DefaultDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    //app.Use(async (context,next) =>
    //{
    //    await next();
    //    if (context.Response.StatusCode == 404)
    //    {
            
    //    }
    //})
}

//app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
