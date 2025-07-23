namespace RestauSimplon.Classes;

public class CommandeArticles
{
    public int Id { get; set; }
    //public Commande Commande { get; set; }
    //public Article Article { get; set; }
    //FK commande et article
    public int? CommandeId { get; set; } //nullable car possibilité de delete une commande de la bdd
    public int? ArticleId { get; set; } // Nullable aussi car possibilité de delete un article de la bdd
    public int Quantite { get; set; } // Quantité de l'article
    public Commande Commande { get; set; } // navigation vers Commande
    public Article Article { get; set; }  // navigation vers Article
}