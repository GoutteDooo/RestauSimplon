namespace RestauSimplon.Classes
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public string NomDeCompte { get; set; }
        public string MdpHash { get; set; }
        public bool EstAdmin { get; set; }
    }
}
