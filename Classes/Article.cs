using System.Text.Json.Serialization;

namespace RestauSimplon.Classes
{
    public class Article
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public decimal Prix { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Categorie Categorie { get; set; }
        public string? Description { get; set; }
        public bool Disponible { get; set; }
        
        //relation manyToMany (n, n) via table d'association Commandes_articles
        //Utilisation d'ICollection pour meilleure flexibilité au niveau des données
        public ICollection<Commandes_articles> Commandes_articles { get; set; } = new List<Commandes_articles>();
    }
}
