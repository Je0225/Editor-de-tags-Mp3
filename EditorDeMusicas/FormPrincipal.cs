using File = TagLib.File;

namespace EditorDeMusicas {

  public partial class FormPrincipal: Form {

    private EditorTags Editor { get; set; }

    private SpotifyApiServices Api = new SpotifyApiServices();
    public FormPrincipal() {
      InitializeComponent();
      Editor = new EditorTags();
      CarregaCapaGenerica();
    }

    private void ProcuraDiretorio() {
      Editor.ProcuraDiretorio(tbFiltro.Text);
      tbFiltro.Text = Editor.Diretorio;
    }

    private void PopulaListView() {
      lvArquivos.Items.Clear();
      Editor.tags.Clear();

      String[]? nomesArquivos = Editor.RetornaNomesDosArquivos();
      if (nomesArquivos == null)
        return;
      foreach (String nome in nomesArquivos) {
        lvArquivos.Items.Add(new ListViewItem(new[] { nome }));
      }
      lblQuantidadeArquivosRes.Text = nomesArquivos.Length.ToString();
    }

    private void MudaSelecionadas() {
      Editor.tags.Clear();
      ListView.SelectedListViewItemCollection sl = lvArquivos.SelectedItems;;
      String[] selecionados = new String[sl.Count];
      for (int i = 0; i < sl.Count; i++) {
        selecionados[i] = sl[i].Text;
      }
      Editor.PopulaListaTags(selecionados);
      foreach (File file in Editor.tags) {
        if (lvArquivos.SelectedItems.Count == 1) {
          tbTitulo.Text = file.Tag.Title;
          tbArtista.Text = file.Tag.Performers.Length > 0 ? file.Tag.Performers[0] : "";
          tbAlbum.Text = file.Tag.Album;
          CarregaCapaDaTag(file);
        } else {
          LimpaCamposEditor();
        }
      }
    }

    private void LimpaCamposEditor() {
      tbTitulo.Clear();
      tbArtista.Clear();
      tbAlbum.Clear();
      CarregaCapaGenerica();
    }

    private void Salvar() {
      Editor.Salvar(tbArtista.Text, tbAlbum.Text, tbTitulo.Text);
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

    private void Pesquisar() {
      if (Editor.tags.Count is > 1 or 0) {
        return;
      }
      List<Items>? tracks = Api.BuscaItems(Editor.tags[0].Tag.Performers[0], Editor.tags[0].Tag.Title);
      if (tracks == null) {
        return;
      }
      FrmTracksRetornadas fmFrmTracksRetornadas = new(tracks);
      fmFrmTracksRetornadas.ShowDialog();
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

  }
  // todo: Manage the token request;
  // todo[editor]: if while editing tags the files in the directory changes?
}