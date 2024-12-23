using System.ComponentModel;
using File = TagLib.File;

namespace EditorDeMusicas {

  public partial class FormPrincipal : Form {

    private EditorTags Editor { get; }

    private SpotifyApiServices Api { get; }

    private Items? ItemDaBusca { get; set; }

    private Boolean RightClicked { get; set; }

    public FormPrincipal() {
      InitializeComponent();
      Editor = new EditorTags();
      Api = new SpotifyApiServices();

      lblQuantidadeArquivosRes.Text = "";
      lblProgressBar.Text = "";
      progressBar.Hide();
      CarregaCapaGenerica();
    }

    private void ProcuraDiretorio() {
      Editor.ProcuraDiretorio(tbFiltro.Text);
      tbFiltro.Text = Editor.Diretorio;
      LimpaEdits();
    }

    private void PopulaListView() {
      lvArquivos.Items.Clear();
      Editor.Tags.Clear();

      String[]? nomesArquivos = Editor.RetornaNomesDosArquivos();
      if (nomesArquivos == null) {
        return;
      }
      foreach (String nome in nomesArquivos) {
        lvArquivos.Items.Add(new ListViewItem(new[] { nome }));
      }
      lblQuantidadeArquivosRes.Text = nomesArquivos.Length.ToString();
    }

    private void MudaSelecionadas() {
      LimpaEdits();
      Editor.Tags.Clear();
      ListView.SelectedListViewItemCollection sl = lvArquivos.SelectedItems;
      String[] selecionados = new String[sl.Count];
      for (int i = 0; i < sl.Count; i++) {
        selecionados[i] = sl[i].Text;
      }
      Editor.PopulaListaTags(selecionados);
      if (Editor.Tags.Count == 1) {
        PopulaEdits();
      }
    }

    private void PopulaEdits() {
      File file = Editor.Tags[0];
      tbArquivo.Text = Path.GetFileNameWithoutExtension(file.Name);
      tbTitulo.Text = file.Tag.Title;
      tbArtistaAlbum.Text = file.Tag.AlbumArtists.Length > 0 ? file.Tag.AlbumArtists.First() : "";
      tbAlbum.Text = file.Tag.Album;
      tbAno.Text = file.Tag.Year.ToString() != "0" ? file.Tag.Year.ToString() : "";
      tbArtistasParticipantes.Text = MontaArrayComArtistasDaTag(file.Tag.Performers);
      tbNumero.Text = file.Tag.Track.ToString() != "0" ? file.Tag.Track.ToString() : "";
      CarregaCapaDaTag(file);
    }

    private String MontaArrayComArtistasDaTag(String[] nomes) {
      if (nomes.Length == 0) {
        return "";
      }
      String conteudo = "";
      foreach (String nome in nomes) {
        conteudo += nome + ";";
      }
      return conteudo;
    }

    private void PopulaEditsComItemDaBusca() {
      if (ItemDaBusca == null) {
        return;
      }
      tbArquivo.Text = ItemDaBusca.Nome;
      tbTitulo.Text = ItemDaBusca.Nome;
      tbAlbum.Text = ItemDaBusca.Album.Nome;
      tbArtistaAlbum.Text = ItemDaBusca.Album.Artistas.First().Nome;
      tbArtistasParticipantes.Text = ItemDaBusca.ArtistsSeparatedByComma;
      tbAno.Text = ItemDaBusca.Album.AnoLancamento;
      tbNumero.Text = ItemDaBusca.Numero.ToString();
      Byte[] bytes = ItemDaBusca.Album.Imagens.Count > 0 ? ItemDaBusca.Album.Imagens[0].Bytes : null;
      if (bytes != null)
        Editor.RecebeImagemDeUmaBusca(bytes);
      pbCapa.Image = Editor.ImagemEscolhida;
    }

    private void LimpaEdits() {
      tbArquivo.Clear();
      tbTitulo.Clear();
      tbArtistasParticipantes.Clear();
      tbArtistaAlbum.Clear();
      tbAlbum.Clear();
      tbNumero.Clear();
      tbAno.Clear();
      CarregaCapaGenerica();
    }

    private void Salvar() {
      if (ItemDaBusca != null) {
        Editor.Salvar(ItemDaBusca.AlbumArtistsSeparatedByComma, ItemDaBusca.Album.Nome, ItemDaBusca.Nome, ItemDaBusca.Nome, ItemDaBusca.ArtistsSeparatedByComma, ItemDaBusca.Album.AnoLancamento, ItemDaBusca.Numero.ToString());
        return;
      }
      Editor.Salvar(tbArtistaAlbum.Text, tbAlbum.Text, tbArquivo.Text, tbTitulo.Text, tbArtistasParticipantes.Text, tbAno.Text, tbNumero.Text);
      PopulaListView();
      LimpaEdits();
    }

    private void CarregaCapaDaTag(File file) {
      if (pbCapa.Image != null) {
        pbCapa.Image.Dispose();
      }
      pbCapa.Image = Editor.RetornaCapaDaTag(file);
      if (pbCapa.Image == null) {
        CarregaCapaGenerica();
      }
    }

    private void CarregaCapaGenerica() {
      if (pbCapa.Image != null) {
        pbCapa.Image.Dispose();
      }
      pbCapa.Image = Editor.RetornaCapaGenerica();
    }

    private void ProcuraImagem() {
      Editor.ProcuraImagem();
      pbCapa.Image = Editor.ImagemEscolhida;
    }

    private void IniciaProgressBar() {
      if (Editor.Tags.Count == 1) {
        progressBar.Style = ProgressBarStyle.Marquee;
        progressBar.MarqueeAnimationSpeed = 4;
      } else if (Editor.Tags.Count > 1) {
        progressBar.Style = ProgressBarStyle.Continuous;
      }
      progressBar.Show();
      lblProgressBar.Text = "";
      progressBar.Maximum = Editor.Tags.Count;
      progressBar.Value = 0;
      AlteraAcessibilidadeControls(false);
    }

    private async void TerminaProgressBar(Boolean sleep) {
      if (sleep) {
        await Task.Delay(1000);
      }
      AlteraAcessibilidadeControls(true);
      lblProgressBar.Text = "";
      progressBar.Hide();
    }

    private void AbreJanelaSeleçãoBusca(List<Items>? tracks) {
      if (tracks?.Count == 0) {
        MessageBox.Show(@"Nenhum item encontrado!");
        return;
      }
      FrmTracksRetornadas frmTracksRetornadas = new(tracks);
      frmTracksRetornadas.ShowDialog();
      if (frmTracksRetornadas.DialogResult != DialogResult.OK || frmTracksRetornadas.ItemSelecionado == null) {
        return;
      }
      ItemDaBusca = frmTracksRetornadas.ItemSelecionado;
    }

    private async void Pesquisar() {
      if (Editor.Tags.Count == 0) {
        MessageBox.Show(@"Selecione pelo menos um ítem para pesquisar!");
        return;
      }
      List<Items>? tracks;
      IniciaProgressBar();

      if (Editor.Tags.Count == 1) {
        File tag = Editor.Tags[0];
        String artista = tbArtistaAlbum.Text;
        String titulo =  tbTitulo.Text == "" ? tbArquivo.Text : tbTitulo.Text;

        tracks = await Task.Run(async () => await Api.BuscaItems(artista, titulo));
        TerminaProgressBar(false);
        AbreJanelaSeleçãoBusca(tracks);
        PopulaEditsComItemDaBusca();
      } else {
        LimpaEdits();

        foreach (File tag in Editor.Tags) {
          String artista = tag.Tag.Performers.Length > 0 && tag.Tag.Performers != null ? tag.Tag.Performers[0] : "";
          String titulo = String.IsNullOrEmpty(tag.Tag.Title) ? Path.GetFileNameWithoutExtension(tag.Name) : tag.Tag.Title;

          lblProgressBar.Text = $@"{progressBar.Value}/{Editor.Tags.Count}";
          tracks = await Task.Run(async () => await Api.BuscaItems(artista, titulo));
          if (tracks == null || tracks.Count == 0) {
            continue;
          }
          progressBar.Value++;
          KeyValuePair<File, List<Items>> resultadosBusca = new KeyValuePair<File, List<Items>>(tag, tracks);
          foreach (ListViewItem lv in lvArquivos.Items.OfType<ListViewItem>().Where(lv => lv.Text == Path.GetFileName(tag.Name))) {
            lv.BackColor = Color.Gold;
            lv.SubItems.Add(Status.Pendente.ToString());
            lv.Tag = resultadosBusca;
          }
        }
        lblProgressBar.Text = $@"{progressBar.Value}/{Editor.Tags.Count}";
        TerminaProgressBar(true);
      }
    }

    private void SincronizaBuscaComATag() {
      if (!RightClicked) {
        return;
      }
      if (lvArquivos.SelectedItems[0].Tag == null || lvArquivos.SelectedItems.Count > 1) {
        return;
      }
      KeyValuePair<File, List<Items>> kvp = (KeyValuePair<File, List<Items>>)lvArquivos.SelectedItems[0].Tag!;
      AbreJanelaSeleçãoBusca(kvp.Value);
      if (ItemDaBusca == null) {
        return;
      }
      lvArquivos.SelectedItems[0].BackColor = Color.MediumSeaGreen;
      lvArquivos.SelectedItems[0].SubItems.RemoveAt(1);
      lvArquivos.SelectedItems[0].SubItems.Add("Sincronizado");
      PopulaEditsComItemDaBusca();
      Salvar();
      //Editor.Salvar(ItemDaBusca.AlbumArtistsSeparatedByComma, ItemDaBusca.Album.Nome, ItemDaBusca.Nome, ItemDaBusca.Nome, ItemDaBusca.ArtistsSeparatedByComma, ItemDaBusca.Album.AnoLancamento, ItemDaBusca.Numero.ToString());
      lvArquivos.SelectedItems.Clear();
      Editor.Tags.Clear();
    }

    private void lvArquivos_MouseDown(object sender, MouseEventArgs e) {
      RightClicked = e.Button == MouseButtons.Right;
    }

    private void AlteraAcessibilidadeControls(Boolean habilitarCampos) {
      foreach (Control control in Controls) {
        if (control.GetType() == typeof(GroupBox)) {
          foreach (Control child in control.Controls) {
            if (child.GetType() == typeof(TextBox) || child.GetType() == typeof(Button) || child.GetType() == typeof(ListView)) {
              child.Enabled = habilitarCampos;
            }
          }
        }
      }
    }

    private void lvArquivos_RightClicked(object sender, EventArgs e) {
      SincronizaBuscaComATag();
    }

    private void btnSalvar_Click(object sender, EventArgs e) {
      Salvar();
    }

    private void btnProcurar_Click(object sender, EventArgs e) {
      ProcuraDiretorio();
      PopulaListView();
    }

    private void lvArquivos_SelectedIndexChanged(object sender, EventArgs e) {
      MudaSelecionadas();
    }

    private void btnProcurarImagem_Click(object sender, EventArgs e) {
      ProcuraImagem();
    }

    private void btnPesquisar_Click(object sender, EventArgs e) {
      Pesquisar();
    }

    private void TextBox_Leave(object sender, EventArgs e) {
      TextBox tb = (TextBox)sender;
      if (tb.Text.Trim() != "" && tb.Text.Trim() != ";" && !tb.Text.Contains(";")) {
        tb.Text = tb.Text.TrimEnd().TrimStart();
        tb.Text += ";";
      }
    }

    private void tbArtistasParticipantes_Enter(object sender, EventArgs e) {
      TextBox tb = (TextBox)sender;
      if (tb.Text.Trim() != "" && tb.Text.Trim() != ";" && tb.Text.Contains(";") && tb.Text.Trim()[^1] != 59) {
        tb.Text = tb.Text.TrimEnd().TrimStart();
        tb.Text += ";";
      }
    }
  }
  /*
   formPrincipal
   Todo: Validate how to write the tb artists by hand
   Todo: How to manage the search params?

   frmTracksReturned
   Todo: Displays all the artists in the viewTrack object
  */
}