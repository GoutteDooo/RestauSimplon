using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using RestauSimplon.Data;
using RestauSimplon.Classes;

namespace RestauSimplon.Routes
{
    public static class UtilisateurEndpoints
    {
        public static IEndpointRouteBuilder MapUtilisateursEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/utilisateurs");

            group.MapGet("", async (RestaurantDb db) =>
                await db.Utilisateurs.ToListAsync());

            group.MapGet("/{id}", async (int id, RestaurantDb db) =>
            {
                var utilisateur = await db.Utilisateurs.FindAsync(id);
                return utilisateur is null ? Results.NotFound() : Results.Ok(utilisateur);
            });

            group.MapPost("", async (Utilisateur utilisateur, RestaurantDb db) =>
            {
                db.Utilisateurs.Add(utilisateur);
                await db.SaveChangesAsync();
                return Results.Created($"/utilisateurs/{utilisateur.Id}", utilisateur);
            });

            group.MapDelete("/{id}", async (int id, RestaurantDb db) =>
            {
                if (await db.Utilisateurs.FindAsync(id) is Utilisateur utilisateur)
                {
                    db.Utilisateurs.Remove(utilisateur);
                    await db.SaveChangesAsync();
                    return Results.NoContent();
                }
                return Results.NotFound();
            });

            group.MapPut("/{id}", async (int id, Utilisateur input, RestaurantDb db) =>
            {
                var utilisateur = await db.Utilisateurs.FindAsync(id);
                if (utilisateur == null) return Results.NotFound();

                utilisateur.NomDeCompte = input.NomDeCompte;
                utilisateur.MdpHash = input.MdpHash;
                utilisateur.EstAdmin = input.EstAdmin;
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            return routes;
        }
    }
}
