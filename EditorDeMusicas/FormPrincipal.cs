using System.ComponentModel;
using File = TagLib.File;

namespace EditorDeMusicas {

  public partial class FormPrincipal : Form {

    private EditorTags Editor { get; }

    private readonly SpotifyApiServices api = new();

    private Items ItemDaBusca { get; set; }

    private Boolean RightClicked { get; set; }

    private Boolean Pesquisou { get; set; } = false;

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
      ;
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

    private void PopulaEdits() {
      if (Editor.Tags.Count == 1) {
        File file = Editor.Tags[0];
        tbTitulo.Text = file.Tag.Title;
        tbArtistaAlbum.Text = file.Tag.Performers.Length > 0 ? file.Tag.Performers[0] : "";
        tbAlbum.Text = file.Tag.Album;
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
      tbCompositor.Clear();
      tbGenero.Clear();
      CarregaCapaGenerica();
    }

    private void Salvar() {
      Editor.Salvar(tbArtistaAlbum.Text, tbAlbum.Text, tbTitulo.Text);
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

    private async Task<List<Items>?> Busca() {
       return await api.BuscaItems(tbArtistaAlbum.Text, tbTitulo.Text);
    }

    private void PesquisarUmaMusica(List<Items>? tracks) {
      if (tracks == null) {
        return;
      }
      Pesquisou = true;
      FrmTracksRetornadas frmTracksRetornadas = new(tracks);
      frmTracksRetornadas.ShowDialog();
      if (frmTracksRetornadas.DialogResult != DialogResult.OK || frmTracksRetornadas.SelectedItem == null) {
        return;
      }
      ItemDaBusca = frmTracksRetornadas.SelectedItem;
      PopulaEditsComItemDaBusca();
    }

    private async Task ProgressBarAsyncUmaPesquisa() {
      progressBar.Show();
      lblProgressBar.Text = "";
      progressBar.Maximum = Editor.Tags.Count;
      progressBar.Style = ProgressBarStyle.Marquee;
      progressBar.MarqueeAnimationSpeed = 4;
      progressBar.Value = 1;
      AlteraAcessibilidadeControls(false);
      List<Items>? tracks = await Task.Run(Busca) ;
      AlteraAcessibilidadeControls(true);
      lblProgressBar.Text = "";
      progressBar.Hide();
      PesquisarUmaMusica(tracks);
    }

    private async void PesquisarVariasMusicas() {
      progressBar.Show();
      lblProgressBar.Text = "";
      progressBar.Maximum = Editor.Tags.Count;
      progressBar.Style = ProgressBarStyle.Continuous;
      progressBar.Step = 1;
      progressBar.Value = 0;
      LimpaCamposEditor();
      AlteraAcessibilidadeControls(false);
      foreach (File tag in Editor.Tags) {
        String artista = tag.Tag.Performers.Length > 0 && tag.Tag.Performers != null ? tag.Tag.Performers[0] : "";
        String titulo = String.IsNullOrEmpty(tag.Tag.Title) ? Path.GetFileName(tag.Name) : tag.Tag.Title;

        if (artista != null) {
          lblProgressBar.Text = $@"{progressBar.Value}/{Editor.Tags.Count}";
          List<Items>? tracks = await Task.Run(async () => await api.BuscaItems(artista, titulo));
          progressBar.Value++;
          KeyValuePair<File, List<Items>> resultadosBusca = new KeyValuePair<File, List<Items>>(tag, tracks);
          foreach (ListViewItem lv in lvArquivos.Items.OfType<ListViewItem>().Where(lv => lv.Text == Path.GetFileName(tag.Name))) {
            lv.BackColor = Color.Gold;
            lv.SubItems.Add(Status.Pendente.ToString());
            lv.Tag = resultadosBusca;
          }
        }
      }
      lblProgressBar.Text = $@"{progressBar.Value}/{Editor.Tags.Count}";
      await Task.Delay(1000);
      AlteraAcessibilidadeControls(true);
      lblProgressBar.Text = "";
      progressBar.Hide();
    }

    private void lvArquivos_MouseDown(object sender, MouseEventArgs e) {
      RightClicked = e.Button == MouseButtons.Right;
    }

    private void SincronizaBuscaComATag() {
      if (RightClicked) {
        if (lvArquivos.SelectedItems[0].Tag == null || lvArquivos.SelectedItems.Count > 1) {
          return;
        }
        KeyValuePair<File, List<Items>> kvp = (KeyValuePair<File, List<Items>>)lvArquivos.SelectedItems[0].Tag;

        FrmTracksRetornadas frmTracksRetornadas = new(kvp.Value);
        frmTracksRetornadas.ShowDialog();
        if (frmTracksRetornadas.DialogResult != DialogResult.OK || frmTracksRetornadas.SelectedItem == null) {
          return;
        }
        ItemDaBusca = frmTracksRetornadas.SelectedItem;
        lvArquivos.SelectedItems[0].BackColor = Color.MediumSeaGreen;
        lvArquivos.SelectedItems[0].SubItems.RemoveAt(1);
        lvArquivos.SelectedItems[0].SubItems.Add("Sincronizado");
        PopulaEditsComItemDaBusca();
        Editor.Salvar(ItemDaBusca.Artistas[0].Nome, ItemDaBusca.Album.Nome, ItemDaBusca.Nome);
        lvArquivos.SelectedItems.Clear();
        Editor.Tags.Clear();
      }
    }

    private void AlteraAcessibilidadeControls(Boolean habilitarCampos) {
      foreach (Control control  in Controls) {
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
      if (Editor.Tags.Count == 1) {
        ProgressBarAsyncUmaPesquisa();
      } else if (Editor.Tags.Count > 1) {
        PesquisarVariasMusicas();
      }
    }

    private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e) {
      BackgroundWorker backgroundWorker = sender as BackgroundWorker;
      backgroundWorker.ReportProgress(( progressBar.Maximum) / 100);
    }

    private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
      progressBar.Value = e.ProgressPercentage;
    }

    private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
      // TODO: do something with final calculation.
    }
  }
  /*
   to improve
   Todo: Its really necessary load 3 images? 

   editorTags
   todo: return the count of the elements from the folder selected;

   formPrincipal
   Todo: Add more fields pf tag properties;
     - Ano, numero, genrero, fornecedor, artistas do album separados por virgula 
      (informaçoes spotify, codificado por editor de tags, url artista no spotify),
      Compositor, 
  */
}