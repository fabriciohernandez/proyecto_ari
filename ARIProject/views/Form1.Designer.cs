
namespace ARIProject
{
    partial class Form1
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
        private void InitializeComponent()
        {
            this.body = new System.Windows.Forms.Panel();
            this.lblTitleKey = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.btnGen = new System.Windows.Forms.Button();
            this.lblTitleDeli = new System.Windows.Forms.Label();
            this.cmbDeli = new System.Windows.Forms.ComboBox();
            this.lblTitleFileType = new System.Windows.Forms.Label();
            this.cmbFileType = new System.Windows.Forms.ComboBox();
            this.lblResultTitle = new System.Windows.Forms.Label();
            this.rTxtResult = new System.Windows.Forms.RichTextBox();
            this.fileContent = new System.Windows.Forms.RichTextBox();
            this.btnDestinySearchRoute = new System.Windows.Forms.Button();
            this.txtDestinyRoute = new System.Windows.Forms.TextBox();
            this.btnOriginSearchRoute = new System.Windows.Forms.Button();
            this.lblTitleDestinyRoute = new System.Windows.Forms.Label();
            this.lblTitleOriginRoute = new System.Windows.Forms.Label();
            this.txtOriginRoute = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.body.SuspendLayout();
            this.SuspendLayout();
            // 
            // body
            // 
            this.body.AutoScroll = true;
            this.body.Controls.Add(this.lblTitleKey);
            this.body.Controls.Add(this.txtKey);
            this.body.Controls.Add(this.btnGen);
            this.body.Controls.Add(this.lblTitleDeli);
            this.body.Controls.Add(this.cmbDeli);
            this.body.Controls.Add(this.lblTitleFileType);
            this.body.Controls.Add(this.cmbFileType);
            this.body.Controls.Add(this.lblResultTitle);
            this.body.Controls.Add(this.rTxtResult);
            this.body.Controls.Add(this.fileContent);
            this.body.Controls.Add(this.btnDestinySearchRoute);
            this.body.Controls.Add(this.txtDestinyRoute);
            this.body.Controls.Add(this.btnOriginSearchRoute);
            this.body.Controls.Add(this.lblTitleDestinyRoute);
            this.body.Controls.Add(this.lblTitleOriginRoute);
            this.body.Controls.Add(this.txtOriginRoute);
            this.body.Controls.Add(this.label2);
            this.body.Controls.Add(this.label1);
            this.body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.body.Location = new System.Drawing.Point(0, 0);
            this.body.Name = "body";
            this.body.Size = new System.Drawing.Size(450, 445);
            this.body.TabIndex = 0;
            // 
            // lblTitleKey
            // 
            this.lblTitleKey.AutoSize = true;
            this.lblTitleKey.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitleKey.Location = new System.Drawing.Point(11, 561);
            this.lblTitleKey.Name = "lblTitleKey";
            this.lblTitleKey.Size = new System.Drawing.Size(128, 17);
            this.lblTitleKey.TabIndex = 28;
            this.lblTitleKey.Text = "Clave de cifrado *";
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(12, 581);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(405, 23);
            this.txtKey.TabIndex = 27;
            // 
            // btnGen
            // 
            this.btnGen.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnGen.Location = new System.Drawing.Point(11, 626);
            this.btnGen.Name = "btnGen";
            this.btnGen.Size = new System.Drawing.Size(406, 40);
            this.btnGen.TabIndex = 26;
            this.btnGen.Text = "Generar";
            this.btnGen.UseVisualStyleBackColor = true;
            this.btnGen.Click += new System.EventHandler(this.btnGen_Click);
            // 
            // lblTitleDeli
            // 
            this.lblTitleDeli.AutoSize = true;
            this.lblTitleDeli.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitleDeli.Location = new System.Drawing.Point(11, 505);
            this.lblTitleDeli.Name = "lblTitleDeli";
            this.lblTitleDeli.Size = new System.Drawing.Size(166, 17);
            this.lblTitleDeli.TabIndex = 25;
            this.lblTitleDeli.Text = "Seleccione delimitador *";
            // 
            // cmbDeli
            // 
            this.cmbDeli.FormattingEnabled = true;
            this.cmbDeli.Items.AddRange(new object[] {
            ",",
            ";",
            ":"});
            this.cmbDeli.Location = new System.Drawing.Point(11, 525);
            this.cmbDeli.Name = "cmbDeli";
            this.cmbDeli.Size = new System.Drawing.Size(406, 23);
            this.cmbDeli.TabIndex = 24;
            // 
            // lblTitleFileType
            // 
            this.lblTitleFileType.AutoSize = true;
            this.lblTitleFileType.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitleFileType.Location = new System.Drawing.Point(11, 447);
            this.lblTitleFileType.Name = "lblTitleFileType";
            this.lblTitleFileType.Size = new System.Drawing.Size(207, 17);
            this.lblTitleFileType.TabIndex = 23;
            this.lblTitleFileType.Text = "Seleccione archivo a generar *";
            // 
            // cmbFileType
            // 
            this.cmbFileType.FormattingEnabled = true;
            this.cmbFileType.Items.AddRange(new object[] {
            "JSON",
            "XML"});
            this.cmbFileType.Location = new System.Drawing.Point(11, 467);
            this.cmbFileType.Name = "cmbFileType";
            this.cmbFileType.Size = new System.Drawing.Size(406, 23);
            this.cmbFileType.TabIndex = 22;
            this.cmbFileType.SelectedIndexChanged += new System.EventHandler(this.cmbFileType_SelectedIndexChanged);
            // 
            // lblResultTitle
            // 
            this.lblResultTitle.AutoSize = true;
            this.lblResultTitle.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblResultTitle.Location = new System.Drawing.Point(11, 683);
            this.lblResultTitle.Name = "lblResultTitle";
            this.lblResultTitle.Size = new System.Drawing.Size(72, 17);
            this.lblResultTitle.TabIndex = 20;
            this.lblResultTitle.Text = "Resultado";
            // 
            // rTxtResult
            // 
            this.rTxtResult.Location = new System.Drawing.Point(12, 707);
            this.rTxtResult.Name = "rTxtResult";
            this.rTxtResult.ReadOnly = true;
            this.rTxtResult.Size = new System.Drawing.Size(405, 311);
            this.rTxtResult.TabIndex = 19;
            this.rTxtResult.Text = "";
            // 
            // fileContent
            // 
            this.fileContent.Location = new System.Drawing.Point(12, 164);
            this.fileContent.Name = "fileContent";
            this.fileContent.ReadOnly = true;
            this.fileContent.Size = new System.Drawing.Size(405, 208);
            this.fileContent.TabIndex = 18;
            this.fileContent.Text = "";
            // 
            // btnDestinySearchRoute
            // 
            this.btnDestinySearchRoute.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnDestinySearchRoute.Location = new System.Drawing.Point(342, 410);
            this.btnDestinySearchRoute.Name = "btnDestinySearchRoute";
            this.btnDestinySearchRoute.Size = new System.Drawing.Size(75, 23);
            this.btnDestinySearchRoute.TabIndex = 17;
            this.btnDestinySearchRoute.Text = "Buscar";
            this.btnDestinySearchRoute.UseVisualStyleBackColor = true;
            this.btnDestinySearchRoute.Click += new System.EventHandler(this.btnDestinySearchRoute_Click);
            // 
            // txtDestinyRoute
            // 
            this.txtDestinyRoute.Location = new System.Drawing.Point(11, 411);
            this.txtDestinyRoute.Name = "txtDestinyRoute";
            this.txtDestinyRoute.ReadOnly = true;
            this.txtDestinyRoute.Size = new System.Drawing.Size(325, 23);
            this.txtDestinyRoute.TabIndex = 16;
            // 
            // btnOriginSearchRoute
            // 
            this.btnOriginSearchRoute.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnOriginSearchRoute.Location = new System.Drawing.Point(342, 124);
            this.btnOriginSearchRoute.Name = "btnOriginSearchRoute";
            this.btnOriginSearchRoute.Size = new System.Drawing.Size(75, 23);
            this.btnOriginSearchRoute.TabIndex = 15;
            this.btnOriginSearchRoute.Text = "Buscar";
            this.btnOriginSearchRoute.UseVisualStyleBackColor = true;
            this.btnOriginSearchRoute.Click += new System.EventHandler(this.btnOriginSearchRoute_Click);
            // 
            // lblTitleDestinyRoute
            // 
            this.lblTitleDestinyRoute.AutoSize = true;
            this.lblTitleDestinyRoute.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitleDestinyRoute.Location = new System.Drawing.Point(11, 388);
            this.lblTitleDestinyRoute.Name = "lblTitleDestinyRoute";
            this.lblTitleDestinyRoute.Size = new System.Drawing.Size(99, 17);
            this.lblTitleDestinyRoute.TabIndex = 14;
            this.lblTitleDestinyRoute.Text = "Ruta destino *";
            // 
            // lblTitleOriginRoute
            // 
            this.lblTitleOriginRoute.AutoSize = true;
            this.lblTitleOriginRoute.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitleOriginRoute.Location = new System.Drawing.Point(12, 103);
            this.lblTitleOriginRoute.Name = "lblTitleOriginRoute";
            this.lblTitleOriginRoute.Size = new System.Drawing.Size(93, 17);
            this.lblTitleOriginRoute.TabIndex = 13;
            this.lblTitleOriginRoute.Text = "Ruta origen *";
            // 
            // txtOriginRoute
            // 
            this.txtOriginRoute.Location = new System.Drawing.Point(12, 125);
            this.txtOriginRoute.Name = "txtOriginRoute";
            this.txtOriginRoute.ReadOnly = true;
            this.txtOriginRoute.Size = new System.Drawing.Size(324, 23);
            this.txtOriginRoute.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(12, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(355, 22);
            this.label2.TabIndex = 11;
            this.label2.Text = "Administración de riesgos informáticos";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(11, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 28);
            this.label1.TabIndex = 10;
            this.label1.Text = "Proyecto";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(450, 445);
            this.Controls.Add(this.body);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bienvenido";
            this.body.ResumeLayout(false);
            this.body.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel body;
        private System.Windows.Forms.Label lblResultTitle;
        private System.Windows.Forms.RichTextBox rTxtResult;
        private System.Windows.Forms.RichTextBox fileContent;
        private System.Windows.Forms.Button btnDestinySearchRoute;
        private System.Windows.Forms.TextBox txtDestinyRoute;
        private System.Windows.Forms.Button btnOriginSearchRoute;
        private System.Windows.Forms.Label lblTitleDestinyRoute;
        private System.Windows.Forms.Label lblTitleOriginRoute;
        private System.Windows.Forms.TextBox txtOriginRoute;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTitleFileType;
        private System.Windows.Forms.ComboBox cmbFileType;
        private System.Windows.Forms.Button btnGen;
        private System.Windows.Forms.Label lblTitleDeli;
        private System.Windows.Forms.ComboBox cmbDeli;
        private System.Windows.Forms.Label lblTitleKey;
        private System.Windows.Forms.TextBox txtKey;
    }
}

