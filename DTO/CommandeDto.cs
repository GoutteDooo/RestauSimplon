using RestauSimplon.Classes;

namespace RestauSimplon.DTO;

public class CommandeDto
{
    public List<int> IdArticles { get; set; } = new();
    public int IdClient { get; set; }
    public TypeCommande Type { get; set; }
}