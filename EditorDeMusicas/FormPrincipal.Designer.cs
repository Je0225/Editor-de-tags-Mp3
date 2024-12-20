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
      clmStatus = new ColumnHeader();
      gbFaixas = new GroupBox();
      lblProgressBar = new Label();
      progressBar = new ProgressBar();
      btnPesquisar = new Button();
      lblQuantidadeArquivosRes = new Label();
      lblQtdArquivosText = new Label();
      gbPropriedades = new GroupBox();
      lblArtistasParticipantes = new Label();
      tbArtistasParticipantes = new TextBox();
      lblAno = new Label();
      btnProcurarImagem = new Button();
      tbAno = new TextBox();
      pbCapa = new PictureBox();
      tbNumero = new TextBox();
      lblNumero = new Label();
      tbAlbum = new TextBox();
      lblAlbum = new Label();
      tbArtistaAlbum = new TextBox();
      lblArtistaAlbum = new Label();
      tbTitulo = new TextBox();
      btnSalvar = new Button();
      lblTitulo = new Label();
      openFileDialog = new OpenFileDialog();
      openFileDialog1 = new OpenFileDialog();
      gbFaixas.SuspendLayout();
      gbPropriedades.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)pbCapa).BeginInit();
      SuspendLayout();
      // 
      // tbFiltro
      // 
      tbFiltro.Location = new Point(9, 19);
      tbFiltro.Name = "tbFiltro";
      tbFiltro.Size = new Size(280, 23);
      tbFiltro.TabIndex = 0;
      // 
      // btnProcurar
      // 
      btnProcurar.Location = new Point(295, 19);
      btnProcurar.Name = "btnProcurar";
      btnProcurar.Size = new Size(98, 23);
      btnProcurar.TabIndex = 1;
      btnProcurar.Text = "Procurar";
      btnProcurar.UseVisualStyleBackColor = true;
      btnProcurar.Click += btnProcurar_Click;
      // 
      // lvArquivos
      // 
      lvArquivos.Columns.AddRange(new ColumnHeader[] { clmNomeArquivo, clmStatus });
      lvArquivos.FullRowSelect = true;
      lvArquivos.Location = new Point(0, 49);
      lvArquivos.Name = "lvArquivos";
      lvArquivos.Size = new Size(407, 307);
      lvArquivos.TabIndex = 2;
      lvArquivos.UseCompatibleStateImageBehavior = false;
      lvArquivos.View = View.Details;
      lvArquivos.ItemChecked += lvArquivos_RightClicked;
      lvArquivos.SelectedIndexChanged += lvArquivos_SelectedIndexChanged;
      lvArquivos.Click += lvArquivos_RightClicked;
      lvArquivos.MouseDown += lvArquivos_MouseDown;
      // 
      // clmNomeArquivo
      // 
      clmNomeArquivo.Text = "Nome do Arquivo";
      clmNomeArquivo.Width = 250;
      // 
      // clmStatus
      // 
      clmStatus.Text = "Status";
      clmStatus.Width = 150;
      // 
      // gbFaixas
      // 
      gbFaixas.Controls.Add(lblProgressBar);
      gbFaixas.Controls.Add(progressBar);
      gbFaixas.Controls.Add(btnPesquisar);
      gbFaixas.Controls.Add(lblQuantidadeArquivosRes);
      gbFaixas.Controls.Add(lblQtdArquivosText);
      gbFaixas.Controls.Add(tbFiltro);
      gbFaixas.Controls.Add(btnProcurar);
      gbFaixas.Controls.Add(lvArquivos);
      gbFaixas.Location = new Point(3, 1);
      gbFaixas.Name = "gbFaixas";
      gbFaixas.Size = new Size(407, 410);
      gbFaixas.TabIndex = 5;
      gbFaixas.TabStop = false;
      gbFaixas.Text = "Faixas";
      // 
      // lblProgressBar
      // 
      lblProgressBar.AutoSize = true;
      lblProgressBar.Location = new Point(181, 358);
      lblProgressBar.Name = "lblProgressBar";
      lblProgressBar.Size = new Size(38, 15);
      lblProgressBar.TabIndex = 9;
      lblProgressBar.Text = "label1";
      // 
      // progressBar
      // 
      progressBar.Enabled = false;
      progressBar.Location = new Point(0, 356);
      progressBar.Name = "progressBar";
      progressBar.Size = new Size(407, 19);
      progressBar.TabIndex = 8;
      // 
      // btnPesquisar
      // 
      btnPesquisar.Location = new Point(6, 381);
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
      lblQuantidadeArquivosRes.BackColor = SystemColors.ButtonHighlight;
      lblQuantidadeArquivosRes.Location = new Point(363, 387);
      lblQuantidadeArquivosRes.Name = "lblQuantidadeArquivosRes";
      lblQuantidadeArquivosRes.Size = new Size(38, 15);
      lblQuantidadeArquivosRes.TabIndex = 6;
      lblQuantidadeArquivosRes.Text = "label1";
      lblQuantidadeArquivosRes.TextAlign = ContentAlignment.MiddleRight;
      // 
      // lblQtdArquivosText
      // 
      lblQtdArquivosText.Location = new Point(221, 386);
      lblQtdArquivosText.Name = "lblQtdArquivosText";
      lblQtdArquivosText.Size = new Size(136, 15);
      lblQtdArquivosText.TabIndex = 5;
      lblQtdArquivosText.Text = "Quantidade de arquivos:";
      // 
      // gbPropriedades
      // 
      gbPropriedades.Controls.Add(lblArtistasParticipantes);
      gbPropriedades.Controls.Add(tbArtistasParticipantes);
      gbPropriedades.Controls.Add(lblAno);
      gbPropriedades.Controls.Add(btnProcurarImagem);
      gbPropriedades.Controls.Add(tbAno);
      gbPropriedades.Controls.Add(pbCapa);
      gbPropriedades.Controls.Add(tbNumero);
      gbPropriedades.Controls.Add(lblNumero);
      gbPropriedades.Controls.Add(tbAlbum);
      gbPropriedades.Controls.Add(lblAlbum);
      gbPropriedades.Controls.Add(tbArtistaAlbum);
      gbPropriedades.Controls.Add(lblArtistaAlbum);
      gbPropriedades.Controls.Add(tbTitulo);
      gbPropriedades.Controls.Add(btnSalvar);
      gbPropriedades.Controls.Add(lblTitulo);
      gbPropriedades.Location = new Point(416, 1);
      gbPropriedades.Name = "gbPropriedades";
      gbPropriedades.Size = new Size(326, 410);
      gbPropriedades.TabIndex = 6;
      gbPropriedades.TabStop = false;
      gbPropriedades.Text = "Propriedades";
      // 
      // lblArtistasParticipantes
      // 
      lblArtistasParticipantes.AutoSize = true;
      lblArtistasParticipantes.Location = new Point(4, 76);
      lblArtistasParticipantes.Name = "lblArtistasParticipantes";
      lblArtistasParticipantes.Size = new Size(117, 15);
      lblArtistasParticipantes.TabIndex = 21;
      lblArtistasParticipantes.Text = "Artistas Participantes";
      // 
      // tbArtistasParticipantes
      // 
      tbArtistasParticipantes.Location = new Point(126, 73);
      tbArtistasParticipantes.Name = "tbArtistasParticipantes";
      tbArtistasParticipantes.Size = new Size(188, 23);
      tbArtistasParticipantes.TabIndex = 20;
      tbArtistasParticipantes.Leave += TextBox_Leave;
      // 
      // lblAno
      // 
      lblAno.AutoSize = true;
      lblAno.Location = new Point(91, 134);
      lblAno.Name = "lblAno";
      lblAno.Size = new Size(29, 15);
      lblAno.TabIndex = 15;
      lblAno.Text = "Ano";
      // 
      // btnProcurarImagem
      // 
      btnProcurarImagem.Location = new Point(91, 196);
      btnProcurarImagem.Name = "btnProcurarImagem";
      btnProcurarImagem.Size = new Size(75, 41);
      btnProcurarImagem.TabIndex = 10;
      btnProcurarImagem.Text = "Procurar Imagem";
      btnProcurarImagem.UseVisualStyleBackColor = true;
      btnProcurarImagem.Click += btnProcurarImagem_Click;
      // 
      // tbAno
      // 
      tbAno.Location = new Point(126, 134);
      tbAno.Name = "tbAno";
      tbAno.Size = new Size(188, 23);
      tbAno.TabIndex = 11;
      // 
      // pbCapa
      // 
      pbCapa.Location = new Point(172, 196);
      pbCapa.Name = "pbCapa";
      pbCapa.Size = new Size(142, 143);
      pbCapa.SizeMode = PictureBoxSizeMode.StretchImage;
      pbCapa.TabIndex = 9;
      pbCapa.TabStop = false;
      // 
      // tbNumero
      // 
      tbNumero.Location = new Point(126, 163);
      tbNumero.Name = "tbNumero";
      tbNumero.Size = new Size(188, 23);
      tbNumero.TabIndex = 8;
      // 
      // lblNumero
      // 
      lblNumero.AutoSize = true;
      lblNumero.Location = new Point(69, 165);
      lblNumero.Name = "lblNumero";
      lblNumero.Size = new Size(51, 15);
      lblNumero.TabIndex = 7;
      lblNumero.Text = "Número";
      // 
      // tbAlbum
      // 
      tbAlbum.Location = new Point(126, 105);
      tbAlbum.Name = "tbAlbum";
      tbAlbum.Size = new Size(188, 23);
      tbAlbum.TabIndex = 6;
      // 
      // lblAlbum
      // 
      lblAlbum.AutoSize = true;
      lblAlbum.Location = new Point(77, 108);
      lblAlbum.Name = "lblAlbum";
      lblAlbum.Size = new Size(43, 15);
      lblAlbum.TabIndex = 5;
      lblAlbum.Text = "Album";
      // 
      // tbArtistaAlbum
      // 
      tbArtistaAlbum.Location = new Point(126, 44);
      tbArtistaAlbum.Name = "tbArtistaAlbum";
      tbArtistaAlbum.Size = new Size(188, 23);
      tbArtistaAlbum.TabIndex = 4;
      tbArtistaAlbum.Leave += TextBox_Leave;
      // 
      // lblArtistaAlbum
      // 
      lblArtistaAlbum.AutoSize = true;
      lblArtistaAlbum.Location = new Point(23, 47);
      lblArtistaAlbum.Name = "lblArtistaAlbum";
      lblArtistaAlbum.Size = new Size(97, 15);
      lblArtistaAlbum.TabIndex = 3;
      lblArtistaAlbum.Text = "Artista do Album";
      // 
      // tbTitulo
      // 
      tbTitulo.Location = new Point(126, 15);
      tbTitulo.Name = "tbTitulo";
      tbTitulo.Size = new Size(188, 23);
      tbTitulo.TabIndex = 2;
      // 
      // btnSalvar
      // 
      btnSalvar.Location = new Point(6, 381);
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
      lblTitulo.Location = new Point(83, 18);
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
      ClientSize = new Size(747, 418);
      Controls.Add(gbPropriedades);
      Controls.Add(gbFaixas);
      MaximizeBox = false;
      Name = "FormPrincipal";
      Text = "Editor de Tags MP3";
      gbFaixas.ResumeLayout(false);
      gbFaixas.PerformLayout();
      gbPropriedades.ResumeLayout(false);
      gbPropriedades.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)pbCapa).EndInit();
      ResumeLayout(false);
    }

    #endregion

    private TextBox tbFiltro;
        private Button btnProcurar;
        private ListView lvArquivos;
        private ColumnHeader clmNomeArquivo;
        private GroupBox gbFaixas;
        private Label lblQuantidadeArquivosRes;
        private Label lblQtdArquivosText;
        private GroupBox gbPropriedades;
        private TextBox tbTitulo;
        private Button btnSalvar;
        private Label lblTitulo;
        private TextBox tbNumero;
        private Label lblNumero;
        private TextBox tbAlbum;
        private Label lblAlbum;
        private TextBox tbArtistaAlbum;
        private Label lblArtistaAlbum;
        private PictureBox pbCapa;
        private Button btnProcurarImagem;
    private OpenFileDialog openFileDialog;
    private OpenFileDialog openFileDialog1;
    private Button btnPesquisar;
    private Label lblAno;
    private TextBox tbAno;
    private Label lblArtistasParticipantes;
    private TextBox tbArtistasParticipantes;
    private ColumnHeader clmStatus;
    private ProgressBar progressBar;
    private Label lblProgressBar;
  }
}
