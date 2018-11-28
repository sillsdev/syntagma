using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Syntagma
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            Input.Text = Form1.inputFile;
            Output.Text = Form1.outputFile;
            SentEnd.Text = Form1.sentenceEnd;
            WrdEnd.Text = Form1.wordEnds;
            WrdStart.Text = Form1.wordStarts;
            CapitaliseBtn.Checked = Form1.wordCapitalise;
        }

        private void bnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void OPBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
            Output.Text = openFileDialog2.FileName;
        }

        private void IPBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            Input.Text = openFileDialog1.FileName;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Form1.inputFile = Input.Text;
            Form1.outputFile = Output.Text;
            Form1.wordCapitalise = CapitaliseBtn.Checked;
            Form1.sentenceEnd = SentEnd.Text;
            Form1.wordStarts = WrdStart.Text;
            Form1.wordEnds = WrdEnd.Text;
            this.DialogResult = DialogResult.OK;

        }
    }
}
