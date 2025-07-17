using System.ComponentModel.DataAnnotations;
using RestauSimplon.Classes;

namespace RestauSimplon.DTO
{
    public class ArticleRequestDto
    {
        
        //permet de gérer la création (POST) et la mise à jour (PUT) d'une requête
        [Required(ErrorMessage = "le nom de l'article est requis.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "le nom doit faire entre 3 et 50 caractères.")]
        public string Nom { get; set; } = string.Empty; //non-nullable mais peut être modifié
        [Required(ErrorMessage = "le prix de l'article est requis.")] //Required oblige la valeur à ne pas être nulle
        [Range(0.10, 1000.00, ErrorMessage = "le prix de l'article doit être entre 0.10 et 1000.00 .")]
        public decimal Price { get; set; } //non nullable mais modifiable
        
        public Categorie Categorie { get; set; }
        [StringLength(500, ErrorMessage = "la description de l'article ne doit pas dépasser 500 caractères.")]
        public string? Description { get; set; } //nullable car optionnel

        public bool Disponible { get; set; } = true; //non-nullable mais peut être modifié
    }
}
