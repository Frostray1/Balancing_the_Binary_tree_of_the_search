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
    public class RedBlackTree
    {
        private readonly RedBlackTreeNode _leaf = RedBlackTreeNode.CreateLeaf();

        public RedBlackTree()
        {
            Root = _leaf;
        }
        public bool Find(int value)
        {
            try
            {
                RedBlackTreeNode node = Root;
                do
                {
                    if (node.Data == value)
                        return true;
                    node = value < node.Data ? node.Left : node.Right;
                } while (true);
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }
        internal RedBlackTreeNode Root { get; private set; }
        public void Add(int value)
        {
            RedBlackTreeNode newNode = RedBlackTreeNode.CreateNode(value, RedBlackTreeNode.ColorEnum.Red);
            Insert(newNode);
        }
        private void Insert(RedBlackTreeNode z)
        {
            var y = _leaf;
            var x = Root;
            while (x != _leaf)
            {
                y = x;
                x = z.Data < x.Data ? x.Left : x.Right;
            }

            z.Parent = y;
            if (y == _leaf)
            {
                Root = z;
            }
            else if (z.Data < y.Data)
            {
                y.Left = z;
            }
            else
            {
                y.Right = z;
            }

            z.Left = _leaf;
            z.Right = _leaf;
            z.Color = RedBlackTreeNode.ColorEnum.Red;
            InsertFixup(z);
        }
        private void InsertFixup(RedBlackTreeNode z)
        {
            while (z.Parent.Color == RedBlackTreeNode.ColorEnum.Red)
            {
                if (z.Parent == z.Parent.Parent.Left)
                {
                    var y = z.Parent.Parent.Right;
                    if (y.Color == RedBlackTreeNode.ColorEnum.Red)
                    {
                        z.Parent.Color = RedBlackTreeNode.ColorEnum.Black;
                        y.Color = RedBlackTreeNode.ColorEnum.Black;
                        z.Parent.Parent.Color = RedBlackTreeNode.ColorEnum.Red;
                        z = z.Parent.Parent;
                    }
                    else
                    {
                        if (z == z.Parent.Right)
                        {
                            z = z.Parent;
                            RotateLeft(z);
                        }

                        z.Parent.Color = RedBlackTreeNode.ColorEnum.Black;
                        z.Parent.Parent.Color = RedBlackTreeNode.ColorEnum.Red;
                        RotateRight(z.Parent.Parent);
                    }
                }
                else
                {
                    var y = z.Parent.Parent.Left;
                    if (y.Color == RedBlackTreeNode.ColorEnum.Red)
                    {
                        z.Parent.Color = RedBlackTreeNode.ColorEnum.Black;
                        y.Color = RedBlackTreeNode.ColorEnum.Black;
                        z.Parent.Parent.Color = RedBlackTreeNode.ColorEnum.Red;
                        z = z.Parent.Parent;
                    }
                    else
                    {
                        if (z == z.Parent.Left)
                        {
                            z = z.Parent;
                            RotateRight(z);
                        }

                        z.Parent.Color = RedBlackTreeNode.ColorEnum.Black;
                        z.Parent.Parent.Color = RedBlackTreeNode.ColorEnum.Red;
                        RotateLeft(z.Parent.Parent);
                    }
                }
            }

            Root.Color = RedBlackTreeNode.ColorEnum.Black;
        }

        internal void Clear()
        {
            Root = null;
        }

        private void RotateLeft(RedBlackTreeNode x)
        {
            var y = x.Right;
            x.Right = y.Left;
            if (y.Left != _leaf)
            {
                y.Left.Parent = x;
            }

            y.Parent = x.Parent;
            if (x.Parent == _leaf)
            {
                Root = y;
            }
            else if (x == x.Parent.Left)
            {
                x.Parent.Left = y;
            }
            else
            {
                x.Parent.Right = y;
            }

            y.Left = x;
            x.Parent = y;
        }
        private void RotateRight(RedBlackTreeNode x)
        {
            var y = x.Left;
            x.Left = y.Right;
            if (y.Right != _leaf)
            {
                y.Right.Parent = x;
            }
            y.Parent = x.Parent;
            if (x.Parent == _leaf)
            {
                Root = y;
            }
            else if (x == x.Parent.Left)
            {
                x.Parent.Left = y;
            }
            else
            {
                x.Parent.Right = y;
            }

            y.Right = x;
            x.Parent = y;
        }
        public Image Visualize()
        {
            Dictionary<int, Color> nodeColors = new Dictionary<int, Color>();
            GraphvizColor transparent = new GraphvizColor(Color.Transparent);
            UndirectedGraph<int, UndirectedEdge<int>> graph = new UndirectedGraph<int, UndirectedEdge<int>>();
            if (Root is null)
                return null;
            Queue<RedBlackTreeNode> queue = new Queue<RedBlackTreeNode>();
            Queue<int> queueInvis = new Queue<int>();
            queue.Enqueue(Root);
            int nextLayer = Root.Data;
            int m = -1;
            while (queue.Count > 0)
            {

                RedBlackTreeNode node = queue.Dequeue();
                nodeColors.Add(node.Data, node.Color == RedBlackTreeNode.ColorEnum.Red ? Color.Red : Color.Black);
                if (node.Left is null || node.Left.Data < 0)
                {
                    graph.AddVerticesAndEdge(new UndirectedEdge<int>(node.Data, --m));
                }
                else
                {
                    graph.AddVerticesAndEdge(new UndirectedEdge<int>(node.Data, node.Left.Data));
                    queue.Enqueue(node.Left);
                }
                if (node.Right is null || node.Right.Data < 0)
                {
                    graph.AddVerticesAndEdge(new UndirectedEdge<int>(node.Data, --m));
                }
                else
                {
                    graph.AddVerticesAndEdge(new UndirectedEdge<int>(node.Data, node.Right.Data));
                    queue.Enqueue(node.Right);
                }
                if (nextLayer == node.Data && queue.Count != 0)
                {
                    nextLayer = queue.ToArray()[queue.Count - 1].Data;
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
                    e.VertexFormatter.FillColor = e.VertexFormatter.StrokeColor = e.VertexFormatter.FontColor = new GraphvizColor(nodeColors[e.Vertex]);
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
                BackgroundGraphvizColor = new GraphvizColor(SystemColors.Control),
                DPI = 100
            };
            var graphViz = new GraphvizAlgorithm<int, UndirectedEdge<int>>(graph, @".", GraphvizImageType.Gif, graphFormat);
            graphViz.FormatVertex += FormatVertex;
            graphViz.FormatEdge += FormatEdge;
            string dot = graphViz.Generate();
            return GraphVizEngine.RenderImage(graphViz.Generate(), "gif");
        }
        internal class RedBlackTreeNode
        {
            public enum ColorEnum
            {
                Red,
                Black
            };

            public readonly int Data = -1;
            public readonly bool IsLeaf;
            public ColorEnum Color;
            public RedBlackTreeNode Parent;
            public RedBlackTreeNode Left;
            public RedBlackTreeNode Right;
            public static RedBlackTreeNode CreateLeaf()
            {
                return new RedBlackTreeNode();
            }
            public static RedBlackTreeNode CreateNode(int value, ColorEnum color)
            {
                return new RedBlackTreeNode(value, color);
            }

            internal RedBlackTreeNode(int value, ColorEnum color)
            {
                Data = value;
                Color = color;
            }
            internal RedBlackTreeNode()
            {
                IsLeaf = true;
                Color = ColorEnum.Black;
            }
            public RedBlackTreeNode Grandparent => Parent?.Parent;
            public RedBlackTreeNode Sibling =>
                Parent == null ? null : Parent.Left == this ? Parent.Right : Parent.Left;
            public RedBlackTreeNode Uncle => Parent?.Sibling;
        }
    }
}
