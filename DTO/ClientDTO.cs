using System.ComponentModel.DataAnnotations;

namespace RestauSimplon.DTO;


public class ClientDto
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string Numero_rue { get; set; }
    public string Nom_rue { get; set; }
    public string Ville { get; set; }
    public int Code_postal { get; set; }
    public int Telephone { get; set; }

    // Propriété calculée pour l'adresse complète
    public string AdresseComplete => $"{Numero_rue} {Nom_rue}, {Code_postal} {Ville}";
}
