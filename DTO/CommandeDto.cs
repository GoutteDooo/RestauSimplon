using RestauSimplon.Classes;

namespace RestauSimplon.DTO;

public class CommandeDto
{
    public int Id { get; set; }
    public DateTime DateCommande { get; set; }
    public TypeCommande TypeCommande { get; set; }
    public bool EstTermine { get; set; }
    //Inclure ClientDTO pour obtenir info client et liste articles dans la requête Json
    public ClientDto Client { get; set; }
    public List<ArticleDto> Articles { get; set; }
}