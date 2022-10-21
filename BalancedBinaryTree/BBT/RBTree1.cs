using QuickGraph;
using QuickGraph.Graphviz;
using QuickGraph.Graphviz.Dot;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedBinaryTree.BST
{
    class RBTree1
    {
        /// <summary>
        /// Object of type Node contains 4 properties
        /// Colour
        /// Left
        /// Right
        /// Parent
        /// Data
        /// </summary>
        public class Node
        {
            public Color color;
            public Node left;
            public Node right;
            public Node parent;
            public int data;

            public Node(int data) { this.data = data; }
            public Node(Color colour) { this.color = colour; }
            public Node(int data, Color colour) { this.data = data; this.color = colour; }
        }
        /// <summary>
        /// Root node of the tree (both reference & pointer)
        /// </summary>
        private Node root;
        /// <summary>
        /// New instance of a Red-Black tree object
        /// </summary>
        public RBTree1() { }
        /// <summary>
        /// Left Rotate
        /// </summary>
        /// <param name="X"></param>
        /// <returns>void</returns>
        private void LeftRotate(Node X)
        {
            Node Y = X.right; // set Y
            X.right = Y.left;//turn Y's left subtree into X's right subtree
            if (Y.left != null)
            {
                Y.left.parent = X;
            }
            if (Y != null)
            {
                Y.parent = X.parent;//link X's parent to Y
            }
            if (X.parent == null)
            {
                root = Y;
            }
            if (X == X.parent.left)
            {
                X.parent.left = Y;
            }
            else
            {
                X.parent.right = Y;
            }
            Y.left = X; //put X on Y's left
            if (X != null)
            {
                X.parent = Y;
            }

        }
        /// <summary>
        /// Rotate Right
        /// </summary>
        /// <param name="Y"></param>
        /// <returns>void</returns>
        private void RightRotate(Node Y)
        {
            // right rotate is simply mirror code from left rotate
            Node X = Y.left;
            Y.left = X.right;
            if (X.right != null)
            {
                X.right.parent = Y;
            }
            if (X != null)
            {
                X.parent = Y.parent;
            }
            if (Y.parent == null)
            {
                root = X;
            }
            if (Y == Y.parent.right)
            {
                Y.parent.right = X;
            }
            if (Y == Y.parent.left)
            {
                Y.parent.left = X;
            }

            X.right = Y;//put Y on X's right
            if (Y != null)
            {
                Y.parent = X;
            }
        }
        /// <summary>
        /// Display Tree
        /// </summary>
        public string DisplayTree()
        {
            if (root == null)
            {
                return "";
            }
            else
            {
                return InOrderDisplay(root);
            }
        }
        /// <summary>
        /// Find item in the tree
        /// </summary>
        /// <param name="key"></param>
        public Node Find(int key)
        {
            Node temp = root;
            while (true)
            {
                if (temp == null)
                {
                    break;
                }
                if (key < temp.data)
                {
                    temp = temp.left;
                }
                if (key > temp.data)
                {
                    temp = temp.right;
                }
                if (key == temp.data)
                {
                }
            }
            return temp;
        }
        /// <summary>
        /// Insert a new object into the RB Tree
        /// </summary>
        /// <param name="item"></param>
        public void Insert(int item)
        {
            Node newItem = new Node(item);
            if (root == null)
            {
                root = newItem;
                root.color = Color.Black;
                return;
            }
            Node Y = null;
            Node X = root;
            while (X != null)
            {
                Y = X;
                if (newItem.data < X.data)
                {
                    X = X.left;
                }
                else
                {
                    X = X.right;
                }
            }
            newItem.parent = Y;
            if (Y == null)
            {
                root = newItem;
            }
            else if (newItem.data < Y.data)
            {
                Y.left = newItem;
            }
            else
            {
                Y.right = newItem;
            }
            newItem.left = null;
            newItem.right = null;
            newItem.color = Color.Red;//colour the new node red
            InsertFixUp(newItem);//call method to check for violations and fix
        }
        private string InOrderDisplay(Node current)
        {
            if (current != null)
            {
                return string.Format("{0} ({1}) {2}", InOrderDisplay(current.left), current.data, InOrderDisplay(current.right));
            }
            else return "";
        }
        private void InsertFixUp(Node item)
        {
            //Checks Red-Black Tree properties
            while (item != root && item.parent.color == Color.Red)
            {
                /*We have a violation*/
                if (item.parent == item.parent.parent.left)
                {
                    Node Y = item.parent.parent.right;
                    if (Y != null && Y.color == Color.Red)//Case 1: uncle is red
                    {
                        item.parent.color = Color.Black;
                        Y.color = Color.Black;
                        item.parent.parent.color = Color.Red;
                        item = item.parent.parent;
                    }
                    else //Case 2: uncle is black
                    {
                        if (item == item.parent.right)
                        {
                            item = item.parent;
                            LeftRotate(item);
                        }
                        //Case 3: recolour & rotate
                        item.parent.color = Color.Black;
                        item.parent.parent.color = Color.Red;
                        RightRotate(item.parent.parent);
                    }

                }
                else
                {
                    //mirror image of code above
                    Node X;

                    X = item.parent.parent.left;
                    if (X != null && X.color == Color.Black)//Case 1
                    {
                        item.parent.color = Color.Red;
                        X.color = Color.Red;
                        item.parent.parent.color = Color.Black;
                        item = item.parent.parent;
                    }
                    else //Case 2
                    {
                        if (item == item.parent.left)
                        {
                            item = item.parent;
                            RightRotate(item);
                        }
                        //Case 3: recolour & rotate
                        item.parent.color = Color.Black;
                        item.parent.parent.color = Color.Red;
                        LeftRotate(item.parent.parent);

                    }

                }
                root.color = Color.Black;//re-colour the root black as necessary
            }
        }
        /// <summary>
        /// Deletes a specified value from the tree
        /// </summary>
        /// <param name="item"></param>
        public void Delete(int key)
        {
            //first find the node in the tree to delete and assign to item pointer/reference
            Node item = Find(key);
            Node X;
            Node Y;

            if (item == null)
            {
                return;
            }
            if (item.left == null || item.right == null)
            {
                Y = item;
            }
            else
            {
                Y = TreeSuccessor(item);
            }
            if (Y.left != null)
            {
                X = Y.left;
            }
            else
            {
                X = Y.right;
            }
            if (X != null)
            {
                X.parent = Y;
            }
            if (Y.parent == null)
            {
                root = X;
            }
            else if (Y == Y.parent.left)
            {
                Y.parent.left = X;
            }
            else
            {
                Y.parent.left = X;
            }
            if (Y != item)
            {
                item.data = Y.data;
            }
            if (Y.color == Color.Black)
            {
                DeleteFixUp(X);
            }

        }
        /// <summary>
        /// Checks the tree for any violations after deletion and performs a fix
        /// </summary>
        /// <param name="X"></param>
        private void DeleteFixUp(Node X)
        {

            while (X != null && X != root && X.color == Color.Black)
            {
                if (X == X.parent.left)
                {
                    Node W = X.parent.right;
                    if (W.color == Color.Red)
                    {
                        W.color = Color.Black; //case 1
                        X.parent.color = Color.Red; //case 1
                        LeftRotate(X.parent); //case 1
                        W = X.parent.right; //case 1
                    }
                    if (W.left.color == Color.Black && W.right.color == Color.Black)
                    {
                        W.color = Color.Red; //case 2
                        X = X.parent; //case 2
                    }
                    else if (W.right.color == Color.Black)
                    {
                        W.left.color = Color.Black; //case 3
                        W.color = Color.Red; //case 3
                        RightRotate(W); //case 3
                        W = X.parent.right; //case 3
                    }
                    W.color = X.parent.color; //case 4
                    X.parent.color = Color.Black; //case 4
                    W.right.color = Color.Black; //case 4
                    LeftRotate(X.parent); //case 4
                    X = root; //case 4
                }
                else //mirror code from above with "right" & "left" exchanged
                {
                    Node W = X.parent.left;
                    if (W.color == Color.Red)
                    {
                        W.color = Color.Black;
                        X.parent.color = Color.Red;
                        RightRotate(X.parent);
                        W = X.parent.left;
                    }
                    if (W.right.color == Color.Black && W.left.color == Color.Black)
                    {
                        W.color = Color.Black;
                        X = X.parent;
                    }
                    else if (W.left.color == Color.Black)
                    {
                        W.right.color = Color.Black;
                        W.color = Color.Red;
                        LeftRotate(W);
                        W = X.parent.left;
                    }
                    W.color = X.parent.color;
                    X.parent.color = Color.Black;
                    W.left.color = Color.Black;
                    RightRotate(X.parent);
                    X = root;
                }
            }
            if (X != null)
                X.color = Color.Black;
        }
        private Node Minimum(Node X)
        {
            while (X.left.left != null)
            {
                X = X.left;
            }
            if (X.left.right != null)
            {
                X = X.left.right;
            }
            return X;
        }
        private Node TreeSuccessor(Node X)
        {
            if (X.left != null)
            {
                return Minimum(X);
            }
            else
            {
                Node Y = X.parent;
                while (Y != null && X == Y.right)
                {
                    X = Y;
                    Y = Y.parent;
                }
                return Y;
            }
        }
        public Image Visualize()
        {
            GraphvizColor transparent = new GraphvizColor(Color.Transparent);
            UndirectedGraph<int, UndirectedEdge<int>> graph = new UndirectedGraph<int, UndirectedEdge<int>>();
            if (root is null)
                return null;
            var queue = new Queue<Node>();
            queue.Enqueue(root);
            Node nextLayer = root;
            int m = 0;
            while (queue.Count > 0)
            {

                Node node = queue.Dequeue();
                if (node.left is null)
                {
                    graph.AddVerticesAndEdge(new UndirectedEdge<int>(node.data, --m));
                }
                else
                {
                    graph.AddVerticesAndEdge(new UndirectedEdge<int>(node.data, node.left.data));
                    queue.Enqueue(node.left);
                }
                if (node.right is null)
                {
                    graph.AddVerticesAndEdge(new UndirectedEdge<int>(node.data, --m));
                }
                else
                {
                    graph.AddVerticesAndEdge(new UndirectedEdge<int>(node.data, node.right.data));
                    queue.Enqueue(node.right);
                }

                if ((nextLayer == node) && (queue.Count != 0))
                {
                    nextLayer = queue.ToArray()[queue.Count - 1];
                }
            }
            void FormatVertex(object sender1, FormatVertexEventArgs<int> e)
            {
                e.VertexFormatter.Label = e.Vertex.ToString();
                e.VertexFormatter.Shape = GraphvizVertexShape.Circle;
                e.VertexFormatter.BottomLabel = e.Vertex.ToString();
                e.VertexFormatter.Font = new GraphvizFont("Times-Roman", 16);
                if (e.Vertex < 0)
                {
                    e.VertexFormatter.FillColor = e.VertexFormatter.StrokeColor = e.VertexFormatter.FontColor = transparent;
                }
                else
                {
                    e.VertexFormatter.FillColor = e.VertexFormatter.StrokeColor = e.VertexFormatter.FontColor = GraphvizColor.Black;
                }
            }
            void FormatEdge(object sender1, FormatEdgeEventArgs<int, UndirectedEdge<int>> e)
            {
                e.EdgeFormatter.Font = new GraphvizFont("Times-Roman", 16);
                if (e.Edge.Target < 0)
                {
                    e.EdgeFormatter.FontGraphvizColor = transparent;
                    e.EdgeFormatter.StrokeGraphvizColors.Add(transparent);
                }
                else
                {
                    e.EdgeFormatter.FontGraphvizColor = GraphvizColor.Black;
                    e.EdgeFormatter.StrokeGraphvizColors.Add(GraphvizColor.Black);
                }
            }
            GraphvizGraph graphFormat = new GraphvizGraph
            {
                BackgroundGraphvizColor = new GraphvizColor(Color.Transparent),
                DPI = 500
            };
            var graphViz = new GraphvizAlgorithm<int, UndirectedEdge<int>>(graph, @".", GraphvizImageType.Gif, graphFormat);
            graphViz.FormatVertex += FormatVertex;
            graphViz.FormatEdge += FormatEdge;
            string dot = graphViz.Generate();
            return GraphVizEngine.RenderImage(graphViz.Generate(), "gif");
        }
    }
}
