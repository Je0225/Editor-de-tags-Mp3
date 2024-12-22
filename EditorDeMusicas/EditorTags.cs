using TagLib;
using File = TagLib.File;
using Tag = TagLib.Id3v2.Tag;

namespace EditorDeMusicas {

  internal class EditorTags {

    public String Diretorio { get; private set; }

    public List<File> Tags { get; set; }

    public IDictionary<String, String> NomesTags { get; }

    public Image ImagemEscolhida { get; private set; }

    private Byte[] BytesImagem { get; set; }

    private String pathCapaGenerica { get; }

    public EditorTags() {
      pathCapaGenerica = Environment.CurrentDirectory + "\\Resources\\imageMp3.jpg";
      Tags = new List<File> {};
      NomesTags = new Dictionary<String, String>();

      Tag.DefaultVersion = 3;
      Tag.ForceDefaultVersion = true;
    }

    public void ProcuraDiretorio(String diretorio) {
      if (!String.IsNullOrEmpty(diretorio) && Diretorio != diretorio) {
        Diretorio = diretorio;
        return;
      }
      FolderBrowserDialog browserDialog = new FolderBrowserDialog();
      browserDialog.ShowHiddenFiles = true;
      if (browserDialog.ShowDialog() != DialogResult.OK) {
        return;
      }
      Diretorio = browserDialog.SelectedPath;
    }

    public String[]? RetornaNomesDosArquivos() {
      NomesTags.Clear();
      if (String.IsNullOrEmpty(Diretorio))
        return null;
      foreach (var caminho in Directory.GetFiles(Diretorio).Where(path => Path.GetExtension(path).Trim() == ".mp3").ToArray()) {
        NomesTags.Add(Path.GetFileName(caminho), caminho);
      }
      return NomesTags.Keys.ToArray();
    }

    public void PopulaListaTags(String[] selecionadas) {
      Tags.Clear();
      foreach (String nome in selecionadas) {
        if (NomesTags.TryGetValue(nome, out String? tag)) {
          var file = File.Create(tag);
          Tags.Add(file);
        }
      }
    }
    // TODO: Create a class to pass the tag info
    public void Salvar(String artistas, String album, String titulo = "", String artistasParticipantes = "", String ano = "",  String numero = "") {
      if (Tags.Count == 0) {
        return;
      }
      String? mensagem = null;
      foreach ( File faixa in Tags) {
        faixa.Tag.AlbumArtists = artistas.Split(";");
        faixa.Tag.Performers = artistasParticipantes.Split(";");
        faixa.Tag.Album = album;
        faixa.Tag.Year = Convert.ToUInt32(ano);
        SetaCapaNaTag(faixa);
        if (Tags.Count == 1) {
          faixa.Tag.Title = titulo;
          faixa.Tag.Track = Convert.ToUInt32(numero);
          faixa.Save();
          string newpath = Diretorio + "\\" + titulo + ".mp3";
          System.IO.File.Move(faixa.Name, newpath);
          mensagem = "Arquivo alterado com sucesso";
        } else {
          faixa.Save();
          mensagem = "Arquivos selecionados alterados com sucesso";
        }
      }
      MessageBox.Show(mensagem);
    }

    public Image? RetornaCapaDaTag(File file) {
      if (file.Tag.Pictures.Length > 0 && file.Tag.Pictures[0].Data.Count > 0) {
        Byte[] bytes = file.Tag.Pictures[0].Data.ToArray();
        ImagemEscolhida = Image.FromStream(new MemoryStream(bytes));
        BytesImagem = bytes;
        return ImagemEscolhida;
      }
      return null;
    }

    public Image RetornaCapaGenerica() {
      return Image.FromFile(pathCapaGenerica);
    }

    public void ProcuraImagem() {
      OpenFileDialog ofd = new OpenFileDialog();
      ofd.Multiselect = false;
      ofd.Title = @"Selecione a capa do album";
      ofd.Filter = @"Images (*.BMP;*.JPG;*.GIF,*.PNG,*.TIFF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF|" + @"All files (*.*)|*.*";
      ofd.CheckFileExists = true;
      ofd.CheckPathExists = true;
      if (ofd.ShowDialog() != DialogResult.OK)
        return;

      Byte[] bytes = System.IO.File.ReadAllBytes(ofd.FileName);
      ImagemEscolhida = Image.FromStream(new MemoryStream(bytes));
      BytesImagem = bytes;
    }

    public void RecebeImagemDeUmaBusca(Byte[] bytes) {
      ImagemEscolhida = Image.FromStream(new MemoryStream(bytes));
      BytesImagem = bytes;
    }

    private void SetaCapaNaTag(File file) {
      file.Tag.Pictures = null;
      file.Tag.Pictures = new IPicture[] {
        new Picture(new ByteVector(BytesImagem)) {
          Type = PictureType.FrontCover,
          MimeType = "image/jpg" // ou "image/png", dependendo da imagem
        }
      };
    }
  }

}