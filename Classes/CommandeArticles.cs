namespace RestauSimplon.Classes;

public class CommandeArticles
{
    public int Id { get; set; }
    //public Commande Commande { get; set; }
    //public Article Article { get; set; }
    //FK commande et article
    public int IdCommande { get; set; }
    public int IdArticle { get; set; }
    public int Quantite { get; set; } // Quantité de l'article
}