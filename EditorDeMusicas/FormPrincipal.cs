using System.ComponentModel;
using File = TagLib.File;

namespace EditorDeMusicas {

  public partial class FormPrincipal : Form {

    private EditorTags Editor { get; }

    private readonly SpotifyApiServices api = new();

    private Items? ItemDaBusca { get; set; }

    private Boolean RightClicked { get; set; }

    public FormPrincipal() {
      InitializeComponent();
      Editor = new EditorTags();
      CarregaCapaGenerica();
      lblQuantidadeArquivosRes.Text = "";
      lblProgressBar.Text = "";
      progressBar.Hide();
    }

    private void ProcuraDiretorio() {
      Editor.ProcuraDiretorio(tbFiltro.Text);
      tbFiltro.Text = Editor.Diretorio;
      LimpaCamposEditor();
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
      Editor.Tags.Clear();
      ListView.SelectedListViewItemCollection sl = lvArquivos.SelectedItems;
      String[] selecionados = new String[sl.Count];
      for (int i = 0; i < sl.Count; i++) {
        selecionados[i] = sl[i].Text;
      }
      Editor.PopulaListaTags(selecionados);
      PopulaEdits();
      if (Editor.Tags.Count > 1) {
        LimpaCamposEditor();
      }
    }

    private String PopulaEditsComArrays(String[] nomes) {
      if (nomes == null || nomes.Length == 0) {
        return "";
      }
      String conteudo = "";
      foreach (String nome in nomes) {
        conteudo += nome + "; ";
      }
      return conteudo;
    }

    private String MontaStringNomeArtistas(List<Artists> artistas) {
      if (artistas == null || artistas.Count == 0) {
        return "";
      }
      String nomes = "";
      foreach (Artists artista in artistas) {
        nomes += artista.Nome + ";";
      }
      return nomes;
    }

    private void PopulaEdits() {
      if (Editor.Tags.Count == 1) {
        File file = Editor.Tags[0];
        tbTitulo.Text = file.Tag.Title;
        tbArtistaAlbum.Text = PopulaEditsComArrays(file.Tag.AlbumArtists);
        tbAlbum.Text = file.Tag.Album;
        tbAno.Text = file.Tag.Year.ToString();
        tbArtistasParticipantes.Text = PopulaEditsComArrays(file.Tag.Performers);
        tbNumero.Text = file.Tag.Track.ToString();
        CarregaCapaDaTag(file);
      }
    }

    private void PopulaEditsComItemDaBusca() {
      tbTitulo.Text = ItemDaBusca.Nome;
      tbAlbum.Text = ItemDaBusca.Album.Nome;
      tbArtistaAlbum.Text = ItemDaBusca.Artistas.First().Nome;
      tbNumero.Text = ItemDaBusca.NumeroDisco.ToString();
      Editor.RecebeImagemDeUmaBusca(ItemDaBusca.Album.Imagens[0].Data);
      pbCapa.Image = Editor.ImagemEscolhida;
    }

    private void LimpaCamposEditor() {
      tbTitulo.Clear();
      tbArtistasParticipantes.Clear();
      tbArtistaAlbum.Clear();
      tbAlbum.Clear();
      tbNumero.Clear();
      tbAno.Clear();
      CarregaCapaGenerica();
    }

    private void Salvar() {
      Editor.Salvar(tbArtistaAlbum.Text, tbAlbum.Text, tbTitulo.Text, tbArtistasParticipantes.Text, tbAno.Text, tbAno.Text);
      PopulaListView();
      LimpaCamposEditor();
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

    private void InicializaProgressBar() {
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

    private async void StopaProgressBar(Boolean sleep) {
      if (sleep) {
        await Task.Delay(1000);
      }
      AlteraAcessibilidadeControls(true);
      lblProgressBar.Text = "";
      progressBar.Hide();
    }

    private void AbreJanelaSeleçãoBusca(List<Items>? tracks) {
      if (tracks == null) {
        return;
      }
      FrmTracksRetornadas frmTracksRetornadas = new(tracks);
      frmTracksRetornadas.ShowDialog();
      if (frmTracksRetornadas.DialogResult != DialogResult.OK || frmTracksRetornadas.SelectedItem == null) {
        return;
      }
      ItemDaBusca = frmTracksRetornadas.SelectedItem;
    }

    private async void Pesquisar() {
      List<Items>? tracks;
      InicializaProgressBar();

      if (Editor.Tags.Count == 1) {
        tracks = await Task.Run(async () => await api.BuscaItems(tbArtistaAlbum.Text, tbTitulo.Text));
        StopaProgressBar(false);
        AbreJanelaSeleçãoBusca(tracks);
        if (ItemDaBusca == null) {
          return;
        }
        PopulaEditsComItemDaBusca();
      } else if (Editor.Tags.Count > 1) {
        LimpaCamposEditor();

        foreach (File tag in Editor.Tags) {
          String artista = tag.Tag.Performers.Length > 0 && tag.Tag.Performers != null ? tag.Tag.Performers[0] : "";
          String titulo = String.IsNullOrEmpty(tag.Tag.Title) ? Path.GetFileName(tag.Name) : tag.Tag.Title;

          lblProgressBar.Text = $@"{progressBar.Value}/{Editor.Tags.Count}";
          tracks = await Task.Run(async () => await api.BuscaItems(artista, titulo));
          progressBar.Value++;
          KeyValuePair<File, List<Items>> resultadosBusca = new KeyValuePair<File, List<Items>>(tag, tracks);
          foreach (ListViewItem lv in lvArquivos.Items.OfType<ListViewItem>().Where(lv => lv.Text == Path.GetFileName(tag.Name))) {
            lv.BackColor = Color.Gold;
            lv.SubItems.Add(Status.Pendente.ToString());
            lv.Tag = resultadosBusca;
          }
        }
        lblProgressBar.Text = $@"{progressBar.Value}/{Editor.Tags.Count}";
        StopaProgressBar(true);
      }
    }

    private void SincronizaBuscaComATag() {
      if (RightClicked) {
        if (lvArquivos.SelectedItems[0].Tag == null || lvArquivos.SelectedItems.Count > 1) {
          return;
        }
        KeyValuePair<File, List<Items>> kvp = (KeyValuePair<File, List<Items>>)lvArquivos.SelectedItems[0].Tag;

        AbreJanelaSeleçãoBusca(kvp.Value);
        if (ItemDaBusca == null) {
          return;
        }
        lvArquivos.SelectedItems[0].BackColor = Color.MediumSeaGreen; 
        lvArquivos.SelectedItems[0].SubItems.RemoveAt(1);
        lvArquivos.SelectedItems[0].SubItems.Add("Sincronizado");
        PopulaEditsComItemDaBusca();
        Editor.Salvar(MontaStringNomeArtistas(ItemDaBusca.Album.Artistas), ItemDaBusca.Album.Nome, ItemDaBusca.Nome, MontaStringNomeArtistas(ItemDaBusca.Artistas), ItemDaBusca.Album.DataLancamento, ItemDaBusca.NumeroDisco.ToString());
        lvArquivos.SelectedItems.Clear();
        Editor.Tags.Clear();
      }
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
      if (tb.Text.Trim() != "" && !tb.Text.Contains(";")) {
        tb.Text = tb.Text.TrimEnd().TrimStart();
        tb.Text += ";";
      }
    }
  }
  /*
   formPrincipal
   Todo: Validate how to write the tb artists by hand

   frmTracksReturned
   Todo: Displays all the artists in the viewTrack object
  */
}