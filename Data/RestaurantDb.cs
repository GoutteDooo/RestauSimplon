using Microsoft.EntityFrameworkCore;
using RestauSimplon.Classes;

namespace RestauSimplon.Data
{
    public class RestaurantDb: DbContext
    {
        public RestaurantDb(DbContextOptions<RestaurantDb> options) : base(options)
        {
        }
        public DbSet<Article> Articles => Set<Article>();
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Commande> Commandes => Set<Commande>();
        public DbSet<Commandes_articles> Commandes_articles  => Set<Commandes_articles>();
        
        
    }
}
