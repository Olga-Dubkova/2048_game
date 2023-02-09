namespace _2048WinFormsApp
{
    partial class ResultsForm
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
            this.resultsDataGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.resultsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // resultsDataGridView
            // 
            this.resultsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.resultsDataGridView.Location = new System.Drawing.Point(1, 2);
            this.resultsDataGridView.Name = "resultsDataGridView";
            this.resultsDataGridView.RowHeadersWidth = 51;
            this.resultsDataGridView.RowTemplate.Height = 29;
            this.resultsDataGridView.Size = new System.Drawing.Size(263, 282);
            this.resultsDataGridView.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Имя";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Width = 125;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Кол-во очков";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Width = 125;
            // 
            // ResultsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 292);
            this.Controls.Add(this.resultsDataGridView);
            this.Name = "ResultsForm";
            this.Text = "ResultsForm";
            this.Load += new System.EventHandler(this.ResultsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.resultsDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView resultsDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}