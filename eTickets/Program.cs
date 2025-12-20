using eTickets.Data.Persistence;
using eTickets.Data.Seed;
using eTickets.Data.Services;
using Microsoft.EntityFrameworkCore;

namespace eTickets
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Create WebApplication builder
            var builder = WebApplication.CreateBuilder(args);


            // Add MVC
            builder.Services.AddControllersWithViews();


            // Add DbContext (SQL Server)
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnectionString")
                )
            );

            // Register Services for Dependency Injection
            builder.Services.AddScoped<IActorService, ActorService>();

            // Build the app
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
                var ct = lifetime.ApplicationStopping;

                await AppDbInitializer.SeedAsync(app, ct);
            }

            app.Run();
        }
    }
}
