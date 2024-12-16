using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace EditorDeMusicas {

  public class SpotifyApiServices {

    private String Url { get; set; } = "https://api.spotify.com/v1/search?q=[QUERY]&limit=[LIMIT]&type=[TYPE]";

    private Tokens? TokenInfo { get; set; }

    private Response? Response { get; set; }

    public async Task<List<Items>?>? BuscaItems(String artista, String track, Boolean buscarProximaPagina = false) {
      String type = "track";
      Int32 limit = 10;
      MontaURL(artista, track, type, limit.ToString());
      await FazRequisicaoTokens();

      Response = JsonConvert.DeserializeObject<Response>(buscarProximaPagina
        ? FazRequisicaoTracks().Result
        : FazRequisicaoTracks(Response.Tracks.ProximaUrl).Result);
      BaixaImagensAsync();
      return Response?.Tracks.Items;
    }

    private async Task BaixaImagensAsync() {
      if (Response?.Tracks.Items == null) {
        return;
      }
      foreach (Items item in Response.Tracks.Items) {
        foreach (Imagem imagem in item.Album.Imagens) {
          using (HttpClient client = new HttpClient()) {
            using (Task<Byte[]> stream = client.GetByteArrayAsync(imagem.Url)) {
              imagem.Data = client.GetByteArrayAsync(imagem.Url).Result;
            }
          }
        }
      }
    }

    private void MontaURL(String artista, String track, String type, String limit) {
      String q = "track:[TRACK] artist:[ARTIST]";

      q = q.Replace("[TRACK]", track).Replace("[ARTIST]", artista);
      q = HttpUtility.UrlEncode(q, Encoding.UTF8);
      Url = Url.Replace("[QUERY]", q).Replace("[TYPE]", type).Replace("[LIMIT]", limit);
    }

    private async Task<String> FazRequisicaoTracks(String urlProximaPagina = "") {
      HttpClient client = new HttpClient();

      client.DefaultRequestHeaders.Add("Authorization", TokenInfo.Tipo + " " + TokenInfo.Token);
      client.BaseAddress = urlProximaPagina != "" ? new Uri(urlProximaPagina) : new Uri(Url);
      HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
      return response.Content.ReadAsStringAsync().Result;
    }

    private async Task FazRequisicaoTokens() {
      const String clientId = "491f7b6137034fa7a596b3bec85d6b8a";
      const String clientSecret = "9085bbee72404ab1a369758a46b638d9";
      HttpClient client = new HttpClient();

      String authValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(clientId + ":" + clientSecret));
      client.DefaultRequestHeaders.Add("Authorization", "Basic " + authValue);
      FormUrlEncodedContent content = new FormUrlEncodedContent(new Dictionary<String, String> { { "grant_type", "client_credentials" } });
      HttpResponseMessage response = await client.PostAsync("https://accounts.spotify.com/api/token", content);
      String responseBody = await response.Content.ReadAsStringAsync();
      if (response.IsSuccessStatusCode) {
        TokenInfo = JsonConvert.DeserializeObject<Tokens>(responseBody);
      } else {
        MessageBox.Show(@"Error: " + response.StatusCode);
      }
    }

  }

}