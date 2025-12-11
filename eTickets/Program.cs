using eTickets.Data.Persistence;
using eTickets.Data.Seed;
using Microsoft.EntityFrameworkCore;

namespace eTickets
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Add DbContext (SQL Server)
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnectionString")
                )
            );

            // 2. Add MVC
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // 3. Default route
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            // SEED DATABASE WITH CANCELLATION TOKEN
            using (var scope = app.Services.CreateScope())
            {
                var lifetime = scope.ServiceProvider.GetRequiredService<IHostApplicationLifetime>();
                CancellationToken ct = lifetime.ApplicationStopping;

                await AppDbInitializer.SeedAsync(app, ct);
            }

            app.Run();
        }
    }
}
