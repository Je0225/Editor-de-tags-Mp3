using System.Data;
using File = TagLib.File;

namespace EditorDeMusicas {

  public partial class FormPrincipal : Form {

    private EditorTags Editor { get; }

    private readonly SpotifyApiServices api = new();

    private Items ItemDaBusca { get; set; }

    private Dictionary<File, List<Items>> ResultadosBuscas { get; set; }

    private Boolean RightClicked { get; set; }

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
      Editor.Tags.Clear();

      String[]? nomesArquivos = Editor.RetornaNomesDosArquivos();
      if (nomesArquivos == null)
        return;
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
      foreach (File file in Editor.Tags) {
        if (lvArquivos.SelectedItems.Count == 1) {
          tbTitulo.Text = file.Tag.Title;
          tbArtistaAlbum.Text = file.Tag.Performers.Length > 0 ? file.Tag.Performers[0] : "";
          tbAlbum.Text = file.Tag.Album;
          CarregaCapaDaTag(file);
        } else {
          LimpaCamposEditor();
        }
      }
    }

    private void LimpaCamposEditor(Boolean carregarCapaGenerica = true) {
      tbTitulo.Clear();
      tbArtistasParticipantes.Clear();
      tbArtistaAlbum.Clear();
      tbAlbum.Clear();
      tbNumero.Clear();
      tbAno.Clear();
      tbCompositor.Clear();
      tbGenero.Clear();
      if (carregarCapaGenerica) {
        CarregaCapaGenerica();
      }
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

    private async Task PesquisarUmaMusica() {
      List<Items>? tracks = await api.BuscaItems(tbArtistaAlbum.Text, tbTitulo.Text);
      if (tracks == null) {
        return;
      }
      FrmTracksRetornadas frmTracksRetornadas = new(tracks);
      frmTracksRetornadas.ShowDialog();
      if (frmTracksRetornadas.DialogResult != DialogResult.OK || frmTracksRetornadas.SelectedItem == null) {
        return;
      }
      ItemDaBusca = frmTracksRetornadas.SelectedItem;
      AddInformacoesBusca(false);
    }

    private async void PesquisarVarias() {
      foreach (File tag in Editor.Tags) {
        String artista = tag.Tag.Performers != null && tag.Tag.Performers.Length > 0 ? tag.Tag.Performers[0] : "";
        string titulo = String.IsNullOrEmpty(tag.Tag.Title) ? Path.GetFileName(tag.Name) : tag.Tag.Title;

        KeyValuePair<File, List<Items>> resultadosBusca = new KeyValuePair<File, List<Items>>(tag, await api.BuscaItems(artista, titulo));

        foreach (ListViewItem lv in lvArquivos.Items.OfType<ListViewItem>().Where(lv => lv.Text == Path.GetFileName(tag.Name))) {
          lv.BackColor = Color.Gold;
          lv.SubItems.Add(Status.Pendente.ToString());
          lv.Tag = resultadosBusca;
        }
      }
    }
    private void lvArquivos_MouseDown(object sender, MouseEventArgs e) {
      RightClicked = e.Button == MouseButtons.Right;
    }
    private void lvArquivos_RightClicked(object sender, EventArgs e) {
      if (RightClicked) {
        ListView lvSelecionado = (ListView)sender;
        if (lvSelecionado.SelectedItems[0].Tag == null || lvSelecionado.SelectedItems.Count > 1) {
          return;
        }
        KeyValuePair<File, List<Items>> kvp = (KeyValuePair<File, List<Items>>)lvSelecionado.SelectedItems[0].Tag;

        FrmTracksRetornadas frmTracksRetornadas = new(kvp.Value);
        frmTracksRetornadas.ShowDialog();
        if (frmTracksRetornadas.DialogResult != DialogResult.OK || frmTracksRetornadas.SelectedItem == null) {
          return;
        }
        ItemDaBusca = frmTracksRetornadas.SelectedItem;
        lvSelecionado.SelectedItems[0].BackColor = Color.MediumSeaGreen;
        lvSelecionado.SelectedItems[0].SubItems.RemoveAt(1);
        lvSelecionado.SelectedItems[0].SubItems.Add("Sincronizado");
        AddInformacoesBusca(false);
        Editor.Salvar(ItemDaBusca.Artistas[0].Nome, ItemDaBusca.Album.Nome, ItemDaBusca.Nome);
        lvArquivos.SelectedItems.Clear();
        Editor.Tags.Clear();
      }
    }

    private void AddInformacoesBusca(Boolean CarregaCapaGenerica) {
      LimpaCamposEditor(CarregaCapaGenerica);
      tbTitulo.Text = ItemDaBusca.Nome;
      tbAlbum.Text = ItemDaBusca.Album.Nome;
      tbArtistaAlbum.Text = ItemDaBusca.Artistas.First().Nome;
      tbNumero.Text = ItemDaBusca.NumeroDisco.ToString();
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
      if (Editor.Tags.Count == 1) {
        PesquisarUmaMusica();
      } else if (Editor.Tags.Count > 1) {
        PesquisarVarias();
        LimpaCamposEditor(true);
      }
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