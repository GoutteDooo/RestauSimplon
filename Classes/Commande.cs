namespace RestauSimplon.Classes
{
    public class Commande
    {
        public int Id { get; set; }
        public DateTime DateCommande { get; set; }
        public TypeCommande TypeCommande { get; set; }
        public bool EstTermine { get; set; }
    }
}
