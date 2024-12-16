using File = TagLib.File;

namespace EditorDeMusicas {

  public partial class FormPrincipal: Form {

    private EditorTags Editor { get; set; }

    private readonly SpotifyApiServices api = new ();

    private Items ItemDaBusca { get; set; }


    public FormPrincipal() {
      InitializeComponent();
      Editor = new EditorTags();
      CarregaCapaGenerica();
      lblQuantidadeArquivosRes.Text = "";
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

    private void LimpaCamposEditor(Boolean carregarCapaGenerica = true) {
      tbTitulo.Clear();
      tbArtista.Clear();
      tbAlbum.Clear();
      tbTrack.Clear();
      if (carregarCapaGenerica) {
        CarregaCapaGenerica();
      }
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
      List<Items>? tracks = api.BuscaItems(tbArtista.Text, tbTitulo.Text);
      if (tracks == null) {
        return;
      }
      FrmTracksRetornadas fmTracksRetornadas = new(tracks);
      fmTracksRetornadas.ShowDialog();
      if (fmTracksRetornadas.DialogResult != DialogResult.OK || fmTracksRetornadas.SelectedItem == null) {
        return;
      }
      ItemDaBusca = fmTracksRetornadas.SelectedItem;
      AddInformacoesBusca();
    }

    private void AddInformacoesBusca() {
      LimpaCamposEditor(false);
      tbTitulo.Text = ItemDaBusca.Nome;
      tbAlbum.Text = ItemDaBusca.Album.Nome;
      tbArtista.Text = ItemDaBusca.Artistas.First().Nome;
      tbTrack.Text = ItemDaBusca.NumeroDisco.ToString();
      Editor.RecebeImagemDeUmaBusca(ItemDaBusca.Album.Imagens[0].Data);
      pbCapa.Image = Editor.ImagemEscolhida;
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
  /*
   for test
   todo[test]: Manage the token request;

   to improve
   todo[editor]: if while editing tags the files in the directory changes?

   editorTags
   todo: return the count of the elements from the folder selected;

   frmTracks
   Todo: make all the trackView elements change wen the mouse events starts;
   Todo: fix the mouse and click events;

  viewTrack
   Todo: Its really necessary load 3 images? 

   formPrincipal
   Todo: Add more fields pf tag properties;
   Todo: Don't close the frmTracks while the select button its not pressed;
  */
}