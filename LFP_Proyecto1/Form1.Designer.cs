namespace LFP_Proyecto1
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirArchivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarComoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDocumentoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generarTraducciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generarReportesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tablaDeTokensReconocidosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tablaDeSímbolosOVariablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tablaDeErroresLéxicosYSintácticosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.limpiarDocsRecientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lbPais = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbSaturacion = new System.Windows.Forms.Label();
            this.lbPoblacion = new System.Windows.Forms.Label();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.menuDocumentoToolStripMenuItem,
            this.acercaDeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1350, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirArchivoToolStripMenuItem,
            this.guardarComoToolStripMenuItem,
            this.salirToolStripMenuItem1});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(107, 20);
            this.menuToolStripMenuItem.Text = "MENU ARCHIVO";
            // 
            // abrirArchivoToolStripMenuItem
            // 
            this.abrirArchivoToolStripMenuItem.Name = "abrirArchivoToolStripMenuItem";
            this.abrirArchivoToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.abrirArchivoToolStripMenuItem.Text = "Abrir Archivo";
            this.abrirArchivoToolStripMenuItem.Click += new System.EventHandler(this.abrirArchivoToolStripMenuItem_Click);
            // 
            // guardarComoToolStripMenuItem
            // 
            this.guardarComoToolStripMenuItem.Name = "guardarComoToolStripMenuItem";
            this.guardarComoToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.guardarComoToolStripMenuItem.Text = "Guardar como...";
            this.guardarComoToolStripMenuItem.Click += new System.EventHandler(this.guardarComoToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem1
            // 
            this.salirToolStripMenuItem1.Name = "salirToolStripMenuItem1";
            this.salirToolStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.salirToolStripMenuItem1.Text = "Salir";
            this.salirToolStripMenuItem1.Click += new System.EventHandler(this.salirToolStripMenuItem1_Click);
            // 
            // menuDocumentoToolStripMenuItem
            // 
            this.menuDocumentoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generarTraducciónToolStripMenuItem,
            this.generarReportesToolStripMenuItem,
            this.limpiarDocsRecientesToolStripMenuItem});
            this.menuDocumentoToolStripMenuItem.Name = "menuDocumentoToolStripMenuItem";
            this.menuDocumentoToolStripMenuItem.Size = new System.Drawing.Size(129, 20);
            this.menuDocumentoToolStripMenuItem.Text = "MENU DOCUMENTO";
            // 
            // generarTraducciónToolStripMenuItem
            // 
            this.generarTraducciónToolStripMenuItem.Name = "generarTraducciónToolStripMenuItem";
            this.generarTraducciónToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.generarTraducciónToolStripMenuItem.Text = "Generar Traducción";
            this.generarTraducciónToolStripMenuItem.Click += new System.EventHandler(this.generarTraducciónToolStripMenuItem_Click);
            // 
            // generarReportesToolStripMenuItem
            // 
            this.generarReportesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tablaDeTokensReconocidosToolStripMenuItem,
            this.tablaDeSímbolosOVariablesToolStripMenuItem,
            this.tablaDeErroresLéxicosYSintácticosToolStripMenuItem});
            this.generarReportesToolStripMenuItem.Name = "generarReportesToolStripMenuItem";
            this.generarReportesToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.generarReportesToolStripMenuItem.Text = "Generar Reportes";
            // 
            // tablaDeTokensReconocidosToolStripMenuItem
            // 
            this.tablaDeTokensReconocidosToolStripMenuItem.Name = "tablaDeTokensReconocidosToolStripMenuItem";
            this.tablaDeTokensReconocidosToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.tablaDeTokensReconocidosToolStripMenuItem.Text = "Tabla de Tokens Reconocidos";
            this.tablaDeTokensReconocidosToolStripMenuItem.Click += new System.EventHandler(this.tablaDeTokensReconocidosToolStripMenuItem_Click);
            // 
            // tablaDeSímbolosOVariablesToolStripMenuItem
            // 
            this.tablaDeSímbolosOVariablesToolStripMenuItem.Name = "tablaDeSímbolosOVariablesToolStripMenuItem";
            this.tablaDeSímbolosOVariablesToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.tablaDeSímbolosOVariablesToolStripMenuItem.Text = "Tabla de Símbolos o Variables";
            this.tablaDeSímbolosOVariablesToolStripMenuItem.Click += new System.EventHandler(this.tablaDeSímbolosOVariablesToolStripMenuItem_Click);
            // 
            // tablaDeErroresLéxicosYSintácticosToolStripMenuItem
            // 
            this.tablaDeErroresLéxicosYSintácticosToolStripMenuItem.Name = "tablaDeErroresLéxicosYSintácticosToolStripMenuItem";
            this.tablaDeErroresLéxicosYSintácticosToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.tablaDeErroresLéxicosYSintácticosToolStripMenuItem.Text = "Tabla de Errores Léxicos y Sintácticos";
            this.tablaDeErroresLéxicosYSintácticosToolStripMenuItem.Click += new System.EventHandler(this.tablaDeErroresLéxicosYSintácticosToolStripMenuItem_Click);
            // 
            // limpiarDocsRecientesToolStripMenuItem
            // 
            this.limpiarDocsRecientesToolStripMenuItem.Name = "limpiarDocsRecientesToolStripMenuItem";
            this.limpiarDocsRecientesToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.limpiarDocsRecientesToolStripMenuItem.Text = "Limpiar Docs Recientes";
            this.limpiarDocsRecientesToolStripMenuItem.Click += new System.EventHandler(this.limpiarDocsRecientesToolStripMenuItem_Click);
            // 
            // acercaDeToolStripMenuItem
            // 
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.acercaDeToolStripMenuItem.Text = "Acerca de";
            this.acercaDeToolStripMenuItem.Click += new System.EventHandler(this.acercaDeToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(37, 37);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(635, 403);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.richTextBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(627, 374);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Código Original en C#";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Yi Baiti", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(621, 369);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(700, 534);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(173, 35);
            this.button2.TabIndex = 3;
            this.button2.Text = "GENERAR PDF";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(959, 613);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(212, 104);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(120, 22);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(170, 89);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // lbPais
            // 
            this.lbPais.AutoSize = true;
            this.lbPais.Location = new System.Drawing.Point(17, 39);
            this.lbPais.Name = "lbPais";
            this.lbPais.Size = new System.Drawing.Size(43, 20);
            this.lbPais.TabIndex = 6;
            this.lbPais.Text = "País:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbSaturacion);
            this.groupBox1.Controls.Add(this.lbPoblacion);
            this.groupBox1.Controls.Add(this.lbPais);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(638, 584);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(302, 111);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SELECCIÓN";
            // 
            // lbSaturacion
            // 
            this.lbSaturacion.AutoSize = true;
            this.lbSaturacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSaturacion.Location = new System.Drawing.Point(18, 100);
            this.lbSaturacion.Name = "lbSaturacion";
            this.lbSaturacion.Size = new System.Drawing.Size(75, 16);
            this.lbSaturacion.TabIndex = 8;
            this.lbSaturacion.Text = "Saturación:";
            // 
            // lbPoblacion
            // 
            this.lbPoblacion.AutoSize = true;
            this.lbPoblacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPoblacion.Location = new System.Drawing.Point(18, 70);
            this.lbPoblacion.Name = "lbPoblacion";
            this.lbPoblacion.Size = new System.Drawing.Size(72, 16);
            this.lbPoblacion.TabIndex = 7;
            this.lbPoblacion.Text = "Población:";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Font = new System.Drawing.Font("Microsoft Yi Baiti", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox2.Location = new System.Drawing.Point(700, 66);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(618, 398);
            this.richTextBox2.TabIndex = 8;
            this.richTextBox2.Text = "";
            // 
            // richTextBox3
            // 
            this.richTextBox3.Location = new System.Drawing.Point(37, 493);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(584, 174);
            this.richTextBox3.TabIndex = 9;
            this.richTextBox3.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PeachPuff;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ANALIZADOR GRAFICO";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirArchivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarComoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lbPais;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbPoblacion;
        private System.Windows.Forms.Label lbSaturacion;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuDocumentoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generarTraducciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generarReportesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tablaDeTokensReconocidosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tablaDeSímbolosOVariablesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tablaDeErroresLéxicosYSintácticosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem limpiarDocsRecientesToolStripMenuItem;
        private System.Windows.Forms.RichTextBox richTextBox3;
    }
}

