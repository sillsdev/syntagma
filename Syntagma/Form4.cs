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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            thresHld.Text = string.Format("{0,0:N1}", Form1.Param1);
            minCollocs.Text = Convert.ToString(Form1.Param2);
            minforClass.Text = Convert.ToString(Form1.Param3);
            perCent.Text = string.Format("{0,0:N1}", Form1.Param4);
            classPC.Text = string.Format("{0,0:N1}", Form1.Param5);
            wordPC.Text = string.Format("{0,0:N1}", Form1.Param6);
            structPC.Text = string.Format("{0,0:N1}", Form1.Param7);
            wdstPC.Text = string.Format("{0,0:N3}", Form1.Param8);
//            MessageBox.Show(string.Format("Form4 Load {0}", thresHld.Text));

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
//            MessageBox.Show(string.Format("Form4 btnOK {0}", thresHld.Text));
            Form1.Param1 = Convert.ToSingle(thresHld.Text);
            Form1.Param2 = Convert.ToInt32(minCollocs.Text);
            Form1.Param3 = Convert.ToInt32(minforClass.Text);
            Form1.Param4 = Convert.ToSingle(perCent.Text);
            Form1.Param5 = Convert.ToSingle(classPC.Text);
            Form1.Param6 = Convert.ToSingle(wordPC.Text);
            Form1.Param7 = Convert.ToSingle(structPC.Text);
            Form1.Param8 = Convert.ToSingle(wdstPC.Text);
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
