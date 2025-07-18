using Microsoft.EntityFrameworkCore;
using RestauSimplon.Classes;
using RestauSimplon.Data;
using RestauSimplon.Routes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace RestauSimplon
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var password = File.ReadAllText("db-password.txt").Trim();
            builder.Services.AddDbContext<RestaurantDb>(opt => opt.UseNpgsql($"Host=localhost;Port=5433;Database=restausimplon;Username=postgres;Password={password}"));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API Restau Simplon",
                    Version = "v1",
                    Description = "Miam miam",
                    Contact = new OpenApiContact
                    {
                        Name = "Toto",
                        Email = "Supertoto@exemple.ex",
                        Url = new Uri("https://restau-simplon.com"),
                    }
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            { 
                app.UseSwagger();
                app.UseSwaggerUI(c =>

                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restau API V1");
                    c.RoutePrefix = "";
                });
            
            
            }


            /* ROUTES */

            app.MapArticlesEndpoints();
            app.MapClientsEndpoints();
            app.MapCommandesEndpoints();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
