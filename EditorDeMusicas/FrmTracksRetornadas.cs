namespace EditorDeMusicas {

  public partial class FrmTracksRetornadas : Form {

    public Items? ItemSelecionado { get; private set; }

    private ViewTrack? ViewTrackSelecionado { get; set; }

    private List<Items> Tracks { get; set; }

    public FrmTracksRetornadas( List<Items> tracks) {
      InitializeComponent();
      Tracks = tracks;
      CriaPaineisDosItens();
    }

    private void CriaPaineisDosItens() {
      foreach (Items track in Tracks) {
        Byte[]? bytes = track.Album.Imagens.Count > 0 ? track.Album.Imagens[0].Bytes : null;
        pnlItems.Controls.Add(new ViewTrack( track.Nome, track.Album.Nome, track.Artistas[0].Nome, bytes) { Tag = track });
      }
      foreach (ViewTrack panel in pnlItems.Controls.OfType<ViewTrack>()) {
        foreach (Control control in panel.Controls) {
          control.Click += ViewTrack_Click;
        }
      }
    }

    private void SelecionaViewTrack(Control ctrl) {
      foreach (ViewTrack control in pnlItems.Controls.OfType<ViewTrack>().Where(control => control.EstaSelecionado = true)) {
        control.EstaSelecionado = false;
        control.BackColor = SystemColors.Control;
      }
      ViewTrackSelecionado = (ViewTrack)ctrl.Parent!;
      ViewTrackSelecionado.EstaSelecionado = true;
      ItemSelecionado = (Items)ViewTrackSelecionado.Tag!;
      ViewTrackSelecionado.Focus();
      ViewTrackSelecionado.BackColor = SystemColors.GradientInactiveCaption;
    }

    private void Selecionar() {
      DialogResult = DialogResult.OK;
      Close();
    }

    private void ViewTrack_Click(Object sender, EventArgs e) {
      SelecionaViewTrack((Control)sender);
    }

    private void btnSelecionar_Click(object sender, EventArgs e) {
      Selecionar();
    }

  }

}