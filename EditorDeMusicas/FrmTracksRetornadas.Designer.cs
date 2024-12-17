namespace EditorDeMusicas {
  partial class FrmTracksRetornadas {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      pnlBottom = new Panel();
      btnSelecionar = new Button();
      pnlItems = new FlowLayoutPanel();
      pnlBottom.SuspendLayout();
      SuspendLayout();
      // 
      // pnlBottom
      // 
      pnlBottom.Controls.Add(btnSelecionar);
      pnlBottom.Dock = DockStyle.Bottom;
      pnlBottom.Location = new Point(0, 513);
      pnlBottom.Name = "pnlBottom";
      pnlBottom.Size = new Size(364, 38);
      pnlBottom.TabIndex = 4;
      // 
      // btnSelecionar
      // 
      btnSelecionar.Location = new Point(277, 6);
      btnSelecionar.Name = "btnSelecionar";
      btnSelecionar.Size = new Size(75, 23);
      btnSelecionar.TabIndex = 4;
      btnSelecionar.Text = "Selecionar";
      btnSelecionar.UseVisualStyleBackColor = true;
      btnSelecionar.Click += btnSelecionar_Click;
      // 
      // pnlItems
      // 
      pnlItems.AutoScroll = true;
      pnlItems.Dock = DockStyle.Fill;
      pnlItems.Location = new Point(0, 0);
      pnlItems.Name = "pnlItems";
      pnlItems.Size = new Size(364, 513);
      pnlItems.TabIndex = 5;
      // 
      // FrmTracksRetornadas
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      AutoScroll = true;
      ClientSize = new Size(364, 551);
      Controls.Add(pnlItems);
      Controls.Add(pnlBottom);
      MaximizeBox = false;
      Name = "FrmTracksRetornadas";
      Text = "Resultados";
      pnlBottom.ResumeLayout(false);
      ResumeLayout(false);
    }

    #endregion
    private Panel pnlBottom;
    private FlowLayoutPanel pnlItems;
    private Button btnSelecionar;
  }
}