using System.ComponentModel.DataAnnotations;
using RestauSimplon.Classes;
namespace RestauSimplon.DTO;
public class ClientDto
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string NumeroRue { get; set; }
    public string NomRue { get; set; }
    public string Ville { get; set; }
    public int CodePostal { get; set; }
    public string Telephone { get; set; }

    public ClientDto() { }
    public ClientDto(Client clientItem) =>
        (Id, Nom, Prenom, NumeroRue, NomRue, Ville, CodePostal, Telephone) 
        = (clientItem.Id, clientItem.Nom, clientItem.Prenom, clientItem.NumeroRue, clientItem.NomRue, clientItem.Ville, clientItem.CodePostal, clientItem.Telephone);

    // Propriété calculée pour l'adresse complète
    public string AdresseComplete => $"{NumeroRue} {NomRue}, {CodePostal} {Ville}";
}
