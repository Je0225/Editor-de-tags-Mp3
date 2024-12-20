using Newtonsoft.Json;

namespace EditorDeMusicas {

  public class Tokens {

    [JsonProperty("access_token")]
    public String Token { get; set; }

    [JsonProperty("token_type")]
    public String Tipo { get; set; }

  }

  public class Response {

    [JsonProperty("tracks")]
    public Tracks Tracks { get; set; }

  }

  public class Tracks {

    [JsonProperty("next")]
    public String ProximaUrl { get; set; }

    [JsonProperty("total")]
    public Int32 TotalResultados { get; set; }

    [JsonProperty("items")]
    public List<Items>? Items { get; set; }

  }

  public class Items {

    [JsonProperty("id")]
    public String Id { get; set; }

    [JsonProperty("album")]
    public Album Album { get; set; }

    [JsonProperty("artists")]
    public List<Artists> Artistas { get; set; }

    [JsonProperty("track_number")]
    public Int32 NumeroDisco { get; set; }

    [JsonProperty("href")]
    public String LinkReferencia { get; set; }

    [JsonProperty("name")]
    public String Nome { get; set; }
  }

  public class Artists {

    [JsonProperty("id")]
    public String Id { get; set; }

    [JsonProperty("name")]
    public String Nome { get; set; }

  }

  public class Album {

    [JsonProperty("id")]
    public String Id { get; set; }

    [JsonProperty("name")]
    public String Nome { get; set; }

    [JsonProperty("release_date_precision")]
    public String DataLancamento { get; set; }

    [JsonProperty("artists")]
    public List<Artists> Artistas { get; set; }

    [JsonProperty("images")]
    public List<Imagem> Imagens { get; set; }

  }

  public class Imagem {

    [JsonProperty("url")]
    public String Url { get; set; }

    [JsonProperty("height")]
    public Int32 Altura { get; set; }

    [JsonProperty("width")]
    public Int32 Largura { get; set; }

    public Byte[]? Data {get; set; }
  }

}