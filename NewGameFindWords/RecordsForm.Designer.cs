namespace NewGameFindWords
{
    partial class RecordsForm
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
            this.listBoxRecords = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBoxRecords
            // 
            this.listBoxRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxRecords.FormattingEnabled = true;
            this.listBoxRecords.ItemHeight = 24;
            this.listBoxRecords.Location = new System.Drawing.Point(55, 62);
            this.listBoxRecords.Name = "listBoxRecords";
            this.listBoxRecords.Size = new System.Drawing.Size(680, 316);
            this.listBoxRecords.TabIndex = 0;
            this.listBoxRecords.SelectedIndexChanged += new System.EventHandler(this.listBoxRecords_SelectedIndexChanged);
            // 
            // RecordsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::NewGameFindWords.Properties.Resources.main3;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listBoxRecords);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "RecordsForm";
            this.Text = "Результаты";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxRecords;
    }
}