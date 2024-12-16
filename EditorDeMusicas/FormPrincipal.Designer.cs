namespace EditorDeMusicas
{
    partial class FormPrincipal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      tbFiltro = new TextBox();
      btnProcurar = new Button();
      lvArquivos = new ListView();
      clmNomeArquivo = new ColumnHeader();
      groupBox1 = new GroupBox();
      btnPesquisar = new Button();
      lblQuantidadeArquivosRes = new Label();
      lblQtdArquivosText = new Label();
      groupBox2 = new GroupBox();
      btnProcurarImagem = new Button();
      pbCapa = new PictureBox();
      tbTrack = new TextBox();
      lblTrack = new Label();
      tbAlbum = new TextBox();
      lblAlbum = new Label();
      tbArtista = new TextBox();
      lblArtista = new Label();
      tbTitulo = new TextBox();
      btnSalvar = new Button();
      lblTitulo = new Label();
      openFileDialog = new OpenFileDialog();
      openFileDialog1 = new OpenFileDialog();
      groupBox1.SuspendLayout();
      groupBox2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)pbCapa).BeginInit();
      SuspendLayout();
      // 
      // tbFiltro
      // 
      tbFiltro.Location = new Point(9, 31);
      tbFiltro.Name = "tbFiltro";
      tbFiltro.Size = new Size(280, 23);
      tbFiltro.TabIndex = 0;
      // 
      // btnProcurar
      // 
      btnProcurar.Location = new Point(295, 31);
      btnProcurar.Name = "btnProcurar";
      btnProcurar.Size = new Size(98, 23);
      btnProcurar.TabIndex = 1;
      btnProcurar.Text = "Procurar";
      btnProcurar.UseVisualStyleBackColor = true;
      btnProcurar.Click += btnProcurar_Click;
      // 
      // lvArquivos
      // 
      lvArquivos.Columns.AddRange(new ColumnHeader[] { clmNomeArquivo });
      lvArquivos.FullRowSelect = true;
      lvArquivos.Location = new Point(9, 60);
      lvArquivos.Name = "lvArquivos";
      lvArquivos.Size = new Size(384, 211);
      lvArquivos.TabIndex = 2;
      lvArquivos.UseCompatibleStateImageBehavior = false;
      lvArquivos.View = View.Details;
      lvArquivos.SelectedIndexChanged += lvArquivos_SelectedIndexChanged;
      // 
      // clmNomeArquivo
      // 
      clmNomeArquivo.Text = "Nome";
      clmNomeArquivo.Width = 120;
      // 
      // groupBox1
      // 
      groupBox1.Controls.Add(btnPesquisar);
      groupBox1.Controls.Add(lblQuantidadeArquivosRes);
      groupBox1.Controls.Add(lblQtdArquivosText);
      groupBox1.Controls.Add(tbFiltro);
      groupBox1.Controls.Add(btnProcurar);
      groupBox1.Controls.Add(lvArquivos);
      groupBox1.Location = new Point(12, 12);
      groupBox1.Name = "groupBox1";
      groupBox1.Size = new Size(407, 366);
      groupBox1.TabIndex = 5;
      groupBox1.TabStop = false;
      groupBox1.Text = "groupBox1";
      // 
      // btnPesquisar
      // 
      btnPesquisar.Location = new Point(9, 277);
      btnPesquisar.Name = "btnPesquisar";
      btnPesquisar.Size = new Size(93, 23);
      btnPesquisar.TabIndex = 7;
      btnPesquisar.Text = "Pesquisar";
      btnPesquisar.UseVisualStyleBackColor = true;
      btnPesquisar.Click += btnPesquisar_Click;
      // 
      // lblQuantidadeArquivosRes
      // 
      lblQuantidadeArquivosRes.AutoSize = true;
      lblQuantidadeArquivosRes.Location = new Point(135, 348);
      lblQuantidadeArquivosRes.Name = "lblQuantidadeArquivosRes";
      lblQuantidadeArquivosRes.Size = new Size(38, 15);
      lblQuantidadeArquivosRes.TabIndex = 6;
      lblQuantidadeArquivosRes.Text = "label1";
      // 
      // lblQtdArquivosText
      // 
      lblQtdArquivosText.AutoSize = true;
      lblQtdArquivosText.Location = new Point(0, 348);
      lblQtdArquivosText.Name = "lblQtdArquivosText";
      lblQtdArquivosText.Size = new Size(136, 15);
      lblQtdArquivosText.TabIndex = 5;
      lblQtdArquivosText.Text = "Quantidade de arquivos:";
      // 
      // groupBox2
      // 
      groupBox2.Controls.Add(btnProcurarImagem);
      groupBox2.Controls.Add(pbCapa);
      groupBox2.Controls.Add(tbTrack);
      groupBox2.Controls.Add(lblTrack);
      groupBox2.Controls.Add(tbAlbum);
      groupBox2.Controls.Add(lblAlbum);
      groupBox2.Controls.Add(tbArtista);
      groupBox2.Controls.Add(lblArtista);
      groupBox2.Controls.Add(tbTitulo);
      groupBox2.Controls.Add(btnSalvar);
      groupBox2.Controls.Add(lblTitulo);
      groupBox2.Location = new Point(425, 18);
      groupBox2.Name = "groupBox2";
      groupBox2.Size = new Size(281, 360);
      groupBox2.TabIndex = 6;
      groupBox2.TabStop = false;
      groupBox2.Text = "groupBox2";
      // 
      // btnProcurarImagem
      // 
      btnProcurarImagem.Location = new Point(168, 167);
      btnProcurarImagem.Name = "btnProcurarImagem";
      btnProcurarImagem.Size = new Size(75, 41);
      btnProcurarImagem.TabIndex = 10;
      btnProcurarImagem.Text = "Procurar Imagem";
      btnProcurarImagem.UseVisualStyleBackColor = true;
      btnProcurarImagem.Click += btnProcurarImagem_Click;
      // 
      // pbCapa
      // 
      pbCapa.Location = new Point(20, 167);
      pbCapa.Name = "pbCapa";
      pbCapa.Size = new Size(142, 143);
      pbCapa.SizeMode = PictureBoxSizeMode.StretchImage;
      pbCapa.TabIndex = 9;
      pbCapa.TabStop = false;
      // 
      // tbTrack
      // 
      tbTrack.Location = new Point(64, 117);
      tbTrack.Name = "tbTrack";
      tbTrack.Size = new Size(188, 23);
      tbTrack.TabIndex = 8;
      // 
      // lblTrack
      // 
      lblTrack.AutoSize = true;
      lblTrack.Location = new Point(20, 120);
      lblTrack.Name = "lblTrack";
      lblTrack.Size = new Size(34, 15);
      lblTrack.TabIndex = 7;
      lblTrack.Text = "Track";
      // 
      // tbAlbum
      // 
      tbAlbum.Location = new Point(64, 88);
      tbAlbum.Name = "tbAlbum";
      tbAlbum.Size = new Size(188, 23);
      tbAlbum.TabIndex = 6;
      // 
      // lblAlbum
      // 
      lblAlbum.AutoSize = true;
      lblAlbum.Location = new Point(20, 91);
      lblAlbum.Name = "lblAlbum";
      lblAlbum.Size = new Size(43, 15);
      lblAlbum.TabIndex = 5;
      lblAlbum.Text = "Album";
      // 
      // tbArtista
      // 
      tbArtista.Location = new Point(64, 59);
      tbArtista.Name = "tbArtista";
      tbArtista.Size = new Size(188, 23);
      tbArtista.TabIndex = 4;
      // 
      // lblArtista
      // 
      lblArtista.AutoSize = true;
      lblArtista.Location = new Point(20, 62);
      lblArtista.Name = "lblArtista";
      lblArtista.Size = new Size(41, 15);
      lblArtista.TabIndex = 3;
      lblArtista.Text = "Artista";
      // 
      // tbTitulo
      // 
      tbTitulo.Location = new Point(64, 30);
      tbTitulo.Name = "tbTitulo";
      tbTitulo.Size = new Size(188, 23);
      tbTitulo.TabIndex = 2;
      // 
      // btnSalvar
      // 
      btnSalvar.Location = new Point(6, 331);
      btnSalvar.Name = "btnSalvar";
      btnSalvar.Size = new Size(75, 23);
      btnSalvar.TabIndex = 1;
      btnSalvar.Text = "Salvar";
      btnSalvar.UseVisualStyleBackColor = true;
      btnSalvar.Click += btnSalvar_Click;
      // 
      // lblTitulo
      // 
      lblTitulo.AutoSize = true;
      lblTitulo.Location = new Point(20, 33);
      lblTitulo.Name = "lblTitulo";
      lblTitulo.Size = new Size(37, 15);
      lblTitulo.TabIndex = 0;
      lblTitulo.Text = "Titulo";
      // 
      // openFileDialog
      // 
      openFileDialog.FileName = "openFileDialog";
      // 
      // openFileDialog1
      // 
      openFileDialog1.FileName = "openFileDialog1";
      // 
      // FormPrincipal
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(718, 390);
      Controls.Add(groupBox2);
      Controls.Add(groupBox1);
      MaximizeBox = false;
      Name = "FormPrincipal";
      Text = "Editor de Tags MP3";
      groupBox1.ResumeLayout(false);
      groupBox1.PerformLayout();
      groupBox2.ResumeLayout(false);
      groupBox2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)pbCapa).EndInit();
      ResumeLayout(false);
    }

    #endregion

    private TextBox tbFiltro;
        private Button btnProcurar;
        private ListView lvArquivos;
        private ColumnHeader clmNomeArquivo;
        private GroupBox groupBox1;
        private Label lblQuantidadeArquivosRes;
        private Label lblQtdArquivosText;
        private GroupBox groupBox2;
        private TextBox tbTitulo;
        private Button btnSalvar;
        private Label lblTitulo;
        private TextBox tbTrack;
        private Label lblTrack;
        private TextBox tbAlbum;
        private Label lblAlbum;
        private TextBox tbArtista;
        private Label lblArtista;
        private PictureBox pbCapa;
        private Button btnProcurarImagem;
    private OpenFileDialog openFileDialog;
    private OpenFileDialog openFileDialog1;
    private Button btnPesquisar;
  }
}
