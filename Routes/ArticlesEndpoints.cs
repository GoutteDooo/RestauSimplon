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

            //GET: /articles Liste d'articles
            group.MapGet("", async (RestaurantDb db) =>
            await db.Articles.ToListAsync());

            //GET: /articles/id affichage d'un article
            group.MapGet("/{id}", async (int id, RestaurantDb db) =>
            {
                var article = await db.Articles.FindAsync(id);
                return article is null ? Results.NotFound() : Results.Ok(article);
            });

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
            
            //DELETE: /articles
            group.MapDelete("/{id}", async (int id, RestaurantDb db) =>
            {
                if (await db.Articles.FindAsync(id) is Article article)
                {
                    db.Articles.Remove(article);
                    await db.SaveChangesAsync();
                    return Results.NoContent();
                }

                return Results.NotFound();
            });

            //PUT: /articles/id
            group.MapPut("/{id}", async (int id, Article inputArticle, RestaurantDb db) =>
            {
                var article = await db.Articles.FindAsync(id);
                if (article == null) return Results.NotFound();

                article.Nom = inputArticle.Nom;
                article.Prix = inputArticle.Prix;
                article.Disponible = inputArticle.Disponible;
                article.Categorie = inputArticle.Categorie;
                article.Description = inputArticle.Description;
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            return routes;
        }
    }
}
