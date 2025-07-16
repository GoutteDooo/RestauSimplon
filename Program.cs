using Microsoft.EntityFrameworkCore;
using RestauSimplon.Data;

namespace RestauSimplon
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<RestaurantDb>(opt => opt.UseInMemoryDatabase("RestaurantDb"));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            var app = builder.Build();

            var articleItems = app.MapGroup("/articles");
            var clientItems = app.MapGroup("/clients");
            var commandeItems = app.MapGroup("/commandes");
            
            

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
