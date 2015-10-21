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
            ((System.ComponentModel.ISupportInitialize)(this.RenderView)).BeginInit();
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
            this.RenderView.Size = new System.Drawing.Size(286, 232);
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
            // ChessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.RenderProgressBar);
            this.Controls.Add(this.RenderButton);
            this.Controls.Add(this.RenderView);
            this.Name = "ChessForm";
            this.Text = "Chess";
            ((System.ComponentModel.ISupportInitialize)(this.RenderView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox RenderView;
        private System.Windows.Forms.Button RenderButton;
        private System.Windows.Forms.ProgressBar RenderProgressBar;
    }
}

