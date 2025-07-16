namespace RestauSimplon.Classes
{
    public class Article
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public decimal Prix { get; set; }
        public Categorie Categorie { get; set; }
        public string? Description { get; set; }
        public bool Disponible { get; set; }
    }
}
