using RestauSimplon.Data;

namespace RestauSimplon.Routes
{
    public static class ClientsEndpoints
    {

        public static IEndpointRouteBuilder MapClientsEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/clients");

            //Implémenter les routes de Clients dans le Trello

            return routes;
        }
    }
}
