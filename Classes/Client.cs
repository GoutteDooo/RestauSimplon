namespace RestauSimplon.Classes
{
    public class Client
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Numero_rue { get; set; }
        public string Nom_rue { get; set; }
        public string Ville { get; set; }
        public int Code_postal { get; set; }
        public string Telephone { get; set; }
        
        //Relation oneToMany (1, n): un client peut avoir plusieurs commandes
        public ICollection<Commande> Commandes { get; set; } = new List<Commande>();
    }
}
