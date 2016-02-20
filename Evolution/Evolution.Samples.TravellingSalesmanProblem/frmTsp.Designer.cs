namespace Evolution.Samples.TravelingSalesmanProblem
{
    partial class FrmTsp
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
            this.pctMap = new System.Windows.Forms.PictureBox();
            this.btnGenerateMap = new System.Windows.Forms.Button();
            this.chkCircular = new System.Windows.Forms.CheckBox();
            this.nbrNumberOfCities = new System.Windows.Forms.NumericUpDown();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnStepForward = new System.Windows.Forms.Button();
            this.lblGeneration = new System.Windows.Forms.Label();
            this.lblBestGeneration = new System.Windows.Forms.Label();
            this.lblBestFitness = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pctMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbrNumberOfCities)).BeginInit();
            this.SuspendLayout();
            // 
            // pctMap
            // 
            this.pctMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pctMap.Location = new System.Drawing.Point(12, 12);
            this.pctMap.Name = "pctMap";
            this.pctMap.Size = new System.Drawing.Size(260, 237);
            this.pctMap.TabIndex = 0;
            this.pctMap.TabStop = false;
            // 
            // btnGenerateMap
            // 
            this.btnGenerateMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerateMap.Location = new System.Drawing.Point(278, 35);
            this.btnGenerateMap.Name = "btnGenerateMap";
            this.btnGenerateMap.Size = new System.Drawing.Size(117, 30);
            this.btnGenerateMap.TabIndex = 1;
            this.btnGenerateMap.Text = "Generate Map";
            this.btnGenerateMap.UseVisualStyleBackColor = true;
            this.btnGenerateMap.Click += new System.EventHandler(this.btnGenerateMap_Click);
            // 
            // chkCircular
            // 
            this.chkCircular.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCircular.AutoSize = true;
            this.chkCircular.Location = new System.Drawing.Point(334, 12);
            this.chkCircular.Name = "chkCircular";
            this.chkCircular.Size = new System.Drawing.Size(61, 17);
            this.chkCircular.TabIndex = 2;
            this.chkCircular.Text = "Circular";
            this.chkCircular.UseVisualStyleBackColor = true;
            // 
            // nbrNumberOfCities
            // 
            this.nbrNumberOfCities.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nbrNumberOfCities.Location = new System.Drawing.Point(278, 11);
            this.nbrNumberOfCities.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.nbrNumberOfCities.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nbrNumberOfCities.Name = "nbrNumberOfCities";
            this.nbrNumberOfCities.Size = new System.Drawing.Size(42, 20);
            this.nbrNumberOfCities.TabIndex = 3;
            this.nbrNumberOfCities.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.Location = new System.Drawing.Point(278, 219);
            this.btnRun.Name = "btnRun";
            this.btnRun.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnRun.Size = new System.Drawing.Size(117, 30);
            this.btnRun.TabIndex = 4;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnStepForward
            // 
            this.btnStepForward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStepForward.Location = new System.Drawing.Point(278, 183);
            this.btnStepForward.Name = "btnStepForward";
            this.btnStepForward.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnStepForward.Size = new System.Drawing.Size(117, 30);
            this.btnStepForward.TabIndex = 5;
            this.btnStepForward.Text = "Step Forward";
            this.btnStepForward.UseVisualStyleBackColor = true;
            this.btnStepForward.Click += new System.EventHandler(this.btnStepForward_Click);
            // 
            // lblGeneration
            // 
            this.lblGeneration.Location = new System.Drawing.Point(278, 162);
            this.lblGeneration.Name = "lblGeneration";
            this.lblGeneration.Size = new System.Drawing.Size(117, 18);
            this.lblGeneration.TabIndex = 6;
            this.lblGeneration.Text = "0";
            this.lblGeneration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblGeneration.Click += new System.EventHandler(this.lblGeneration_Click);
            // 
            // lblBestGeneration
            // 
            this.lblBestGeneration.Location = new System.Drawing.Point(278, 86);
            this.lblBestGeneration.Name = "lblBestGeneration";
            this.lblBestGeneration.Size = new System.Drawing.Size(117, 18);
            this.lblBestGeneration.TabIndex = 8;
            this.lblBestGeneration.Text = "Best Generation:";
            this.lblBestGeneration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblBestGeneration.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblBestFitness
            // 
            this.lblBestFitness.Location = new System.Drawing.Point(278, 68);
            this.lblBestFitness.Name = "lblBestFitness";
            this.lblBestFitness.Size = new System.Drawing.Size(117, 18);
            this.lblBestFitness.TabIndex = 9;
            this.lblBestFitness.Text = "Best Fitness:";
            this.lblBestFitness.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmTsp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 261);
            this.Controls.Add(this.lblBestFitness);
            this.Controls.Add(this.lblBestGeneration);
            this.Controls.Add(this.lblGeneration);
            this.Controls.Add(this.btnStepForward);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.nbrNumberOfCities);
            this.Controls.Add(this.chkCircular);
            this.Controls.Add(this.btnGenerateMap);
            this.Controls.Add(this.pctMap);
            this.Name = "FrmTsp";
            this.Text = "TSP";
            this.Load += new System.EventHandler(this.FrmTsp_Load);
            this.Resize += new System.EventHandler(this.FrmTsp_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pctMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbrNumberOfCities)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pctMap;
        private System.Windows.Forms.Button btnGenerateMap;
        private System.Windows.Forms.CheckBox chkCircular;
        private System.Windows.Forms.NumericUpDown nbrNumberOfCities;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnStepForward;
        private System.Windows.Forms.Label lblGeneration;
        private System.Windows.Forms.Label lblBestGeneration;
        private System.Windows.Forms.Label lblBestFitness;
    }
}

