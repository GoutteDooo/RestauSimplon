using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using RestauSimplon.Data;
using RestauSimplon.Classes;
using RestauSimplon.DTO;

namespace RestauSimplon.Routes
{
    public static class ClientsEndpoints
    {

        public static IEndpointRouteBuilder MapClientsEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/clients");

            //Implémenter les routes de Clients dans le Trello
            // GET: /clients
            // Récupère la liste des clients
            group.MapGet("", async (RestaurantDb db) =>
                await db.Clients.ToListAsync());

            //GET : /clients/{id}
            // Obtient le client trouvé par son ID (s'il existe)
            group.MapGet("/{id}", async Task<IResult> (int id, RestaurantDb db) =>
            {
                return await db.Clients.FindAsync(id) is Client client
                ? TypedResults.Ok(new ClientDto(client))
                : TypedResults.NotFound();
            });

            // POST : /clients
            // Ajoute le client passé en paramètre
            group.MapPost("", async (Client client, RestaurantDb db) =>
            {
                db.Clients.Add(client);
                await db.SaveChangesAsync();

                return Results.Created($"/clients/{client.Id}", client);
            });

            // DELETE : /clients/{id}
            // Supprime le client passé en paramètre
            group.MapDelete("{id}", async Task<IResult> (int id, RestaurantDb db) =>
            {
                if (await db.Clients.FindAsync(id) is Client client)
                {
                    db.Clients.Remove(client);
                    await db.SaveChangesAsync();
                    return TypedResults.NoContent();
                }

                return TypedResults.NotFound();
            });

            return routes;
        }
    }
}
