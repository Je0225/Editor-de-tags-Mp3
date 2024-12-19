namespace EditorDeMusicas {

  public class ViewTrack: Panel {

    private Label LblInformacoes { get; set; }

    private PictureBox pbCapa { get; set; }

    public Boolean EstaSelecionado { get; set; }

    public ViewTrack(String titulo, String album, String artista, Byte[] DataCapa) {

      LblInformacoes = new Label {
        Text = $"Titulo : {titulo}\nAlbum : {album}\nArtista : {artista}\n",
        AutoSize = false,
        Size = new Size(246, 78),
        Dock = DockStyle.Left,
      };

      pbCapa = new PictureBox {
        Image = Image.FromStream(new MemoryStream(DataCapa)),
        SizeMode = PictureBoxSizeMode.StretchImage,
        Dock = DockStyle.Right,
        Size = new Size(104, 78),
      };

      LblInformacoes.MouseHover += OnMouseHover_ViewTrack;
      LblInformacoes.MouseLeave += OnMouseLeave_ViewTrack;
      pbCapa.MouseHover += OnMouseHover_ViewTrack;
      pbCapa.MouseLeave += OnMouseLeave_ViewTrack;

      Size = new Size(350, 78);
      Location = new Point(12, 12);
      BorderStyle = BorderStyle.FixedSingle;
      Controls.AddRange(new Control[] { pbCapa , LblInformacoes });
    }

    private void MouseHoverFrm() {
      if (Focused) {
        return;
      }
      BackColor = SystemColors.InactiveCaption;
    }

    private void MouseLeaveFrm() {
      if (Focused) {
        return;
      }
      BackColor = SystemColors.Control;
    }

    private void OnMouseHover_ViewTrack(Object? sender, EventArgs e) {
      MouseHoverFrm();
    }

    private void OnMouseLeave_ViewTrack(Object? sender, EventArgs e) {
      MouseLeaveFrm();
    }

  }

}