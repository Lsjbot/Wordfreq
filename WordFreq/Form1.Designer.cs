namespace WordFreq
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.QuitButton = new System.Windows.Forms.Button();
            this.ReadCorpusButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.DatabaseButton = new System.Windows.Forms.Button();
            this.button_readmail = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.TB_ngram = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.NgramButton = new System.Windows.Forms.Button();
            this.CB_ngrams = new System.Windows.Forms.CheckBox();
            this.Wordfreqbutton = new System.Windows.Forms.Button();
            this.CB_wordfreq = new System.Windows.Forms.CheckBox();
            this.CB_selwords = new System.Windows.Forms.CheckBox();
            this.meanshiftbutton = new System.Windows.Forms.Button();
            this.CB_usewordfreq = new System.Windows.Forms.CheckBox();
            this.TBmeanshift = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Kmeansbutton = new System.Windows.Forms.Button();
            this.PCAbutton = new System.Windows.Forms.Button();
            this.TBpcachartmax = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CB_bitmap = new System.Windows.Forms.CheckBox();
            this.CB_savecluster = new System.Windows.Forms.CheckBox();
            this.TB_Kmeans = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CB_json = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.TB_ngram)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(351, 349);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // QuitButton
            // 
            this.QuitButton.Location = new System.Drawing.Point(487, 502);
            this.QuitButton.Name = "QuitButton";
            this.QuitButton.Size = new System.Drawing.Size(180, 44);
            this.QuitButton.TabIndex = 2;
            this.QuitButton.Text = "Quit";
            this.QuitButton.UseVisualStyleBackColor = true;
            this.QuitButton.Click += new System.EventHandler(this.QuitButton_Click);
            // 
            // ReadCorpusButton
            // 
            this.ReadCorpusButton.Location = new System.Drawing.Point(487, 411);
            this.ReadCorpusButton.Name = "ReadCorpusButton";
            this.ReadCorpusButton.Size = new System.Drawing.Size(180, 39);
            this.ReadCorpusButton.TabIndex = 3;
            this.ReadCorpusButton.Text = "Read Corpus";
            this.ReadCorpusButton.UseVisualStyleBackColor = true;
            this.ReadCorpusButton.Click += new System.EventHandler(this.ReadCorpusButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = " Text files (*.txt)|*.txt|All files (*.*)|*.*";
            this.openFileDialog1.InitialDirectory = "D:\\Ling\\";
            this.openFileDialog1.Multiselect = true;
            // 
            // DatabaseButton
            // 
            this.DatabaseButton.Location = new System.Drawing.Point(487, 456);
            this.DatabaseButton.Name = "DatabaseButton";
            this.DatabaseButton.Size = new System.Drawing.Size(180, 39);
            this.DatabaseButton.TabIndex = 4;
            this.DatabaseButton.Text = "Fill database from frequency file";
            this.DatabaseButton.UseVisualStyleBackColor = true;
            this.DatabaseButton.Click += new System.EventHandler(this.DatabaseButton_Click);
            // 
            // button_readmail
            // 
            this.button_readmail.Location = new System.Drawing.Point(487, 80);
            this.button_readmail.Name = "button_readmail";
            this.button_readmail.Size = new System.Drawing.Size(180, 42);
            this.button_readmail.TabIndex = 5;
            this.button_readmail.Text = "Read mail file (JA)";
            this.button_readmail.UseVisualStyleBackColor = true;
            this.button_readmail.Click += new System.EventHandler(this.button_readmail_Click);
            // 
            // TB_ngram
            // 
            this.TB_ngram.Location = new System.Drawing.Point(335, 394);
            this.TB_ngram.Maximum = 5;
            this.TB_ngram.Minimum = 2;
            this.TB_ngram.Name = "TB_ngram";
            this.TB_ngram.Size = new System.Drawing.Size(104, 45);
            this.TB_ngram.TabIndex = 6;
            this.TB_ngram.Value = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(341, 383);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "2      n-gram      5";
            // 
            // NgramButton
            // 
            this.NgramButton.Location = new System.Drawing.Point(487, 368);
            this.NgramButton.Name = "NgramButton";
            this.NgramButton.Size = new System.Drawing.Size(180, 37);
            this.NgramButton.TabIndex = 8;
            this.NgramButton.Text = "Analyze n-gram file";
            this.NgramButton.UseVisualStyleBackColor = true;
            this.NgramButton.Click += new System.EventHandler(this.NgramButton_Click);
            // 
            // CB_ngrams
            // 
            this.CB_ngrams.AutoSize = true;
            this.CB_ngrams.Location = new System.Drawing.Point(344, 433);
            this.CB_ngrams.Name = "CB_ngrams";
            this.CB_ngrams.Size = new System.Drawing.Size(98, 17);
            this.CB_ngrams.TabIndex = 9;
            this.CB_ngrams.Text = "Collect n-grams";
            this.CB_ngrams.UseVisualStyleBackColor = true;
            // 
            // Wordfreqbutton
            // 
            this.Wordfreqbutton.Location = new System.Drawing.Point(487, 326);
            this.Wordfreqbutton.Name = "Wordfreqbutton";
            this.Wordfreqbutton.Size = new System.Drawing.Size(180, 35);
            this.Wordfreqbutton.TabIndex = 10;
            this.Wordfreqbutton.Text = "Analyze wordfreq file";
            this.Wordfreqbutton.UseVisualStyleBackColor = true;
            this.Wordfreqbutton.Click += new System.EventHandler(this.Wordfreqbutton_Click);
            // 
            // CB_wordfreq
            // 
            this.CB_wordfreq.AutoSize = true;
            this.CB_wordfreq.Location = new System.Drawing.Point(344, 456);
            this.CB_wordfreq.Name = "CB_wordfreq";
            this.CB_wordfreq.Size = new System.Drawing.Size(121, 17);
            this.CB_wordfreq.TabIndex = 11;
            this.CB_wordfreq.Text = "Save wordfreq table";
            this.CB_wordfreq.UseVisualStyleBackColor = true;
            // 
            // CB_selwords
            // 
            this.CB_selwords.AutoSize = true;
            this.CB_selwords.Location = new System.Drawing.Point(344, 479);
            this.CB_selwords.Name = "CB_selwords";
            this.CB_selwords.Size = new System.Drawing.Size(121, 17);
            this.CB_selwords.TabIndex = 12;
            this.CB_selwords.Text = "Selected words only";
            this.CB_selwords.UseVisualStyleBackColor = true;
            // 
            // meanshiftbutton
            // 
            this.meanshiftbutton.Location = new System.Drawing.Point(487, 283);
            this.meanshiftbutton.Margin = new System.Windows.Forms.Padding(2);
            this.meanshiftbutton.Name = "meanshiftbutton";
            this.meanshiftbutton.Size = new System.Drawing.Size(180, 38);
            this.meanshiftbutton.TabIndex = 29;
            this.meanshiftbutton.Text = "Mean Shift clustering";
            this.meanshiftbutton.UseVisualStyleBackColor = true;
            this.meanshiftbutton.Click += new System.EventHandler(this.meanshiftbutton_Click);
            // 
            // CB_usewordfreq
            // 
            this.CB_usewordfreq.AutoSize = true;
            this.CB_usewordfreq.Checked = true;
            this.CB_usewordfreq.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_usewordfreq.Location = new System.Drawing.Point(345, 502);
            this.CB_usewordfreq.Name = "CB_usewordfreq";
            this.CB_usewordfreq.Size = new System.Drawing.Size(113, 17);
            this.CB_usewordfreq.TabIndex = 30;
            this.CB_usewordfreq.Text = "Use wordfreq data";
            this.CB_usewordfreq.UseVisualStyleBackColor = true;
            // 
            // TBmeanshift
            // 
            this.TBmeanshift.Location = new System.Drawing.Point(220, 527);
            this.TBmeanshift.Margin = new System.Windows.Forms.Padding(2);
            this.TBmeanshift.Name = "TBmeanshift";
            this.TBmeanshift.Size = new System.Drawing.Size(76, 20);
            this.TBmeanshift.TabIndex = 33;
            this.TBmeanshift.Text = "500";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(109, 529);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 13);
            this.label6.TabIndex = 32;
            this.label6.Text = "Mean-shift bandwidth";
            // 
            // Kmeansbutton
            // 
            this.Kmeansbutton.Location = new System.Drawing.Point(487, 243);
            this.Kmeansbutton.Name = "Kmeansbutton";
            this.Kmeansbutton.Size = new System.Drawing.Size(180, 35);
            this.Kmeansbutton.TabIndex = 34;
            this.Kmeansbutton.Text = "K-means clustering";
            this.Kmeansbutton.UseVisualStyleBackColor = true;
            this.Kmeansbutton.Click += new System.EventHandler(this.Kmeansbutton_Click);
            // 
            // PCAbutton
            // 
            this.PCAbutton.Location = new System.Drawing.Point(487, 202);
            this.PCAbutton.Name = "PCAbutton";
            this.PCAbutton.Size = new System.Drawing.Size(180, 35);
            this.PCAbutton.TabIndex = 35;
            this.PCAbutton.Text = "PCA";
            this.PCAbutton.UseVisualStyleBackColor = true;
            this.PCAbutton.Click += new System.EventHandler(this.PCAbutton_Click);
            // 
            // TBpcachartmax
            // 
            this.TBpcachartmax.Location = new System.Drawing.Point(221, 507);
            this.TBpcachartmax.Name = "TBpcachartmax";
            this.TBpcachartmax.Size = new System.Drawing.Size(75, 20);
            this.TBpcachartmax.TabIndex = 36;
            this.TBpcachartmax.Text = "100";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(109, 510);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "PCA chart max value";
            // 
            // CB_bitmap
            // 
            this.CB_bitmap.AutoSize = true;
            this.CB_bitmap.Location = new System.Drawing.Point(344, 525);
            this.CB_bitmap.Name = "CB_bitmap";
            this.CB_bitmap.Size = new System.Drawing.Size(118, 17);
            this.CB_bitmap.TabIndex = 38;
            this.CB_bitmap.Text = "Make bitmap image";
            this.CB_bitmap.UseVisualStyleBackColor = true;
            // 
            // CB_savecluster
            // 
            this.CB_savecluster.AutoSize = true;
            this.CB_savecluster.Location = new System.Drawing.Point(123, 456);
            this.CB_savecluster.Name = "CB_savecluster";
            this.CB_savecluster.Size = new System.Drawing.Size(173, 17);
            this.CB_savecluster.TabIndex = 39;
            this.CB_savecluster.Text = "Save cluster feature evaluation";
            this.CB_savecluster.UseVisualStyleBackColor = true;
            // 
            // TB_Kmeans
            // 
            this.TB_Kmeans.Location = new System.Drawing.Point(220, 490);
            this.TB_Kmeans.Name = "TB_Kmeans";
            this.TB_Kmeans.Size = new System.Drawing.Size(76, 20);
            this.TB_Kmeans.TabIndex = 40;
            this.TB_Kmeans.Text = "10";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(109, 493);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 41;
            this.label3.Text = "K-means clusters";
            // 
            // CB_json
            // 
            this.CB_json.AutoSize = true;
            this.CB_json.Location = new System.Drawing.Point(123, 433);
            this.CB_json.Name = "CB_json";
            this.CB_json.Size = new System.Drawing.Size(73, 17);
            this.CB_json.TabIndex = 42;
            this.CB_json.Text = "Save json";
            this.CB_json.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 100000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 558);
            this.Controls.Add(this.CB_json);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TB_Kmeans);
            this.Controls.Add(this.CB_savecluster);
            this.Controls.Add(this.CB_bitmap);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TBpcachartmax);
            this.Controls.Add(this.PCAbutton);
            this.Controls.Add(this.Kmeansbutton);
            this.Controls.Add(this.TBmeanshift);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.CB_usewordfreq);
            this.Controls.Add(this.meanshiftbutton);
            this.Controls.Add(this.CB_selwords);
            this.Controls.Add(this.CB_wordfreq);
            this.Controls.Add(this.Wordfreqbutton);
            this.Controls.Add(this.CB_ngrams);
            this.Controls.Add(this.NgramButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TB_ngram);
            this.Controls.Add(this.button_readmail);
            this.Controls.Add(this.DatabaseButton);
            this.Controls.Add(this.ReadCorpusButton);
            this.Controls.Add(this.QuitButton);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.TB_ngram)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button QuitButton;
        private System.Windows.Forms.Button ReadCorpusButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button DatabaseButton;
        private System.Windows.Forms.Button button_readmail;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TrackBar TB_ngram;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button NgramButton;
        private System.Windows.Forms.CheckBox CB_ngrams;
        private System.Windows.Forms.Button Wordfreqbutton;
        private System.Windows.Forms.CheckBox CB_wordfreq;
        private System.Windows.Forms.CheckBox CB_selwords;
        private System.Windows.Forms.Button meanshiftbutton;
        private System.Windows.Forms.CheckBox CB_usewordfreq;
        private System.Windows.Forms.TextBox TBmeanshift;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button Kmeansbutton;
        private System.Windows.Forms.Button PCAbutton;
        private System.Windows.Forms.TextBox TBpcachartmax;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox CB_bitmap;
        private System.Windows.Forms.CheckBox CB_savecluster;
        private System.Windows.Forms.TextBox TB_Kmeans;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox CB_json;
        private System.Windows.Forms.Timer timer1;
    }
}

