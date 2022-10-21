using QuickGraph;
using QuickGraph.Graphviz;
using QuickGraph.Graphviz.Dot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BalancedBinaryTree
{
    public partial class MainForm : Form
    {
        readonly BST.RBTree tree = new BST.RBTree();
        public MainForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            tree.Add((int)numericUpDown1.Value);
            pictureBox.Image = tree.Visualize();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //richTextBox1.Text = tree.LevelOrderTraversal();
            pictureBox.Image = tree.Visualize();
        }

        private void Button_remove_Click(object sender, EventArgs e)
        {
            tree.Remove((int)numericUpDown1.Value);
            pictureBox.Image = tree.Visualize();
        }
    }
}
