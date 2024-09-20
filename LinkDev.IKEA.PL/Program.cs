using LinkDev.IKEA.DAL.Persistance.Data;
using LinkDev.IKEA.DAL.Persistance.Repositories.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LinkDev.IKEA.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Service

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(
            //contextLifetime:ServiceLifetime.Scoped,
            //optionsLifetime:ServiceLifetime.Scoped,
            (optionsBuilder)=>
            {
                optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }/*,contextLifetime:ServiceLifetime.Scoped,optionsLifetime:ServiceLifetime.Scoped*/
            );
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            ///builder.Services.AddScoped<ApplicationDbContext>();
            ///builder.Services.AddScoped<DbContextOptions<ApplicationDbContext>>((ServiceProvider) =>
            ///{
            ///    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            ///
            ///    optionsBuilder.UseSqlServer("");
            ///
            ///    var option=optionsBuilder.Options;
            ///
            ///    return option;
            ///});
            
            #endregion

            var app = builder.Build();

            #region Configure Kestrel Middlewares

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            #endregion

            app.Run();
        }
    }
}
