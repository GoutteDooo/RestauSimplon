using RestauSimplon.Classes;

namespace RestauSimplon.DTO
{
    public class CommandeResponseDto
    {
        public int Id { get; set; }
        public DateTime DateCommande { get; set; }
        public TypeCommande TypeCommande { get; set; }
        public bool EstTermine { get; set; }
        public int ClientId { get; set; }
        public List<CommandeArticleDto> Articles { get; set; }
    }
}
