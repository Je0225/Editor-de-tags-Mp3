using System.Windows.Forms;
using File = TagLib.File;
using Tag = TagLib.Id3v2.Tag;

namespace EditorDeMusicas {

  public partial class FrmTracksRetornadas : Form {

    private ViewTrack? SelectedItem { get; set; }

    private List<Items>? Tracks { get; set; }

    private File File { get; set; }

    public FrmTracksRetornadas( File file ) {
      InitializeComponent();
      Tracks = new List<Items>();
      File = file;

      FazBusca();
      CriaPanelsTracks();
    }

    private void CriaPanelsTracks() {
      foreach (Items track in Tracks) {
        pnlItems.Controls.Add(new ViewTrack(
          track.Nome,
          track.Album.Nome,
          track.Artistas[0].Nome,
          track.Album.Imagens[1].Data));
      }
      foreach (Control panel in pnlItems.Controls) {
        panel.Click += ViewTrack_Click;
      }
    }

    private void FazBusca() {
      Tracks.Clear();
      SpotifyApiServices api = new SpotifyApiServices();
      Tracks = api.BuscaItems(File.Tag.Artists[0], File.Tag.Title).Result;
    }

    private void btnFazOutraBusca_Click(Object sender, EventArgs e) {
      
    }

    private void btnSelecionar_Click(object sender, EventArgs e) {

    }

    private void ViewTrack_Click(Object? sender, EventArgs e) {
      foreach (Control control in pnlItems.Controls) {
        control.BackColor = SystemColors.Control;
      }
      SelectedItem = (ViewTrack)sender!;
      SelectedItem.Focus();
      SelectedItem.BackColor = SystemColors.GradientInactiveCaption;
    }

  }

}