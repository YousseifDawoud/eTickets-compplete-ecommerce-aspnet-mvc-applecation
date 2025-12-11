using Bogus;
using eTickets.Data.Enums;
using eTickets.Data.Persistence;
using eTickets.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Seed
{
    public class AppDbInitializer
    {
        // Seed method to initialize the database with default data
        public static async Task SeedAsync(IApplicationBuilder app, CancellationToken ct = default)
        {
            // Create a service scope to access the database context
            using var serviceScope = app.ApplicationServices.CreateScope();

            // Get the database context from the service provider
            var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

            // --------------------------------------
            // Apply Migrations FIRST (added)
            // --------------------------------------
            await context.Database.MigrateAsync(ct);

            // Ensure the database is created (kept as you requested)
            context.Database.EnsureCreated();

            // Initialize Faker for generating fake data
            var faker = new Faker("en");
            var random = new Random();

            #region Seed Cinemas

            if (!await context.Cinemas.AnyAsync(ct))
            {
                var cinemas = new List<Cinema>();
                for (int i = 1; i <= 5; i++) // Create 5 cinemas
                {
                    cinemas.Add(new Cinema
                    {
                        Name = $"Cinema {i}",
                        Logo = $"http://dotnethow.net/images/cinemas/cinema-{i}.jpeg",
                        Description = faker.Lorem.Sentence(10)
                    });
                }
                await context.Cinemas.AddRangeAsync(cinemas, ct);
                await context.SaveChangesAsync(ct);
            }

            #endregion

            #region Seed Actors

            if (!await context.Actors.AnyAsync(ct))
            {
                var actors = new List<Actor>();
                for (int i = 1; i <= 10; i++)
                {
                    actors.Add(new Actor
                    {
                        FullName = faker.Name.FullName(),
                        Bio = faker.Lorem.Sentence(12),
                        ProfilePictureURL = $"http://dotnethow.net/images/actors/actor-{i}.jpeg"
                    });
                }
                await context.Actors.AddRangeAsync(actors, ct);
                await context.SaveChangesAsync(ct);
            }

            #endregion

            #region Seed Producers

            if (!await context.Producers.AnyAsync(ct))
            {
                var producers = new List<Producer>();
                for (int i = 1; i <= 5; i++)
                {
                    producers.Add(new Producer
                    {
                        FullName = faker.Name.FullName(),
                        Bio = faker.Lorem.Sentence(15),
                        ProfilePictureURL = $"http://dotnethow.net/images/producers/producer-{i}.jpeg"
                    });
                }
                await context.Producers.AddRangeAsync(producers, ct);
                await context.SaveChangesAsync(ct);
            }

            #endregion

            #region Seed Movies
            if (!await context.Movies.AnyAsync(ct))
            {
                var cinemas = context.Cinemas.ToList();
                var producers = context.Producers.ToList();

                var movies = new List<Movie>();

                for (int i = 1; i <= 15; i++)
                {
                    var cinema = faker.PickRandom(cinemas);
                    var producer = faker.PickRandom(producers);

                    movies.Add(new Movie
                    {
                        Name = faker.Lorem.Word(),
                        Description = faker.Lorem.Sentence(20),
                        Price = faker.Random.Decimal(10, 50),
                        ImageUrl = $"http://dotnethow.net/images/movies/movie-{i}.jpeg",
                        StartDate = DateTime.Now.AddDays(faker.Random.Int(-10, 0)),
                        EndDate = DateTime.Now.AddDays(faker.Random.Int(1, 30)),
                        CinemaId = cinema.Id,
                        ProducerId = producer.Id,
                        MovieCategory = faker.PickRandom<MovieCategory>()
                    });
                }
                await context.Movies.AddRangeAsync(movies, ct);
                await context.SaveChangesAsync(ct);
            }

            #endregion

            #region Seed Actor_Movie (Many-to-Many)

            if (!await context.Actors_Movies.AnyAsync(ct))
            {
                var movies = context.Movies.ToList();
                var actors = context.Actors.ToList();

                var actorMovies = new List<Actor_Movie>();

                // For each movie, assign random actors
                foreach (var movie in movies)
                {
                    // Select 2 to 5 random actors for each movie
                    var selectedActors = faker.PickRandom(actors, faker.Random.Int(2, 5)).ToList();

                    // Create Actor_Movie entries
                    foreach (var actor in selectedActors)
                    {
                        // Avoid duplicate entries for the same actor and movie
                        actorMovies.Add(new Actor_Movie
                        {
                            ActorId = actor.Id,
                            MovieId = movie.Id
                        });
                    }
                }
                await context.Actors_Movies.AddRangeAsync(actorMovies, ct);
                await context.SaveChangesAsync(ct);
            }

            #endregion

            //#region Seed Identity Users and Roles

            //var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            //if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
            //    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            //if (!await roleManager.RoleExistsAsync(UserRoles.User))
            //    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            //string adminEmail = "admin@etickets.com";
            //if (await userManager.FindByEmailAsync(adminEmail) == null)
            //{
            //    var adminUser = new ApplicationUser
            //    {
            //        FullName = "Admin User",
            //        UserName = "admin-user",
            //        Email = adminEmail,
            //        EmailConfirmed = true
            //    };
            //    await userManager.CreateAsync(adminUser, "Coding@1234?");
            //    await userManager.AddToRoleAsync(adminUser, UserRoles.Admin);
            //}

            //string appUserEmail = "user@etickets.com";
            //if (await userManager.FindByEmailAsync(appUserEmail) == null)
            //{
            //    var appUser = new ApplicationUser
            //    {
            //        FullName = "Application User",
            //        UserName = "app-user",
            //        Email = appUserEmail,
            //        EmailConfirmed = true
            //    };
            //    await userManager.CreateAsync(appUser, "Coding@1234?");
            //    await userManager.AddToRoleAsync(appUser, UserRoles.User);
            //}

            //#endregion
        }
    }
}
