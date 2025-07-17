using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using RestauSimplon.Data;
using RestauSimplon.Classes;

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

            // POST : /clients
            // Ajoute le client passé en paramètre
            group.MapPost("", async (Client client, RestaurantDb db) =>
            {
                db.Clients.Add(client);
                await db.SaveChangesAsync();

                return Results.Created($"/clients/{client.Id}", client);
            });

            return routes;
        }
    }
}
