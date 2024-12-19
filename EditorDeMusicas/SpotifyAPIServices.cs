using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace EditorDeMusicas {

  public class SpotifyApiServices {

    private String Url { get; set; }

    private Tokens? TokenInfo { get; set; }

    private Response? Response { get; set; }

    public async Task<List<Items>?> BuscaItems(String artista, String track) {
      String type = "track";
      Int32 limit = 50;
      MontaURL(artista, track, type, limit.ToString());

      Response = JsonConvert.DeserializeObject<Response>(await FazRequisicaoTracks());
      BaixaImagens();
      return Response?.Tracks.Items;
    }

    private void BaixaImagens() {
      if (Response?.Tracks.Items == null) {
        return;
      }
      foreach (Items item in Response.Tracks.Items) {
        foreach (Imagem imagem in item.Album.Imagens) {
          using HttpClient client = new HttpClient();
          using Task<Byte[]> stream = client.GetByteArrayAsync(imagem.Url);
          imagem.Data = stream.Result;
        }
      }
    }

    private void MontaURL(String artista, String track, String type, String limit) {
      Url = "https://api.spotify.com/v1/search?q=[QUERY]&limit=[LIMIT]&type=[TYPE]";
      String q = "track:[TRACK] artist:[ARTIST]";

      q = q.Replace("[TRACK]", track).Replace("[ARTIST]", artista);
      q = HttpUtility.UrlEncode(q, Encoding.UTF8);
      Url = Url.Replace("[QUERY]", q).Replace("[TYPE]", type).Replace("[LIMIT]", limit);
    }

    private async Task<String> FazRequisicaoTracks() {
      HttpClient client = new HttpClient();
      if (TokenInfo == null) {
        RequestTokens();
      }
      for (int i = 0; i <= 3; i++) {
        client.DefaultRequestHeaders.Add("Authorization", TokenInfo.Tipo + " " + TokenInfo.Token);
        client.BaseAddress = new Uri(Url);
        HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
        if (response.StatusCode == HttpStatusCode.OK) {
          return response.Content.ReadAsStringAsync().Result;
        }
        RequestTokens();
      }
      return null;
    }

    private void RequestTokens() {
      const String clientId = "491f7b6137034fa7a596b3bec85d6b8a";
      const String clientSecret = "dd935b5a39264b049cf3a6fc73815762";
      HttpClient client = new HttpClient();

      String authValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(clientId + ":" + clientSecret));
      client.DefaultRequestHeaders.Add("Authorization", "Basic " + authValue);
      FormUrlEncodedContent content = new FormUrlEncodedContent(new Dictionary<String, String> { { "grant_type", "client_credentials" } });
      HttpResponseMessage response = client.PostAsync("https://accounts.spotify.com/api/token", content).Result;
      String responseBody =  response.Content.ReadAsStringAsync().Result;
      if (response.IsSuccessStatusCode) {
        TokenInfo = JsonConvert.DeserializeObject<Tokens>(responseBody);
      } else {
        MessageBox.Show(@"Erro ao requisitar tokens: " + response.StatusCode + ": \n" + response.Content);
      }
    }

  }

}