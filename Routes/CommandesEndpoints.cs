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

            group.MapPut("/{id}/terminee", async Task<IResult> (int id, RestaurantDb db) =>
            {
                var commande = await db.Commandes.FindAsync(id);
                if (commande == null)
                    return TypedResults.NotFound();

                commande.EstTermine = true;
                await db.SaveChangesAsync();

                return TypedResults.Ok(commande);
            });

            group.MapPost("", async (Commande nouvelleCommande, RestaurantDb db) =>
            {
                nouvelleCommande.DateCommande = DateTime.Now;
                nouvelleCommande.EstTermine = false;

                db.Commandes.Add(nouvelleCommande);
                await db.SaveChangesAsync();

                return Results.Created($"/commandes/{nouvelleCommande.Id}", nouvelleCommande);
            });

            group.MapDelete("/{id}/supprimer", async Task<IResult> (int id, RestaurantDb db) =>
            {
                var commande = await db.Commandes.FindAsync(id);
                if (commande == null)
                    return Results.NotFound();

                db.Commandes.Remove(commande);
                await db.SaveChangesAsync();

                return Results.NoContent();
            });

            return routes;
        }
    }
}
