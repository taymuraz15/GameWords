namespace NewGameFindWords
{
    partial class NameInputForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxPlayerName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxPlayerName
            // 
            this.textBoxPlayerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPlayerName.Location = new System.Drawing.Point(239, 189);
            this.textBoxPlayerName.Name = "textBoxPlayerName";
            this.textBoxPlayerName.Size = new System.Drawing.Size(265, 49);
            this.textBoxPlayerName.TabIndex = 0;
            this.textBoxPlayerName.TextChanged += new System.EventHandler(this.textBoxPlayerName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Monotype Corsiva", 36F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(244, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(260, 57);
            this.label1.TabIndex = 1;
            this.label1.Text = "Введите имя";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Font = new System.Drawing.Font("Monotype Corsiva", 26.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOk.Location = new System.Drawing.Point(239, 262);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(265, 51);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "Продолжить";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // NameInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::NewGameFindWords.Properties.Resources.main2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPlayerName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "NameInputForm";
            this.Text = "Введите имя";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPlayerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOk;
    }
}