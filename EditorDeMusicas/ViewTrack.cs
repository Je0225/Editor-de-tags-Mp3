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

    const int WM_PARENTNOTIFY = 0x210;
    const int WM_LBUTTONDOWN = 0x21;

    protected override void WndProc(ref Message m) {
      if (m.Msg == WM_LBUTTONDOWN || (m.Msg == WM_PARENTNOTIFY && (int)m.WParam == WM_LBUTTONDOWN))
        if (this.ClientRectangle.Contains(PointToClient(Cursor.Position))) 
          MessageBox.Show("Clicou Aqui!");
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