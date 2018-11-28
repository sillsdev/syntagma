namespace Syntagma
{
    partial class Form4
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.thresHld = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.minCollocs = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.perCent = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.classPC = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.wordPC = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.structPC = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.wdstPC = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.minforClass = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.minCollocs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minforClass)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(17, 352);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(98, 39);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(624, 352);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(91, 39);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(598, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Parameter 1: Number of standard deviations for significant collocations:";
            // 
            // thresHld
            // 
            this.thresHld.Location = new System.Drawing.Point(630, 20);
            this.thresHld.Name = "thresHld";
            this.thresHld.Size = new System.Drawing.Size(63, 28);
            this.thresHld.TabIndex = 3;
            this.thresHld.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(529, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "Parameter 2: Minimum number of collocations for significance:";
            // 
            // minCollocs
            // 
            this.minCollocs.Location = new System.Drawing.Point(630, 60);
            this.minCollocs.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.minCollocs.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.minCollocs.Name = "minCollocs";
            this.minCollocs.Size = new System.Drawing.Size(63, 28);
            this.minCollocs.TabIndex = 5;
            this.minCollocs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.minCollocs.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(555, 24);
            this.label3.TabIndex = 6;
            this.label3.Text = "Parameter 3: Number of significant links required for classification:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(607, 24);
            this.label4.TabIndex = 8;
            this.label4.Text = "Parameter 4: Percentage significant overlap required for joining classes:";
            // 
            // perCent
            // 
            this.perCent.Location = new System.Drawing.Point(630, 140);
            this.perCent.Name = "perCent";
            this.perCent.Size = new System.Drawing.Size(63, 28);
            this.perCent.TabIndex = 9;
            this.perCent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(690, 143);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 24);
            this.label5.TabIndex = 10;
            this.label5.Text = "%";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 180);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(582, 24);
            this.label6.TabIndex = 11;
            this.label6.Text = "Parameter 5: Percentage of times class must occur between classes:";
            // 
            // classPC
            // 
            this.classPC.Location = new System.Drawing.Point(630, 180);
            this.classPC.Name = "classPC";
            this.classPC.Size = new System.Drawing.Size(63, 28);
            this.classPC.TabIndex = 12;
            this.classPC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(690, 183);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 24);
            this.label7.TabIndex = 10;
            this.label7.Text = "%";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 220);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(582, 24);
            this.label8.TabIndex = 13;
            this.label8.Text = "Parameter 6: Percentage of times word must occur between classes:";
            // 
            // wordPC
            // 
            this.wordPC.Location = new System.Drawing.Point(630, 220);
            this.wordPC.Name = "wordPC";
            this.wordPC.Size = new System.Drawing.Size(63, 28);
            this.wordPC.TabIndex = 14;
            this.wordPC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(690, 183);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(25, 24);
            this.label9.TabIndex = 10;
            this.label9.Text = "%";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(690, 220);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(25, 24);
            this.label10.TabIndex = 10;
            this.label10.Text = "%";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 260);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(603, 24);
            this.label11.TabIndex = 15;
            this.label11.Text = "Parameter 7: Percentage of times item must occur in a certain structure:";
            // 
            // structPC
            // 
            this.structPC.Location = new System.Drawing.Point(630, 260);
            this.structPC.Name = "structPC";
            this.structPC.Size = new System.Drawing.Size(63, 28);
            this.structPC.TabIndex = 16;
            this.structPC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(690, 264);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(25, 24);
            this.label12.TabIndex = 10;
            this.label12.Text = "%";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 300);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(568, 24);
            this.label13.TabIndex = 17;
            this.label13.Text = "Parameter 8: Percentage of times an item must appear in a context:";
            // 
            // wdstPC
            // 
            this.wdstPC.Location = new System.Drawing.Point(624, 300);
            this.wdstPC.Name = "wdstPC";
            this.wdstPC.Size = new System.Drawing.Size(69, 28);
            this.wdstPC.TabIndex = 18;
            this.wdstPC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(690, 303);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(25, 24);
            this.label14.TabIndex = 10;
            this.label14.Text = "%";
            // 
            // minforClass
            // 
            this.minforClass.Location = new System.Drawing.Point(630, 101);
            this.minforClass.Name = "minforClass";
            this.minforClass.Size = new System.Drawing.Size(63, 28);
            this.minforClass.TabIndex = 19;
            this.minforClass.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.minforClass.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 410);
            this.ControlBox = false;
            this.Controls.Add(this.minforClass);
            this.Controls.Add(this.wdstPC);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.structPC);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.wordPC);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.classPC);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.perCent);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.minCollocs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.thresHld);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form4";
            this.Text = "Running parameters";
            this.Load += new System.EventHandler(this.Form4_Load);
            ((System.ComponentModel.ISupportInitialize)(this.minCollocs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minforClass)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox thresHld;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown minCollocs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox perCent;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox classPC;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox wordPC;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox structPC;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox wdstPC;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown minforClass;
    }
}