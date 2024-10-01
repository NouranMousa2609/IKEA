using LinkDev.IKEA.BLL.Common.Services.Attachments;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.BLL.Services.Employees;
using LinkDev.IKEA.DAL.Entities.Identity;
using LinkDev.IKEA.DAL.Persistance.Data;
using LinkDev.IKEA.DAL.Persistance.Repositories.Departments;
using LinkDev.IKEA.DAL.Persistance.Repositories.Employees;
using LinkDev.IKEA.DAL.Persistance.UnitOfwork;
using LinkDev.IKEA.PL.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection;

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
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }/*,contextLifetime:ServiceLifetime.Scoped,optionsLifetime:ServiceLifetime.Scoped*/
            );
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddTransient<IAttachmentService,AttachmentService>();
            builder.Services.AddScoped<IDepartmentService,DepartmentService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            //builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));
            builder.Services.AddAutoMapper(M=>M.AddProfile(new MappingProfile()));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireDigit = true;
                //options.Password.RequireLowercase = true;
                //options.Password.RequireUppercase = true;
                //options.Password.RequireNonAlphanumeric = true;
                //options.Password.RequiredUniqueChars = 1;

                options.User.RequireUniqueEmail = true;

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(5);


            }).AddEntityFrameworkStores<ApplicationDbContext>();
			//builder.Services.AddScoped<UserManager<ApplicationUser>>();
			//builder.Services.AddScoped<SignInManager<ApplicationUser>>();
			//builder.Services.AddScoped<RoleManager<IdentityRole>>();



			builder.Services.ConfigureApplicationCookie(option =>
            {
                option.LoginPath = "/Account/SignIn";
                option.AccessDeniedPath = "/Home/Error";
                option.ExpireTimeSpan = TimeSpan.FromDays(5);
                option.LogoutPath = "/Account/SignIn";
            });

            builder.Services.AddAuthentication();
            builder.Services.AddAuthentication("Identity.Apllication");
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Identity.Apllication";
                options.DefaultChallengeScheme = "Identity.Apllication";

            });
			//.AddCookie("Hamada", ".ASPNetCore.Hamada", option=>
			//{
			//option.LoginPath = "/Account/SignIn";
			//option.AccessDeniedPath = "/Home/Error";
			//option.ExpireTimeSpan = TimeSpan.FromDays(5);
			//option.LogoutPath = "/Account/SignIn";

			//});

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
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            #endregion

            app.Run();
        }
    }
}
