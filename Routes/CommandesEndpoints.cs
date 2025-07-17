namespace RestauSimplon.Routes
{
    public static class CommandesEndpoints
    {
        public static IEndpointRouteBuilder MapCommandesEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/commandes");

            //Implémenter les routes de Commandes dans le Trello

            return routes;
        }
    }
}
