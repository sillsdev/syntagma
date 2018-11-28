using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Syntagma
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            if (Form1.treeLoaded) return;
            if (System.IO.File.Exists("Documentation.txt") != true)
            {
                MessageBox.Show("File Documentation.txt cannot be found.");
                return;
            }
            StreamReader infoFile = new StreamReader("Documentation.txt");
            string inLine, outLine;
            char nodeShift;
            int nodePresent, depth;
            int maxLength = 100;
            int breakPoint, newLine;
            int numParents = 0;             // Number of nodes at top level
            List<TreeNode> nextTn = new List<TreeNode>();
            TreeNode tnNode = new TreeNode();
            List<int> trBranch = new List<int>();
            tnNode.Name = "treeView1";
            inLine = infoFile.ReadLine();   // Read and insert first line ('General')
            treeView1.Nodes.Add(inLine);
            //            MessageBox.Show(inLine);
            nextTn.Add(treeView1.Nodes[0]);
            trBranch.Add(0);
            inLine = infoFile.ReadLine();
            inLine = inLine.Substring(1);   // Remove '/' from second line ('History')
            nextTn[0].Nodes.Add(inLine);
            nextTn.Add(nextTn[0].Nodes[0]);
            trBranch.Add(0);
            //            MessageBox.Show(string.Format("trBranch.Count {0}",trBranch.Count));
            nodePresent = 0;

            while ((inLine = infoFile.ReadLine()) != null)
            {
                while (inLine[inLine.Length - 1] == '-')
                    inLine = inLine.Substring(0, inLine.Length - 1) + " " + infoFile.ReadLine();
                nodeShift = inLine[0];
                inLine = inLine.Substring(1);
                if (nodeShift == '/')   // Go down a level
                {
                    ++nodePresent;
                    if (nextTn.Count < nodePresent + 1)
                    {
                        depth = trBranch.Count;
                        nextTn.Add(nextTn[nodePresent - 1].Nodes[0]);
                        trBranch.Add(0);
                    }
                    else
                    {
                        if (nodePresent > 0)
                            nextTn[nodePresent] = nextTn[nodePresent - 1].Nodes[trBranch[nodePresent]];
                        else
                        {
                            nextTn.Clear();
                            trBranch.Clear();
                            numParents++;
                            nextTn.Add(treeView1.Nodes[numParents]);
                            trBranch.Add(numParents);
                            nodePresent = 0;
                        }
                    }
                }
                else // Go up a level
                {
                    if (nodePresent > 0)
                    {
                        ++trBranch[nodePresent];
                        if (trBranch.Count > nodePresent + 1) trBranch[nodePresent + 1] = 0;
                    }
                    --nodePresent;
                }
                if (nodePresent < 0) treeView1.Nodes.Add(inLine);   // New parent node
                else
                {
                    if (inLine.Length > 0)
                    {
                        while ((inLine.Length > maxLength) || (inLine.IndexOf("##") > 0))
                        {
                            newLine = inLine.IndexOf("##");
                            if ((newLine > 0) && (newLine < maxLength))
                            {
                                outLine = inLine.Substring(0, newLine);
                                inLine = inLine.Substring(newLine + 2, inLine.Length - newLine - 2);
                            }
                            else
                            {
                                breakPoint = inLine.IndexOf(" ", maxLength - 15);
                                if (breakPoint > 0)
                                {
                                    outLine = inLine.Substring(0, breakPoint);
                                    inLine = inLine.Substring(breakPoint + 1,
                                        inLine.Length - breakPoint - 1);
                                }
                                else
                                {
                                    outLine = inLine;
                                    inLine = "";
                                }
                            }
                            nextTn[nodePresent].Nodes.Add(outLine);
                            //                            MessageBox.Show(inLine);
                        }
                        nextTn[nodePresent].Nodes.Add(inLine);
                    }
                }
            }
            infoFile.Close();
            Form1.treeLoaded = true;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
