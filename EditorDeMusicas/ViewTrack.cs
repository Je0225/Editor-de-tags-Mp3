namespace EditorDeMusicas {

  public class ViewTrack: FlowLayoutPanel {

    private Label LblInformacoes { get; set; }

    private PictureBox pbCapa { get; set; }

    public ViewTrack(String titulo, String album, String artista, Byte[] DataCapa) {
      LblInformacoes = new Label {
        Text = $"Titulo : {titulo}\n Album : {album}\n Artista : {artista}\n",
        AutoSize = true
      };

      // Colocar os bytes no picture box
      pbCapa = new PictureBox {
        Size = new Size(70, 70),
        BackColor = Color.Black
      };

      AutoSize = false;
      Size = new Size(350, 78);
      Location = new Point(12, 12);
      BorderStyle = BorderStyle.FixedSingle;
      Controls.AddRange(new Control[] { LblInformacoes, pbCapa });

      MouseHover += OnMouseHover_ViewTrack;
      MouseLeave += OnMouseLeave_ViewTrack;
    }

    private void OnMouseHover_ViewTrack(object sender, EventArgs e) {
      if (Focused) {
        return;
      }
      BackColor = SystemColors.InactiveCaption;
    }

    private void OnMouseLeave_ViewTrack(object sender, EventArgs e) {
      if (Focused) {
        return;
      }
      BackColor = SystemColors.Control;
    }

  }

}