namespace EditorDeMusicas {

  public class ViewTrack: Panel {

    private Label LblInformacoes { get; set; }

    private PictureBox pbCapa { get; set; }

    public Boolean EstaSelecionado { get; set; }

    public ViewTrack(String titulo, String album, String artista, Byte[] DataCapa) {

      LblInformacoes = new Label {
        Text = $"Titulo : {titulo}\nAlbum : {album}\nArtista : {artista}\n",
        AutoSize = false,
        Size = new Size(253, 78),
        Dock = DockStyle.Left,
      };

      pbCapa = new PictureBox {
        Image = Image.FromStream(new MemoryStream(DataCapa)),
        SizeMode = PictureBoxSizeMode.StretchImage,
        Dock = DockStyle.Right,
        Size = new Size(104, 78),
      };

      Size = new Size(350, 78);
      Location = new Point(12, 12);
      BorderStyle = BorderStyle.FixedSingle;
      Controls.AddRange(new Control[] { LblInformacoes, pbCapa });
    }

    private void MouseHoverFrm() {
      if (Focused) {
        return;
      }
      BackColor = SystemColors.InactiveCaption;
      LblInformacoes.BackColor = SystemColors.InactiveCaption;
    }

    private void MouseLeaveFrm() {
      if (Focused) {
        return;
      }
      BackColor = SystemColors.Control;
      LblInformacoes.BackColor = SystemColors.Control;
    }

    protected override void WndProc(ref Message m) {
      if (m.Msg == 0x201 || (m.Msg == 0x210 && (int)m.WParam == 0x201)) {
        if (!ClientRectangle.Contains(PointToClient(Cursor.Position))) {
          BackColor = SystemColors.GradientInactiveCaption;
        }
      }
      if (m.Msg == 0x84) {
        if (!this.ClientRectangle.Contains(PointToClient(Cursor.Position))) {
          BackColor = SystemColors.InactiveCaption;
          ;
        }else {
          BackColor = SystemColors.Control;
        }
      }
      base.WndProc(ref m);
    }

    private void OnMouseHover_ViewTrack(object sender, EventArgs e) {
      MouseHoverFrm();
    }

    private void OnMouseLeave_ViewTrack(object sender, EventArgs e) {
      MouseLeaveFrm();
    }

  }

}