namespace RestauSimplon.Classes
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public required string NomDeCompte { get; set; }
        public required string MdpHash { get; set; }
        public bool EstAdmin { get; set; }
    }
}
