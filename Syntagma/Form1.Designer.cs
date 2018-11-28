namespace Syntagma
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
            this.Setinitial = new System.Windows.Forms.Button();
            this.Helpbutton = new System.Windows.Forms.Button();
            this.Params = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LogDisplay = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.LogPrint = new System.Windows.Forms.NumericUpDown();
            this.Phase1 = new System.Windows.Forms.Button();
            this.Phase2 = new System.Windows.Forms.Button();
            this.readParam = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.LogDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LogPrint)).BeginInit();
            this.SuspendLayout();
            // 
            // Setinitial
            // 
            this.Setinitial.Location = new System.Drawing.Point(20, 30);
            this.Setinitial.Name = "Setinitial";
            this.Setinitial.Size = new System.Drawing.Size(304, 33);
            this.Setinitial.TabIndex = 0;
            this.Setinitial.Text = "Set initial parameters";
            this.Setinitial.UseVisualStyleBackColor = true;
            this.Setinitial.Click += new System.EventHandler(this.Setinitial_Click);
            // 
            // Helpbutton
            // 
            this.Helpbutton.Location = new System.Drawing.Point(375, 30);
            this.Helpbutton.Name = "Helpbutton";
            this.Helpbutton.Size = new System.Drawing.Size(326, 33);
            this.Helpbutton.TabIndex = 1;
            this.Helpbutton.Text = "Documentation and help";
            this.Helpbutton.UseVisualStyleBackColor = true;
            this.Helpbutton.Click += new System.EventHandler(this.Helpbutton_Click);
            // 
            // Params
            // 
            this.Params.Location = new System.Drawing.Point(20, 90);
            this.Params.Name = "Params";
            this.Params.Size = new System.Drawing.Size(302, 33);
            this.Params.TabIndex = 2;
            this.Params.Text = "Change running parameters";
            this.Params.UseVisualStyleBackColor = true;
            this.Params.Click += new System.EventHandler(this.Params_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Level of logging:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(194, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Displayed";
            // 
            // LogDisplay
            // 
            this.LogDisplay.Location = new System.Drawing.Point(298, 160);
            this.LogDisplay.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.LogDisplay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.LogDisplay.Name = "LogDisplay";
            this.LogDisplay.Size = new System.Drawing.Size(55, 30);
            this.LogDisplay.TabIndex = 5;
            this.LogDisplay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.LogDisplay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(430, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Filed";
            // 
            // LogPrint
            // 
            this.LogPrint.Location = new System.Drawing.Point(490, 160);
            this.LogPrint.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.LogPrint.Name = "LogPrint";
            this.LogPrint.Size = new System.Drawing.Size(51, 30);
            this.LogPrint.TabIndex = 7;
            this.LogPrint.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.LogPrint.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Phase1
            // 
            this.Phase1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Phase1.Location = new System.Drawing.Point(25, 227);
            this.Phase1.Name = "Phase1";
            this.Phase1.Size = new System.Drawing.Size(459, 40);
            this.Phase1.TabIndex = 8;
            this.Phase1.Text = "Run Phase 1: Read text and form initial classes";
            this.Phase1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Phase1.UseVisualStyleBackColor = false;
            this.Phase1.Click += new System.EventHandler(this.Phase1_Click);
            // 
            // Phase2
            // 
            this.Phase2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Phase2.Location = new System.Drawing.Point(25, 311);
            this.Phase2.Name = "Phase2";
            this.Phase2.Size = new System.Drawing.Size(459, 40);
            this.Phase2.TabIndex = 9;
            this.Phase2.Text = "Run Phase 2: (may be repeated); run no. 1";
            this.Phase2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Phase2.UseVisualStyleBackColor = false;
            this.Phase2.Click += new System.EventHandler(this.Phase2_Click);
            // 
            // readParam
            // 
            this.readParam.Location = new System.Drawing.Point(375, 90);
            this.readParam.Name = "readParam";
            this.readParam.Size = new System.Drawing.Size(326, 32);
            this.readParam.TabIndex = 10;
            this.readParam.Text = "Read running parameters";
            this.readParam.UseVisualStyleBackColor = true;
            this.readParam.Click += new System.EventHandler(this.readParam_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 388);
            this.Controls.Add(this.readParam);
            this.Controls.Add(this.Phase2);
            this.Controls.Add(this.Phase1);
            this.Controls.Add(this.LogPrint);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LogDisplay);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Params);
            this.Controls.Add(this.Helpbutton);
            this.Controls.Add(this.Setinitial);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Syntagma: Syntax discovery program version 1.2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.LogDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LogPrint)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Setinitial;
        private System.Windows.Forms.Button Helpbutton;
        private System.Windows.Forms.Button Params;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown LogDisplay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown LogPrint;
        private System.Windows.Forms.Button Phase1;
        private System.Windows.Forms.Button Phase2;
        private System.Windows.Forms.Button readParam;
    }
}

