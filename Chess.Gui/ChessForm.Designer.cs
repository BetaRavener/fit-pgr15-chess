namespace Chess.Gui
{
    partial class ChessForm
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
            this.RenderView = new System.Windows.Forms.PictureBox();
            this.RenderButton = new System.Windows.Forms.Button();
            this.RenderProgressBar = new System.Windows.Forms.ProgressBar();
            this.ThreadsNumber = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.RenderView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThreadsNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // RenderView
            // 
            this.RenderView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RenderView.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.RenderView.Location = new System.Drawing.Point(0, 0);
            this.RenderView.Name = "RenderView";
            this.RenderView.Size = new System.Drawing.Size(286, 206);
            this.RenderView.TabIndex = 0;
            this.RenderView.TabStop = false;
            this.RenderView.Resize += new System.EventHandler(this.RenderView_Resize);
            // 
            // RenderButton
            // 
            this.RenderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RenderButton.Location = new System.Drawing.Point(211, 238);
            this.RenderButton.Name = "RenderButton";
            this.RenderButton.Size = new System.Drawing.Size(75, 23);
            this.RenderButton.TabIndex = 1;
            this.RenderButton.Text = "Render";
            this.RenderButton.UseVisualStyleBackColor = true;
            this.RenderButton.Click += new System.EventHandler(this.RenderButton_Click);
            // 
            // RenderProgressBar
            // 
            this.RenderProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RenderProgressBar.Location = new System.Drawing.Point(0, 238);
            this.RenderProgressBar.Name = "RenderProgressBar";
            this.RenderProgressBar.Size = new System.Drawing.Size(205, 23);
            this.RenderProgressBar.TabIndex = 2;
            // 
            // ThreadsNumber
            // 
            this.ThreadsNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ThreadsNumber.Location = new System.Drawing.Point(112, 212);
            this.ThreadsNumber.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.ThreadsNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ThreadsNumber.Name = "ThreadsNumber";
            this.ThreadsNumber.Size = new System.Drawing.Size(42, 20);
            this.ThreadsNumber.TabIndex = 3;
            this.ThreadsNumber.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 214);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Number of threads";
            // 
            // ChessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ThreadsNumber);
            this.Controls.Add(this.RenderProgressBar);
            this.Controls.Add(this.RenderButton);
            this.Controls.Add(this.RenderView);
            this.Name = "ChessForm";
            this.Text = "Chess";
            ((System.ComponentModel.ISupportInitialize)(this.RenderView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThreadsNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox RenderView;
        private System.Windows.Forms.Button RenderButton;
        private System.Windows.Forms.ProgressBar RenderProgressBar;
        private System.Windows.Forms.NumericUpDown ThreadsNumber;
        private System.Windows.Forms.Label label1;
    }
}

