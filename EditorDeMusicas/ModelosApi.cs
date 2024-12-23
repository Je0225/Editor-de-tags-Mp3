using Newtonsoft.Json;

namespace EditorDeMusicas {

  public class Tokens {

    [JsonProperty("access_token")]
    public String? Token { get; set; }

    [JsonProperty("token_type")]
    public String? Tipo { get; set; }

  }

  public class Response {

    [JsonProperty("tracks")]
    public Tracks Tracks { get; set; }

  }

  public class Tracks {

    [JsonProperty("items")]
    public List<Items>? Items { get; set; }

  }

  public class Items {

   [JsonProperty("album")]
    public  Album Album { get; set; }

    [JsonProperty("artists")]
    public List<Artists> Artistas { get; set; }

    [JsonProperty("track_number")]
    public Int32 Numero { get; set; }

    [JsonProperty("name")]
    public String Nome { get; set; }

    public String ArtistsSeparatedByComma => MontaStringDeArtistas(Artistas);

    public String AlbumArtistsSeparatedByComma => MontaStringDeArtistas(Artistas);

    private String MontaStringDeArtistas(List<Artists>? artistas) {
      if (artistas == null || artistas.Count == 0) {
        return "";
      }
      String nomes = "";
      foreach (Artists artista in artistas) {
        nomes += artista.Nome + ";";
      }
      return nomes;
    }

  }

  public class Artists {

   [JsonProperty("name")]
    public String Nome { get; set; }

  }

  public class Album {

    [JsonProperty("name")]
    public String Nome { get; set; }

    [JsonProperty("release_date")]
    private String ano;

    public String AnoLancamento => ano.Remove(4);

    [JsonProperty("artists")]
    public List<Artists> Artistas { get; set; }

    [JsonProperty("images")]
    public List<Imagem> Imagens { get; set; }

  }

  public class Imagem {

    [JsonProperty("url")]
    public String Url { get; set; }

    public Byte[]? Bytes {get; set; }

  }

}