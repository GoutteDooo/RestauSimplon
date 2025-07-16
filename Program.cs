using Microsoft.EntityFrameworkCore;
using RestauSimplon.Data;
using RestauSimplon.Classes;

namespace RestauSimplon
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<RestaurantDb>(opt => opt.UseSqlite("Data source=resto.db"));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            var app = builder.Build();
            //pipo
            RouteGroupBuilder articleItems = app.MapGroup("/articles");
            RouteGroupBuilder clientItems = app.MapGroup("/clients");
            RouteGroupBuilder commandeItems = app.MapGroup("/commandes");

            /* ROUTES */
            
            // -- GET --
            // ---------

            articleItems.MapGet("", async (RestaurantDb db) =>
                await db.Articles.ToListAsync());
            articleItems.MapGet("/disponible", async (RestaurantDb db) =>
                await db.Articles.Where(t => t.Disponible).ToListAsync());

            // -- POST --
            // ----------

            articleItems.MapPost("", async (Article article, RestaurantDb db) =>
            {
                db.Articles.Add(article);
                await db.SaveChangesAsync();

                return Results.Created($"/articles/{article.Id}", article);
            });


            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
