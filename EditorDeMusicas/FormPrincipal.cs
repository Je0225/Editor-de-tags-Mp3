using File = TagLib.File;

namespace EditorDeMusicas {

  public partial class FormPrincipal : Form {

    private EditorTags editor { get; set; }

    public FormPrincipal() {
      InitializeComponent();
      editor = new EditorTags();
      CarregaCapaGenerica();
    }


    private void ProcuraDiretorio() {
      editor.ProcuraDiretorio(tbFiltro.Text);
      tbFiltro.Text = editor.Diretorio;
    }

    private void PopulaListView() {
      lvArquivos.Items.Clear();
      editor.tags.Clear();

      String[]? nomesArquivos = editor.RetornaNomesDosArquivos();
      if (nomesArquivos == null)
        return;
      foreach (String nome in nomesArquivos) {
        lvArquivos.Items.Add(new ListViewItem(new[] { nome }));
      }
      lblQuantidadeArquivosRes.Text = nomesArquivos.Length.ToString();
    }

    private void MudaSelecionadas() {
      editor.tags.Clear();
      ListView.SelectedListViewItemCollection sl = lvArquivos.SelectedItems;
      String[] selecionados = new String[sl.Count];
      for (int i = 0; i < sl.Count; i++) {
        selecionados[i] = sl[i].Text;
      }
      editor.PopulaListaTags(selecionados);
      foreach (File file in editor.tags) {
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
      editor.Salvar(tbArtista.Text, tbAlbum.Text, tbTitulo.Text);
      PopulaListView();
      LimpaCamposEditor();
    }

    private void CarregaCapaDaTag(File file) {
      if (pbCapa.Image != null) {
        pbCapa.Image.Dispose();
      }
      pbCapa.Image = editor.RetornaCapaDaTag(file);
      if (pbCapa.Image == null) {
        CarregaCapaGenerica();
      }
    }

    private void CarregaCapaGenerica() {
      if (pbCapa.Image != null) {
        pbCapa.Image.Dispose();
      }
      pbCapa.Image = editor.RetornaCapaGenerica();
    }

    private void ProcuraImagem() {
      editor.ProcuraImagem();
      pbCapa.Image = editor.ImagemEscolhida;
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

    public async Task pesquisarAsync()
        {
            RequestTheAudioDB r = new RequestTheAudioDB();
            await r.RequestTokens();
            // esperar o metodo asyncrono terminar a execução.
            r.FazRequisicaoArtista("Manchester Orchestra", "Gold");
        }

    private void btnPesquisar_Click(object sender, EventArgs e) {
            pesquisarAsync();
        }
  }

}