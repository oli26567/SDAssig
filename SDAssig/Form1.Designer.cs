namespace SDAssig
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
            textSearch = new TextBox();
            btnSearch = new Button();
            btnIndex = new Button();
            lstResults = new ListBox();
            SuspendLayout();
            // 
            // textSearch
            // 
            textSearch.Location = new Point(12, 81);
            textSearch.Name = "textSearch";
            textSearch.Size = new Size(202, 27);
            textSearch.TabIndex = 0;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(230, 80);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(94, 29);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "btnSearch";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click_1;
            // 
            // btnIndex
            // 
            btnIndex.Location = new Point(362, 79);
            btnIndex.Name = "btnIndex";
            btnIndex.Size = new Size(94, 29);
            btnIndex.TabIndex = 2;
            btnIndex.Text = "btnIndex";
            btnIndex.UseVisualStyleBackColor = true;
            btnIndex.Click += btnIndex_Click;
            // 
            // lstResults
            // 
            lstResults.FormattingEnabled = true;
            lstResults.Location = new Point(12, 142);
            lstResults.Name = "lstResults";
            lstResults.Size = new Size(776, 284);
            lstResults.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lstResults);
            Controls.Add(btnIndex);
            Controls.Add(btnSearch);
            Controls.Add(textSearch);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textSearch;
        private Button btnSearch;
        private Button btnIndex;
        private ListBox lstResults;
    }
}
