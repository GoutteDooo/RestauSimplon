using System.ComponentModel.DataAnnotations;

namespace RestauSimplon.DTO;

public class ClientRequestDto
{
    //permet de gérer la création (POST) et la mise à jour (PUT) d'une requête
    
    [Required(ErrorMessage = "le nom du client est requis.")] //required oblige que la valeur ne soit pas nulle
    [StringLength(50, MinimumLength = 2, ErrorMessage = "le nom doit faire entre 2 et 50 caractères")]
    public string Nom { get; set; } = string.Empty;//non-nullable mais modifiable
    [Required(ErrorMessage = "le prenom du client est requis.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "le nom doit faire entre 2 et 50 caractères")]
    public string Prenom { get; set; } = string.Empty;//non-nullable mais modifiable
    [Required(ErrorMessage = "Le numéro de rue est requis.")]
    [StringLength(10, ErrorMessage = "Le numéro de rue ne peut pas dépasser 10 caractères.")]
    public string Numero_rue { get; set; } = string.Empty;//non-nullable mais modifiable
    [Required(ErrorMessage = "le nom de rue du client est requis.")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "Le nom de rue doit avoir entre 5 et 200 caractères.")]
    public string Nom_rue { get; set; } = string.Empty;//non-nullable mais modifiable
    [Required(ErrorMessage = "La ville est requise.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Le nom de la ville doit avoir entre 2 et 100 caractères.")]
    public string Ville { get; set; } = string.Empty;//non-nullable mais modifiable
    [Required(ErrorMessage = "Le code postal est requis.")]
    [Range(01000, 99999, ErrorMessage = "Le code postal doit être un nombre à 5 chiffres (ex: 75000).")]
    public int Code_postal { get; set; } // int ici car c'est un nombre à 5 chiffres en France
    //non-nullable mais modifiable
    [Required(ErrorMessage = "Le numéro de téléphone est requis.")]
    [Phone(ErrorMessage = "Le format du numéro de téléphone n'est pas valide.")] // Validation de format de téléphone
    [StringLength(10, MinimumLength = 10, ErrorMessage = "Le nom de la ville doit avoir entre 2 et 100 caractères.")]
    public string Telephone { get; set; }  // ne serait-il pas mieux de transformer Telephone en type String ? on a pas besoin de faire de calculs sur le telephone et puis il y a la possibilité que les zeros initiaux soient perdus et ça créerait des soucis de formatage.
}