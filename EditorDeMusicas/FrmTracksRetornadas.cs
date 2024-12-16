using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using File = TagLib.File;
using Tag = TagLib.Id3v2.Tag;

namespace EditorDeMusicas {

  public partial class FrmTracksRetornadas : Form {

    public Items? SelectedItem { get; set; }
    private ViewTrack? SelectedViewTrack { get; set; }

    private List<Items>? Tracks { get; set; }

    public FrmTracksRetornadas( List<Items> tracks) {
      InitializeComponent();
      Tracks = tracks;
      CriaPanelsTracks();
    }

    private void CriaPanelsTracks() {
      foreach (Items track in Tracks) {
        pnlItems.Controls.Add(new ViewTrack(
          track.Nome,
          track.Album.Nome,
          track.Artistas[0].Nome,
          track.Album.Imagens[1].Data) {
          Tag = track
        });
      }
      //foreach (ViewTrack panel in pnlItems.Controls.OfType<ViewTrack>()) {
       // panel.Click += ViewTrack_Click;
      //}
    }

    private void ViewTrack_Click(Object sender, EventArgs e) {
      foreach (ViewTrack control in pnlItems.Controls.OfType<ViewTrack>().Where(control => control.EstaSelecionado = true)) {
        control.EstaSelecionado = false;
        control.BackColor = SystemColors.Control;
      }
      SelectedViewTrack = (ViewTrack)sender;
      SelectedViewTrack.EstaSelecionado = true;
      SelectedItem = (Items)SelectedViewTrack.Tag;
      SelectedViewTrack.Focus();
      SelectedViewTrack.BackColor = SystemColors.GradientInactiveCaption;
    }

    private void Selecionar() {
      DialogResult = DialogResult.OK;
      Close();
    }

    private void FazerOutraBusca() {
      DialogResult = DialogResult.Continue;
    }
    private void btnFazOutraBusca_Click(Object sender, EventArgs e) {
      FazerOutraBusca();
    }

    private void btnSelecionar_Click(object sender, EventArgs e) {
      Selecionar();
    }

  }

}