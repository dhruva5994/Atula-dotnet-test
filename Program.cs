using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PracticalDemo.Models;


namespace PracticalDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           
            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            // builder.Services.AddControllersWithViews()
            //.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginViewModelValidator>());


            builder.Services.AddControllersWithViews();
           // builder.Services.AddControllersWithViews()
            //.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginViewModelValidator>());

            //builder.Services.AddTransient<IValidator<LoginViewModel>, LoginViewModelValidator>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");
            //app.MapRazorPages();

            app.Run();
        }
    }
}
