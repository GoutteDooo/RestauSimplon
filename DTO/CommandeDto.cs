using System.Text.Json.Serialization;
using RestauSimplon.Classes;

namespace RestauSimplon.DTO;

public class CommandeDto
{
    public List<int> IdArticles { get; set; } = new();
    //public List<Article> Articles { get; set; }
    public int IdClient { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TypeCommande Type { get; set; }

    /* {
     *  "idArticles":[1,2,2],
     *  "articles": [
     *  {
     *      "id":1,
     *      "nom":"coca",
     *      "...",
     *  },
     *  {
     *      "id":2,
     *      "nom":"pizza",
     *      "..."
     *  }
     *  ]
     *  "idClient":5,
     *  "type":"sur place"
     * }
     */
}