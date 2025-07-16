using Microsoft.EntityFrameworkCore;
using RestauSimplon.Data;

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

            articleItems.MapGet("", async (RestaurantDb db) =>
                await db.Articles.ToListAsync());


            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
