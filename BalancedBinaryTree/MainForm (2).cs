using QuickGraph;
using QuickGraph.Graphviz;
using QuickGraph.Graphviz.Dot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BalancedBinaryTree
{
    public partial class MainForm : Form
    {
        readonly BST.RBTree RBtree = new BST.RBTree();
        readonly BST.AVLTree AVLtree = new BST.AVLTree();
        public MainForm()
        {
            InitializeComponent();
        }

        private void Button_add_Click(object sender, EventArgs e)
        {
            RBtree.Add((int)numericUpDown.Value);
            AVLtree.Add((int)numericUpDown.Value);
        }
        private void Button_remove_Click(object sender, EventArgs e)
        {
            RBtree.Remove((int)numericUpDown.Value);
            AVLtree.Remove((int)numericUpDown.Value);
        }
        private void Button_showAVL_Click(object sender, EventArgs e)
        {
            pictureBox.Image = AVLtree.Visualize();
        }
        private void Button_showRB_Click(object sender, EventArgs e)
        {
            pictureBox.Image = RBtree.Visualize();
        }
        private void Button_saveAVL_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog
            {
                DefaultExt = "gif",
                FileName = "AVLTreeImage.gif",
                InitialDirectory = Directory.GetCurrentDirectory(),
                Filter = "gif images (*.gif) | *.gif"
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        File.Delete(sfd.FileName);
                    }
                    using (Image img = AVLtree.Visualize())
                    {
                        img.Save(sfd.FileName);
                    }

                }

            }
        }
        private void Button_saveRB_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog
            {
                DefaultExt = "gif",
                FileName = "RBTreeImage.gif",
                InitialDirectory = Directory.GetCurrentDirectory(),
                Filter = "gif images (*.gif) | *.gif"
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        File.Delete(sfd.FileName);
                    }
                    using (Image img = RBtree.Visualize())
                    {
                        img.Save(sfd.FileName);
                    }

                }

            }
        }
        private void Button_addRange_Click(object sender, EventArgs e)
        {
            for (int i = (int)numericUpDown_min.Value; i < (int)numericUpDown_max.Value; i++)
            {
                RBtree.Add(i);
                AVLtree.Add(i);
            }
        }

        private void Button_clear_Click(object sender, EventArgs e)
        {
            RBtree.Clear();
            AVLtree.Clear();
        }
    }
}
