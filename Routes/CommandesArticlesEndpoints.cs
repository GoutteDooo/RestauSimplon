using RestauSimplon.Classes;
using RestauSimplon.Data;
using RestauSimplon.DTO;

namespace RestauSimplon.Routes
{
    public static class CommandesArticlesEndpoints
    {
        public static IEndpointRouteBuilder MapCommandesArticlesEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/commande-articles");

            /**
             * Méthode POST pour créer une CommandeArticle
             * Plusieurs CommandeArticle seront créés en fonction du nombre d'articles d'une commande
             * id est tout le temps différent
             * idCommande est toujours le même
             * idArticle est tout le temps différent
             */
            group.MapPost("", async (CommandeArticlesDto dto, RestaurantDb db) =>
            {
                // Si le DTO ne contient aucun article
                if (dto.IdArticles == null || dto.IdArticles.Count == 0)
                    return Results.BadRequest("La liste des articles est vide.");

                var commandeArticles = dto.IdArticles.Select((idArticle, index) => new CommandeArticles
                {
                    // Id est auto-généré par la base car c’est une clé auto-incrémentée
                    IdCommande = dto.IdCommande,
                    IdArticle = idArticle
                }).ToList();

                db.CommandeArticles.AddRange(commandeArticles);
                await db.SaveChangesAsync();

                return Results.Created($"/commande-articles/commande/{dto.IdCommande}", commandeArticles);
            });

            return routes;
        }
    }
}
