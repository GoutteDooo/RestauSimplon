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
        public DbSet<CommandeArticles> CommandeArticles => Set<CommandeArticles>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Commande>()
                .HasMany(c => c.CommandeArticles)
                .WithOne()
                .HasForeignKey("IdCommande")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
