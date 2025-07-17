using RestauSimplon.Classes;
using System.ComponentModel.DataAnnotations;

namespace RestauSimplon.DTO;

public class ArticleDto
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public decimal Prix { get; set; }
    public Categorie Categorie { get; set; }
    public string? Description { get; set; }
    public bool Disponible { get; set; }

    // Propriété pour afficher le nom de la catégorie
    // public string NomCategorie => Categorie.ToString();
}