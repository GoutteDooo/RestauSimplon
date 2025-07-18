using Microsoft.EntityFrameworkCore;
using RestauSimplon.Classes;
using RestauSimplon.Data;
using RestauSimplon.DTO;
using System.Threading.Tasks;

namespace RestauSimplon.Routes
{
    public static class CommandesEndpoints
    {
        public static async Task<IEndpointRouteBuilder> MapCommandesEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/commandes");

            group.MapGet("", async (RestaurantDb db) =>
            {
                var commandes = await db.Commandes
                    .OrderByDescending(c => c.DateCommande)
                    .ToListAsync();

                return commandes;
            });

            group.MapGet("/{id}", async Task<IResult> (int id, RestaurantDb db) =>
            {
                return await db.Commandes.FindAsync(id) is Commande commande
                ? TypedResults.Ok(commande)
                : TypedResults.NotFound();
            });

            // PUT : /{id}/terminee
            // -- Modifie le champ "EstTermine" sur true
            group.MapPut("/{id}/terminee", async Task<IResult> (int id, RestaurantDb db) =>
            {
                var commande = await db.Commandes.FindAsync(id);
                if (commande == null)
                    return TypedResults.NotFound();

                commande.EstTermine = true;
                await db.SaveChangesAsync();

                return TypedResults.Ok(commande);
            });

            /**
             * POST : /commandes
             * Insère une nouvelle Commande dans la bdd
             * Et insère le nombre de lignes correspondant au nombre d'articles de la commande dans CommandeArticles dans la bdd
             */
            group.MapPost("", async (CommandeDto dto, RestaurantDb db) =>
            {
                // Vérifier si le client existe
                Client? client = await db.Clients.FindAsync(dto.IdClient);
                if (client == null)
                    return TypedResults.NotFound();

                // Vérifier que les articles existent
                foreach(int idArticle in dto.IdArticles)
                {
                    if(await db.Articles.FindAsync(idArticle) == null)
                        return TypedResults.NotFound();
                }

                /* La liste reçue par le DTO peut contenir plusieurs ids identique
                 * Par exemple:
                 * 
                 *     idArticles = [1, 2, 2, 3, 5, 5, 5]
                 * 
                 * Dans ce cas, on va créer un dictionnaire avec pour clé l'id de l'article, 
                 * et pour valeur la quantité
                 * 
                 * Dans notre exemple, ça donne :
                 * 
                 *      {
                 *          ['1']: 1,
                 *          ['2']: 2,
                 *          ['3']: 1,
                 *          ['5']: 3
                 *      }
                 *      
                 * Une fois que c'est fait, on peut créer nos enregistrements pour la table CommandeArticles
                 */

                Dictionary<int, int> quantiteArticles = new();

                // Insérer clés et valeurs
                foreach (int idArticle in dto.IdArticles)
                {
                    if (!quantiteArticles.ContainsKey(idArticle))
                        quantiteArticles[idArticle] = 0; // Initialise la clé à 0 pour éviter une Exception
                    quantiteArticles[idArticle]++;
                }

                DateTime aujourdhui = DateTime.UtcNow;
                // On crée la nouvelle commande
                Commande commande = new Commande
                {
                    DateCommande = aujourdhui,
                    TypeCommande = dto.Type,
                    EstTermine = false,
                    ClientId = dto.IdClient,
                    Client = client
                };

                // Et avec le dictionnaire, on peut créer la liste de CommandeArticles
                foreach (KeyValuePair<int, int> quantiteArticle in quantiteArticles)
                {
                    // Crée une instance de CommandeArticles et l'ajoute à la ICollection de la commande
                    commande.CommandeArticles.Add(new CommandeArticles
                    {
                        IdArticle = quantiteArticle.Key,
                        Quantite = quantiteArticle.Value
                    });
                }

                db.Commandes.Add(commande);

                await db.SaveChangesAsync();

                CommandeResponseDto response = new CommandeResponseDto
                {
                    Id = commande.Id,
                    DateCommande = commande.DateCommande,
                    TypeCommande = commande.TypeCommande,
                    EstTermine = commande.EstTermine,
                    ClientId = commande.ClientId,
                    Articles = commande.CommandeArticles
                        .Select(ca => new CommandeArticleDto
                        {
                            IdArticle = ca.IdArticle,
                            Quantite = ca.Quantite
                        }).ToList()
                };

                return Results.Created($"/commandes/{commande.Id}", response);
            });

            group.MapDelete("/{id}/supprimer", async Task<IResult> (int id, RestaurantDb db) =>
            {
                if (await db.Commandes.FindAsync(id) is Commande commande)
                {
                    db.Commandes.Remove(commande);
                    await db.SaveChangesAsync();
                    return TypedResults.NoContent();
                }

                return TypedResults.NotFound();
            });

            return routes;
        }
    }
}
