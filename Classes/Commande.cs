namespace RestauSimplon.Classes
{
    public class Commande
    {
        public int Id { get; set; }
        public DateTime DateCommande { get; set; }
        public TypeCommande TypeCommande { get; set; }
        public bool EstTermine { get; set; }
        
        public int ClientId { get; set; } //FK vers Client
        public Client Client { get; set; }
        
        //relation manyToMany (n, n) via table d'association Commandes_articles
        public ICollection<Commandes_articles> Commandes_articles { get; set; } = new List<Commandes_articles>();
        
    }
}
