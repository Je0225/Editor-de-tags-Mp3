using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using File = TagLib.File;
using Tag = TagLib.Id3v2.Tag;

namespace EditorDeMusicas {

  public partial class FrmTracksRetornadas : Form {

    public Items? SelectedItem { get; private set; }
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
          track.Album.Imagens[0].Data) {
          Tag = track
        });
      }
      foreach (ViewTrack panel in pnlItems.Controls.OfType<ViewTrack>()) {
        foreach (Control control in panel.Controls) {
          control.Click += ViewTrack_Click;
        }
      }
    }

    private void ViewTrack_Click(Object sender, EventArgs e) {
      foreach (ViewTrack control in pnlItems.Controls.OfType<ViewTrack>().Where(control => control.EstaSelecionado = true)) {
        control.EstaSelecionado = false;
        control.BackColor = SystemColors.Control;
      }
      Control ctrl = (Control)sender;
      SelectedViewTrack = (ViewTrack)ctrl.Parent;
      SelectedViewTrack.EstaSelecionado = true;
      SelectedItem = (Items)SelectedViewTrack.Tag;
      SelectedViewTrack.Focus();
      SelectedViewTrack.BackColor = SystemColors.GradientInactiveCaption;
    }

    private void Selecionar() {
      DialogResult = DialogResult.OK;
      Close();
    }

    private void btnSelecionar_Click(object sender, EventArgs e) {
      Selecionar();
    }

  }

}