using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using RestauSimplon.Data;
using RestauSimplon.Classes;

namespace RestauSimplon.Routes
{
    public static class ArticlesEndpoints
    {
        public static IEndpointRouteBuilder MapArticlesEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/articles");

            //Implémenter les routes de Articles dans le Trello

            //GET: /articles
            group.MapGet("", async (RestaurantDb db) =>
            await db.Articles.ToListAsync());

            // GET: /articles/disponible
            group.MapGet("/disponible", async (RestaurantDb db) =>
                await db.Articles.Where(t => t.Disponible).ToListAsync());

            // POST: /articles
            group.MapPost("", async (Article article, RestaurantDb db) =>
            {
                db.Articles.Add(article);
                await db.SaveChangesAsync();

                return Results.Created($"/articles/{article.Id}", article);
            });

            return routes;
        }
    }
}
