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
            this.reflectionDepth = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.loadSceneButton = new System.Windows.Forms.Button();
            this.exportImageButton = new System.Windows.Forms.Button();
            this.antialiasingFactor = new System.Windows.Forms.NumericUpDown();
            this.saveSceneButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.loadedSceneLabel = new System.Windows.Forms.Label();
            this.RenderButton = new System.Windows.Forms.Button();
            this.shininessFactor = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.RenderView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThreadsNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reflectionDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.antialiasingFactor)).BeginInit();
            this.SuspendLayout();
            // 
            // RenderView
            // 
            this.RenderView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RenderView.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.RenderView.Location = new System.Drawing.Point(0, 0);
            this.RenderView.Margin = new System.Windows.Forms.Padding(5);
            this.RenderView.Name = "RenderView";
            this.RenderView.Size = new System.Drawing.Size(579, 414);
            this.RenderView.TabIndex = 0;
            this.RenderView.TabStop = false;
            this.RenderView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RenderView_MouseDown);
            this.RenderView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.RenderView_MouseMove);
            this.RenderView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RenderView_MouseUp);
            this.RenderView.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.RenderView_MouseWheel);
            this.RenderView.Resize += new System.EventHandler(this.RenderView_Resize);
            // 
            // RenderProgressBar
            // 
            this.RenderProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RenderProgressBar.Location = new System.Drawing.Point(420, 541);
            this.RenderProgressBar.Margin = new System.Windows.Forms.Padding(5);
            this.RenderProgressBar.Name = "RenderProgressBar";
            this.RenderProgressBar.Size = new System.Drawing.Size(144, 35);
            this.RenderProgressBar.TabIndex = 2;
            this.RenderProgressBar.Visible = false;
            // 
            // ThreadsNumber
            // 
            this.ThreadsNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ThreadsNumber.Location = new System.Drawing.Point(194, 424);
            this.ThreadsNumber.Margin = new System.Windows.Forms.Padding(5);
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
            this.ThreadsNumber.Size = new System.Drawing.Size(63, 26);
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
            this.label1.Location = new System.Drawing.Point(11, 426);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Number of threads";
            // 
            // labelLightPosition
            // 
            this.labelLightPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelLightPosition.AutoSize = true;
            this.labelLightPosition.Location = new System.Drawing.Point(11, 461);
            this.labelLightPosition.Name = "labelLightPosition";
            this.labelLightPosition.Size = new System.Drawing.Size(155, 20);
            this.labelLightPosition.TabIndex = 5;
            this.labelLightPosition.Text = "Light position (x, y, z)";
            // 
            // lightX
            // 
            this.lightX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lightX.Location = new System.Drawing.Point(194, 457);
            this.lightX.Name = "lightX";
            this.lightX.Size = new System.Drawing.Size(38, 26);
            this.lightX.TabIndex = 6;
            this.lightX.Text = "0";
            this.lightX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lightX.TextChanged += new System.EventHandler(this.lightX_TextChanged);
            // 
            // lightY
            // 
            this.lightY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lightY.Location = new System.Drawing.Point(238, 457);
            this.lightY.Name = "lightY";
            this.lightY.Size = new System.Drawing.Size(38, 26);
            this.lightY.TabIndex = 7;
            this.lightY.Text = "100";
            this.lightY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lightY.TextChanged += new System.EventHandler(this.lightY_TextChanged);
            // 
            // lightZ
            // 
            this.lightZ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lightZ.Location = new System.Drawing.Point(280, 457);
            this.lightZ.Name = "lightZ";
            this.lightZ.Size = new System.Drawing.Size(38, 26);
            this.lightZ.TabIndex = 8;
            this.lightZ.Text = "-10";
            this.lightZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lightZ.TextChanged += new System.EventHandler(this.lightZ_TextChanged);
            // 
            // cameraZ
            // 
            this.cameraZ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cameraZ.Location = new System.Drawing.Point(280, 493);
            this.cameraZ.Name = "cameraZ";
            this.cameraZ.Size = new System.Drawing.Size(38, 26);
            this.cameraZ.TabIndex = 16;
            this.cameraZ.Text = "-150";
            this.cameraZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cameraZ.TextChanged += new System.EventHandler(this.cameraZ_TextChanged);
            // 
            // cameraY
            // 
            this.cameraY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cameraY.Location = new System.Drawing.Point(238, 493);
            this.cameraY.Name = "cameraY";
            this.cameraY.Size = new System.Drawing.Size(38, 26);
            this.cameraY.TabIndex = 15;
            this.cameraY.Text = "70";
            this.cameraY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cameraY.TextChanged += new System.EventHandler(this.cameraY_TextChanged);
            // 
            // cameraX
            // 
            this.cameraX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cameraX.Location = new System.Drawing.Point(194, 493);
            this.cameraX.Name = "cameraX";
            this.cameraX.Size = new System.Drawing.Size(38, 26);
            this.cameraX.TabIndex = 14;
            this.cameraX.Text = "0";
            this.cameraX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cameraX.TextChanged += new System.EventHandler(this.cameraX_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 494);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "Camera position (x, y, z)";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(333, 549);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 20);
            this.label3.TabIndex = 17;
            this.label3.Text = "FPS";
            // 
            // FPSlabel
            // 
            this.FPSlabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.FPSlabel.AutoSize = true;
            this.FPSlabel.Location = new System.Drawing.Point(383, 549);
            this.FPSlabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.FPSlabel.Name = "FPSlabel";
            this.FPSlabel.Size = new System.Drawing.Size(27, 20);
            this.FPSlabel.TabIndex = 18;
            this.FPSlabel.Text = "??";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 530);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 20);
            this.label4.TabIndex = 20;
            this.label4.Text = "Depth of reflection";
            // 
            // reflectionDepth
            // 
            this.reflectionDepth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.reflectionDepth.Location = new System.Drawing.Point(194, 527);
            this.reflectionDepth.Margin = new System.Windows.Forms.Padding(5);
            this.reflectionDepth.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.reflectionDepth.Name = "reflectionDepth";
            this.reflectionDepth.Size = new System.Drawing.Size(63, 26);
            this.reflectionDepth.TabIndex = 19;
            this.reflectionDepth.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.reflectionDepth.ValueChanged += new System.EventHandler(this.reflectionDepth_ValueChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 566);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 20);
            this.label5.TabIndex = 22;
            this.label5.Text = "Antialiasing";
            // 
            // loadSceneButton
            // 
            this.loadSceneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.loadSceneButton.Location = new System.Drawing.Point(335, 468);
            this.loadSceneButton.Margin = new System.Windows.Forms.Padding(2);
            this.loadSceneButton.Name = "loadSceneButton";
            this.loadSceneButton.Size = new System.Drawing.Size(114, 39);
            this.loadSceneButton.TabIndex = 23;
            this.loadSceneButton.Text = "Load scene";
            this.loadSceneButton.UseVisualStyleBackColor = true;
            this.loadSceneButton.Click += new System.EventHandler(this.openSceneButton_Click);
            // 
            // exportImageButton
            // 
            this.exportImageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exportImageButton.Location = new System.Drawing.Point(335, 425);
            this.exportImageButton.Margin = new System.Windows.Forms.Padding(2);
            this.exportImageButton.Name = "exportImageButton";
            this.exportImageButton.Size = new System.Drawing.Size(114, 39);
            this.exportImageButton.TabIndex = 24;
            this.exportImageButton.Text = "Export image";
            this.exportImageButton.UseVisualStyleBackColor = true;
            this.exportImageButton.Click += new System.EventHandler(this.exportImageButton_Click);
            // 
            // antialiasingFactor
            // 
            this.antialiasingFactor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.antialiasingFactor.Location = new System.Drawing.Point(194, 563);
            this.antialiasingFactor.Margin = new System.Windows.Forms.Padding(5);
            this.antialiasingFactor.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.antialiasingFactor.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.antialiasingFactor.Name = "antialiasingFactor";
            this.antialiasingFactor.Size = new System.Drawing.Size(63, 26);
            this.antialiasingFactor.TabIndex = 25;
            this.antialiasingFactor.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.antialiasingFactor.ValueChanged += new System.EventHandler(this.antialiasingFactor_ValueChanged);
            // 
            // saveSceneButton
            // 
            this.saveSceneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveSceneButton.Location = new System.Drawing.Point(453, 468);
            this.saveSceneButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveSceneButton.Name = "saveSceneButton";
            this.saveSceneButton.Size = new System.Drawing.Size(114, 39);
            this.saveSceneButton.TabIndex = 26;
            this.saveSceneButton.Text = "Save scene";
            this.saveSceneButton.UseVisualStyleBackColor = true;
            this.saveSceneButton.Click += new System.EventHandler(this.saveSceneButton_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(333, 516);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 20);
            this.label6.TabIndex = 27;
            this.label6.Text = "Loaded:";
            // 
            // loadedSceneLabel
            // 
            this.loadedSceneLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.loadedSceneLabel.AutoSize = true;
            this.loadedSceneLabel.Location = new System.Drawing.Point(410, 516);
            this.loadedSceneLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.loadedSceneLabel.Name = "loadedSceneLabel";
            this.loadedSceneLabel.Size = new System.Drawing.Size(126, 20);
            this.loadedSceneLabel.TabIndex = 28;
            this.loadedSceneLabel.Text = "<<base layout>>";
            // 
            // RenderButton
            // 
            this.RenderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RenderButton.BackColor = System.Drawing.Color.YellowGreen;
            this.RenderButton.Location = new System.Drawing.Point(335, 586);
            this.RenderButton.Margin = new System.Windows.Forms.Padding(5);
            this.RenderButton.Name = "RenderButton";
            this.RenderButton.Size = new System.Drawing.Size(229, 51);
            this.RenderButton.TabIndex = 1;
            this.RenderButton.Text = "Render";
            this.RenderButton.UseVisualStyleBackColor = false;
            this.RenderButton.Click += new System.EventHandler(this.RenderButton_Click);
            // 
            // shininessFactor
            // 
            this.shininessFactor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.shininessFactor.AutoSize = true;
            this.shininessFactor.Checked = true;
            this.shininessFactor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.shininessFactor.Location = new System.Drawing.Point(18, 600);
            this.shininessFactor.Name = "shininessFactor";
            this.shininessFactor.Size = new System.Drawing.Size(104, 24);
            this.shininessFactor.TabIndex = 30;
            this.shininessFactor.Text = "Shininess";
            this.shininessFactor.UseVisualStyleBackColor = true;
            this.shininessFactor.CheckedChanged += new System.EventHandler(this.shininessFactor_CheckedChanged);
            // 
            // ChessForm
            // 
            this.AcceptButton = this.RenderButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 644);
            this.Controls.Add(this.shininessFactor);
            this.Controls.Add(this.loadedSceneLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.saveSceneButton);
            this.Controls.Add(this.antialiasingFactor);
            this.Controls.Add(this.exportImageButton);
            this.Controls.Add(this.loadSceneButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.reflectionDepth);
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
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MinimumSize = new System.Drawing.Size(600, 300);
            this.Name = "ChessForm";
            this.Text = "Chess";
            ((System.ComponentModel.ISupportInitialize)(this.RenderView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThreadsNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reflectionDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.antialiasingFactor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox RenderView;
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
        private NumericUpDown reflectionDepth;
        private Label label5;
        private Button loadSceneButton;
        private Button exportImageButton;
        private NumericUpDown antialiasingFactor;
        private Button saveSceneButton;
        private Label label6;
        private Label loadedSceneLabel;
        private Button RenderButton;
        private CheckBox shininessFactor;
    }
}

