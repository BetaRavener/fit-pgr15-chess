using System.Windows.Forms;

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
            this.labelLightPosition = new System.Windows.Forms.Label();
            this.lightX = new System.Windows.Forms.TextBox();
            this.lightY = new System.Windows.Forms.TextBox();
            this.lightZ = new System.Windows.Forms.TextBox();
            this.cameraZ = new System.Windows.Forms.TextBox();
            this.cameraY = new System.Windows.Forms.TextBox();
            this.cameraX = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.FPSlabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ReflectionDepth = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.RenderView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThreadsNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReflectionDepth)).BeginInit();
            this.SuspendLayout();
            // 
            // RenderView
            // 
            this.RenderView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RenderView.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.RenderView.Location = new System.Drawing.Point(0, 0);
            this.RenderView.Margin = new System.Windows.Forms.Padding(6);
            this.RenderView.Name = "RenderView";
            this.RenderView.Size = new System.Drawing.Size(920, 613);
            this.RenderView.TabIndex = 0;
            this.RenderView.TabStop = false;
            this.RenderView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RenderView_MouseDown);
            this.RenderView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.RenderView_MouseMove);
            this.RenderView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RenderView_MouseUp);
            this.RenderView.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.RenderView_MouseWheel);
            this.RenderView.Resize += new System.EventHandler(this.RenderView_Resize);
            // 
            // RenderButton
            // 
            this.RenderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RenderButton.Location = new System.Drawing.Point(785, 751);
            this.RenderButton.Margin = new System.Windows.Forms.Padding(6);
            this.RenderButton.Name = "RenderButton";
            this.RenderButton.Size = new System.Drawing.Size(138, 42);
            this.RenderButton.TabIndex = 1;
            this.RenderButton.Text = "Render";
            this.RenderButton.UseVisualStyleBackColor = true;
            this.RenderButton.Click += new System.EventHandler(this.RenderButton_Click);
            // 
            // RenderProgressBar
            // 
            this.RenderProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RenderProgressBar.Location = new System.Drawing.Point(249, 751);
            this.RenderProgressBar.Margin = new System.Windows.Forms.Padding(6);
            this.RenderProgressBar.Name = "RenderProgressBar";
            this.RenderProgressBar.Size = new System.Drawing.Size(524, 42);
            this.RenderProgressBar.TabIndex = 2;
            // 
            // ThreadsNumber
            // 
            this.ThreadsNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ThreadsNumber.Location = new System.Drawing.Point(249, 626);
            this.ThreadsNumber.Margin = new System.Windows.Forms.Padding(6);
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
            this.ThreadsNumber.Size = new System.Drawing.Size(77, 29);
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
            this.label1.Location = new System.Drawing.Point(13, 628);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Number of threads";
            // 
            // labelLightPosition
            // 
            this.labelLightPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelLightPosition.AutoSize = true;
            this.labelLightPosition.Location = new System.Drawing.Point(13, 670);
            this.labelLightPosition.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelLightPosition.Name = "labelLightPosition";
            this.labelLightPosition.Size = new System.Drawing.Size(195, 25);
            this.labelLightPosition.TabIndex = 5;
            this.labelLightPosition.Text = "Light position (x, y, z)";
            // 
            // lightX
            // 
            this.lightX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lightX.Location = new System.Drawing.Point(249, 666);
            this.lightX.Margin = new System.Windows.Forms.Padding(4);
            this.lightX.Name = "lightX";
            this.lightX.Size = new System.Drawing.Size(46, 29);
            this.lightX.TabIndex = 6;
            this.lightX.Text = "0";
            this.lightX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lightX.TextChanged += new System.EventHandler(this.lightX_TextChanged);
            // 
            // lightY
            // 
            this.lightY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lightY.Location = new System.Drawing.Point(303, 666);
            this.lightY.Margin = new System.Windows.Forms.Padding(4);
            this.lightY.Name = "lightY";
            this.lightY.Size = new System.Drawing.Size(46, 29);
            this.lightY.TabIndex = 7;
            this.lightY.Text = "100";
            this.lightY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lightY.TextChanged += new System.EventHandler(this.lightY_TextChanged);
            // 
            // lightZ
            // 
            this.lightZ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lightZ.Location = new System.Drawing.Point(354, 666);
            this.lightZ.Margin = new System.Windows.Forms.Padding(4);
            this.lightZ.Name = "lightZ";
            this.lightZ.Size = new System.Drawing.Size(46, 29);
            this.lightZ.TabIndex = 8;
            this.lightZ.Text = "-10";
            this.lightZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lightZ.TextChanged += new System.EventHandler(this.lightZ_TextChanged);
            // 
            // cameraZ
            // 
            this.cameraZ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cameraZ.Location = new System.Drawing.Point(354, 709);
            this.cameraZ.Margin = new System.Windows.Forms.Padding(4);
            this.cameraZ.Name = "cameraZ";
            this.cameraZ.Size = new System.Drawing.Size(46, 29);
            this.cameraZ.TabIndex = 16;
            this.cameraZ.Text = "-150";
            this.cameraZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cameraZ.TextChanged += new System.EventHandler(this.cameraZ_TextChanged);
            // 
            // cameraY
            // 
            this.cameraY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cameraY.Location = new System.Drawing.Point(303, 709);
            this.cameraY.Margin = new System.Windows.Forms.Padding(4);
            this.cameraY.Name = "cameraY";
            this.cameraY.Size = new System.Drawing.Size(46, 29);
            this.cameraY.TabIndex = 15;
            this.cameraY.Text = "70";
            this.cameraY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cameraY.TextChanged += new System.EventHandler(this.cameraY_TextChanged);
            // 
            // cameraX
            // 
            this.cameraX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cameraX.Location = new System.Drawing.Point(249, 709);
            this.cameraX.Margin = new System.Windows.Forms.Padding(4);
            this.cameraX.Name = "cameraX";
            this.cameraX.Size = new System.Drawing.Size(46, 29);
            this.cameraX.TabIndex = 14;
            this.cameraX.Text = "0";
            this.cameraX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cameraX.TextChanged += new System.EventHandler(this.cameraX_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 711);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(223, 25);
            this.label2.TabIndex = 13;
            this.label2.Text = "Camera position (x, y, z)";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 761);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 25);
            this.label3.TabIndex = 17;
            this.label3.Text = "FPS";
            // 
            // FPSlabel
            // 
            this.FPSlabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FPSlabel.AutoSize = true;
            this.FPSlabel.Location = new System.Drawing.Point(83, 761);
            this.FPSlabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.FPSlabel.Name = "FPSlabel";
            this.FPSlabel.Size = new System.Drawing.Size(34, 25);
            this.FPSlabel.TabIndex = 18;
            this.FPSlabel.Text = "??";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(404, 628);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(168, 25);
            this.label4.TabIndex = 20;
            this.label4.Text = "Depth of reflection";
            // 
            // ReflectionDepth
            // 
            this.ReflectionDepth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ReflectionDepth.Location = new System.Drawing.Point(584, 625);
            this.ReflectionDepth.Margin = new System.Windows.Forms.Padding(6);
            this.ReflectionDepth.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.ReflectionDepth.Name = "ReflectionDepth";
            this.ReflectionDepth.Size = new System.Drawing.Size(77, 29);
            this.ReflectionDepth.TabIndex = 19;
            this.ReflectionDepth.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.ReflectionDepth.TextChanged += new System.EventHandler(this.reflectionDepth_TextChanged);
            // 
            // ChessForm
            // 
            this.AcceptButton = this.RenderButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 796);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ReflectionDepth);
            this.Controls.Add(this.FPSlabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cameraZ);
            this.Controls.Add(this.cameraY);
            this.Controls.Add(this.cameraX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lightZ);
            this.Controls.Add(this.lightY);
            this.Controls.Add(this.lightX);
            this.Controls.Add(this.labelLightPosition);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ThreadsNumber);
            this.Controls.Add(this.RenderProgressBar);
            this.Controls.Add(this.RenderButton);
            this.Controls.Add(this.RenderView);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "ChessForm";
            this.Text = "Chess";
            ((System.ComponentModel.ISupportInitialize)(this.RenderView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThreadsNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReflectionDepth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox RenderView;
        private System.Windows.Forms.Button RenderButton;
        private System.Windows.Forms.ProgressBar RenderProgressBar;
        private System.Windows.Forms.NumericUpDown ThreadsNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelLightPosition;
        private System.Windows.Forms.TextBox lightX;
        private System.Windows.Forms.TextBox lightY;
        private System.Windows.Forms.TextBox lightZ;
        private System.Windows.Forms.TextBox cameraZ;
        private System.Windows.Forms.TextBox cameraY;
        private System.Windows.Forms.TextBox cameraX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label FPSlabel;
        private Label label4;
        private NumericUpDown ReflectionDepth;
    }
}

