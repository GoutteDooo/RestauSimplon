namespace RestauSimplon.DTO
{
    public class CommandeArticlesDto
    {
        public int IdCommande {  get; set; }
        public List<int> IdArticles { get; set; } = new List<int>();
    }
}
