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
      panel1 = new Panel();
      btnSelecionar = new Button();
      pnlItems = new FlowLayoutPanel();
      pnlBottom.SuspendLayout();
      panel1.SuspendLayout();
      SuspendLayout();
      // 
      // pnlBottom
      // 
      pnlBottom.Controls.Add(panel1);
      pnlBottom.Dock = DockStyle.Bottom;
      pnlBottom.Location = new Point(0, 513);
      pnlBottom.Name = "pnlBottom";
      pnlBottom.Size = new Size(371, 38);
      pnlBottom.TabIndex = 4;
      // 
      // panel1
      // 
      panel1.Controls.Add(btnSelecionar);
      panel1.Dock = DockStyle.Right;
      panel1.Location = new Point(272, 0);
      panel1.Name = "panel1";
      panel1.Size = new Size(99, 38);
      panel1.TabIndex = 5;
      // 
      // btnSelecionar
      // 
      btnSelecionar.Location = new Point(12, 6);
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
      pnlItems.Size = new Size(371, 513);
      pnlItems.TabIndex = 5;
      // 
      // FrmTracksRetornadas
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      AutoScroll = true;
      ClientSize = new Size(371, 551);
      Controls.Add(pnlItems);
      Controls.Add(pnlBottom);
      MinimizeBox = false;
      MinimumSize = new Size(387, 590);
      Name = "FrmTracksRetornadas";
      Text = "Resultados";
      pnlBottom.ResumeLayout(false);
      panel1.ResumeLayout(false);
      ResumeLayout(false);
    }

    #endregion
    private Panel pnlBottom;
    private FlowLayoutPanel pnlItems;
    private Button btnSelecionar;
    private Panel panel1;
  }
}