namespace Syntagma
{
    partial class Form2
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
            this.bnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Input = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.Output = new System.Windows.Forms.TextBox();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.btnOK = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SentEnd = new System.Windows.Forms.TextBox();
            this.CapitaliseBtn = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.WrdStart = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.WrdEnd = new System.Windows.Forms.TextBox();
            this.IPBrowse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bnCancel
            // 
            this.bnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bnCancel.Location = new System.Drawing.Point(666, 322);
            this.bnCancel.Name = "bnCancel";
            this.bnCancel.Size = new System.Drawing.Size(108, 33);
            this.bnCancel.TabIndex = 0;
            this.bnCancel.Text = "Cancel";
            this.bnCancel.UseVisualStyleBackColor = true;
            this.bnCancel.Click += new System.EventHandler(this.bnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Input text file (.txt)";
            // 
            // Input
            // 
            this.Input.Location = new System.Drawing.Point(173, 20);
            this.Input.Name = "Input";
            this.Input.Size = new System.Drawing.Size(456, 28);
            this.Input.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "Logging file (.txt)";
            // 
            // Output
            // 
            this.Output.Location = new System.Drawing.Point(173, 80);
            this.Output.Name = "Output";
            this.Output.Size = new System.Drawing.Size(456, 28);
            this.Output.TabIndex = 5;
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(16, 322);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(107, 33);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(331, 24);
            this.label3.TabIndex = 8;
            this.label3.Text = "Characters indicating end of sentence:";
            // 
            // SentEnd
            // 
            this.SentEnd.Location = new System.Drawing.Point(378, 140);
            this.SentEnd.Name = "SentEnd";
            this.SentEnd.Size = new System.Drawing.Size(146, 28);
            this.SentEnd.TabIndex = 9;
            // 
            // CapitaliseBtn
            // 
            this.CapitaliseBtn.AutoSize = true;
            this.CapitaliseBtn.Location = new System.Drawing.Point(585, 144);
            this.CapitaliseBtn.Name = "CapitaliseBtn";
            this.CapitaliseBtn.Size = new System.Drawing.Size(176, 28);
            this.CapitaliseBtn.TabIndex = 10;
            this.CapitaliseBtn.TabStop = true;
            this.CapitaliseBtn.Text = "Capitalise the text";
            this.CapitaliseBtn.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(355, 24);
            this.label4.TabIndex = 11;
            this.label4.Text = "Punctuation to be ignored at start of word:";
            // 
            // WrdStart
            // 
            this.WrdStart.Location = new System.Drawing.Point(380, 201);
            this.WrdStart.Name = "WrdStart";
            this.WrdStart.Size = new System.Drawing.Size(220, 28);
            this.WrdStart.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 260);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(355, 24);
            this.label5.TabIndex = 13;
            this.label5.Text = "Punctuation to be ignored at end of word:";
            // 
            // WrdEnd
            // 
            this.WrdEnd.Location = new System.Drawing.Point(380, 260);
            this.WrdEnd.Name = "WrdEnd";
            this.WrdEnd.Size = new System.Drawing.Size(220, 28);
            this.WrdEnd.TabIndex = 14;
            // 
            // IPBrowse
            // 
            this.IPBrowse.Location = new System.Drawing.Point(636, 20);
            this.IPBrowse.Name = "IPBrowse";
            this.IPBrowse.Size = new System.Drawing.Size(90, 30);
            this.IPBrowse.TabIndex = 3;
            this.IPBrowse.Text = "Browse";
            this.IPBrowse.UseVisualStyleBackColor = true;
            this.IPBrowse.Click += new System.EventHandler(this.IPBrowse_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bnCancel;
            this.ClientSize = new System.Drawing.Size(786, 375);
            this.ControlBox = false;
            this.Controls.Add(this.WrdEnd);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.WrdStart);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CapitaliseBtn);
            this.Controls.Add(this.SentEnd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.Output);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.IPBrowse);
            this.Controls.Add(this.Input);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bnCancel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.Text = "Set initial parameters";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Input;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Output;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox SentEnd;
        private System.Windows.Forms.RadioButton CapitaliseBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox WrdStart;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox WrdEnd;
        private System.Windows.Forms.Button IPBrowse;
    }
}