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

            /** GET : /
             * Récupère la liste des commandes par ordre décroissant
             * Le json récupéré ressemble à ça :
             * {
             *      "id": 6,
             *      "dateCommande": "2025-07-20T09:16:24.203242Z",
             *      "typeCommande": "SurPlace",
             *      "estLivree": false,
             *      "clientId": 1,
             *      "client": {
                        "idClient":idDuClient,
             *          "nom": nomDuClient,
             *          "prenom":prenomDuClient
             *          }
             *      "idArticles":[1, 2, 2, 3, 5, 5, 5]
             * }
             */
            group.MapGet("", async (RestaurantDb db) =>
            {
                var commandes = await db.Commandes
                    .OrderByDescending(c => c.DateCommande)
                    .Select(c => new
                    {
                        c.Id,
                        c.DateCommande,
                        c.TypeCommande,
                        c.EstLivree,
                        c.ClientId,
                        Client = new
                        {
                            idClient = c.Client.Id,
                            nom = c.Client.Nom,
                            prenom = c.Client.Prenom
                        },
                        articles = c.CommandeArticles
                            .Select(ca => new {
                                idArticle = ca.IdArticle,
                                nomArticle = ca.Article.Nom,
                                prixArticle = ca.Article.Prix,
                                quantite = ca.Quantite
                            })
                            .ToList(),
                        prixTotal = c.CommandeArticles.Sum(ca => ca.Quantite * ca.Article.Prix)
                    })
                    .ToListAsync();

                return commandes;
            });


            /**
             * GET : /{id}
             * Récupère une commande par son id
             */
            group.MapGet("/{id}", async Task<IResult> (int id, RestaurantDb db) =>
            {
                return await db.Commandes.FindAsync(id) is Commande commande
                ? TypedResults.Ok(commande)
                : TypedResults.NotFound();
            });

            /**
             * PUT : /{id}/livree
             *  Modifie le champ "EstLivree" sur true
             */
            group.MapPut("/{id}/livree", async Task<IResult> (int id, RestaurantDb db) =>
            {
                var commande = await db.Commandes.FindAsync(id);
                if (commande == null)
                    return TypedResults.NotFound();

                commande.EstLivree = true;
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
                    EstLivree = false,
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
                    EstLivree = commande.EstLivree,
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


            /** DELETE : /{id}/supprimer
             * Supprime la commande dont l'id correspond
             */

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

            /**
             * GET : /non-livree
             * Récupère les commandes en attente de livraison
             */
            group.MapGet("/non-livree", async Task<IResult> (RestaurantDb db) =>
            {

                var commandesNonLivrees = await db.Commandes
                    .OrderByDescending(c => c.DateCommande)
                    .Where(c => !c.EstLivree)
                    .Select(c => new
                    {
                        c.Id,
                        c.DateCommande,
                        c.TypeCommande,
                        c.EstLivree,
                        c.ClientId,
                        Client = new
                        {
                            idClient = c.Client.Id,
                            nom = c.Client.Nom,
                            prenom = c.Client.Prenom
                        },
                        articles = c.CommandeArticles
                            .Select(ca => new {
                                idArticle = ca.IdArticle,
                                nomArticle = ca.Article.Nom,
                                prixArticle = ca.Article.Prix,
                                quantite = ca.Quantite
                            })
                            .ToList(),
                        prixTotal = c.CommandeArticles.Sum(ca => ca.Quantite * ca.Article.Prix)
                    })
                    .ToListAsync();

                return commandesNonLivrees.Count > 0 ? TypedResults.Ok(commandesNonLivrees) : TypedResults.NoContent();
            });

            return routes;
        }
    }
}
