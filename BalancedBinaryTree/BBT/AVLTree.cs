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
    class AVLTree
    {
        class Node
        {
            public int Data { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public Node(int data)
            {
                Data = data;
            }
        }
        Node root;
        public AVLTree()
        { }
        public void Add(int data)
        {
            Node newItem = new Node(data);
            if (root == null)
            {
                root = newItem;
            }
            else
            {
                root = RecursiveInsert(root, newItem);
            }
        }
        private Node RecursiveInsert(Node current, Node n)
        {
            if (current == null)
            {
                current = n;
                return current;
            }
            else if (n.Data < current.Data)
            {
                current.Left = RecursiveInsert(current.Left, n);
                current = Balance_tree(current);
            }
            else if (n.Data > current.Data)
            {
                current.Right = RecursiveInsert(current.Right, n);
                current = Balance_tree(current);
            }
            return current;
        }
        private Node Balance_tree(Node current)
        {
            int b_factor = Balance_factor(current);
            if (b_factor > 1)
            {
                if (Balance_factor(current.Left) > 0)
                {
                    current = RotateLL(current);
                }
                else
                {
                    current = RotateLR(current);
                }
            }
            else if (b_factor < -1)
            {
                if (Balance_factor(current.Right) > 0)
                {
                    current = RotateRL(current);
                }
                else
                {
                    current = RotateRR(current);
                }
            }
            return current;
        }
        public void Remove(int target)
        {
            root = Remove(root, target);
        }
        private Node Remove(Node current, int target)
        {
            Node parent;
            if (current == null)
            { return null; }
            else
            {
                if (target < current.Data)
                {
                    current.Left = Remove(current.Left, target);
                    if (Balance_factor(current) == -2)
                    {
                        if (Balance_factor(current.Right) <= 0)
                        {
                            current = RotateRR(current);
                        }
                        else
                        {
                            current = RotateRL(current);
                        }
                    }
                }
                else if (target > current.Data)
                {
                    current.Right = Remove(current.Right, target);
                    if (Balance_factor(current) == 2)
                    {
                        if (Balance_factor(current.Left) >= 0)
                        {
                            current = RotateLL(current);
                        }
                        else
                        {
                            current = RotateLR(current);
                        }
                    }
                }
                else
                {
                    if (current.Right != null)
                    {
                        parent = current.Right;
                        while (parent.Left != null)
                        {
                            parent = parent.Left;
                        }
                        current.Data = parent.Data;
                        current.Right = Remove(current.Right, parent.Data);
                        if (Balance_factor(current) == 2)
                        {
                            if (Balance_factor(current.Left) >= 0)
                            {
                                current = RotateLL(current);
                            }
                            else { current = RotateLR(current); }
                        }
                    }
                    else
                    {
                        return current.Left;
                    }
                }
            }
            return current;
        }
        public void Find(int key)
        {
            if (Find(key, root).Data == key)
            {
                Console.WriteLine("{0} was found!", key);
            }
            else
            {
                Console.WriteLine("Nothing found!");
            }
        }
        public void Clear()
        {
            root = null;
        }
        private Node Find(int target, Node current)
        {

            if (target < current.Data)
            {
                if (target == current.Data)
                {
                    return current;
                }
                else
                    return Find(target, current.Left);
            }
            else
            {
                if (target == current.Data)
                {
                    return current;
                }
                else
                    return Find(target, current.Right);
            }

        }
        private int GetHeight(Node current)
        {
            int height = 0;
            if (current != null)
            {
                int l = GetHeight(current.Left);
                int r = GetHeight(current.Right);
                int m = Math.Max(l, r);
                height = m + 1;
            }
            return height;
        }
        private int Balance_factor(Node current)
        {
            int l = GetHeight(current.Left);
            int r = GetHeight(current.Right);
            int b_factor = l - r;
            return b_factor;
        }
        private Node RotateRR(Node parent)
        {
            Node pivot = parent.Right;
            parent.Right = pivot.Left;
            pivot.Left = parent;
            return pivot;
        }
        private Node RotateLL(Node parent)
        {
            Node pivot = parent.Left;
            parent.Left = pivot.Right;
            pivot.Right = parent;
            return pivot;
        }
        private Node RotateLR(Node parent)
        {
            Node pivot = parent.Left;
            parent.Left = RotateRR(pivot);
            return RotateLL(parent);
        }
        private Node RotateRL(Node parent)
        {
            Node pivot = parent.Right;
            parent.Right = RotateLL(pivot);
            return RotateRR(parent);
        }
        public Image Visualize()
        {
            GraphvizColor transparent = new GraphvizColor(Color.Transparent);
            UndirectedGraph<int, UndirectedEdge<int>> graph = new UndirectedGraph<int, UndirectedEdge<int>>();
            if (root is null)
                return null;
            Queue<Node> queue = new Queue<Node>();
            Queue<int> queueInvis = new Queue<int>();
            queue.Enqueue(root);
            int nextLayer = root.Data;
            int m = -1;
            while (queue.Count > 0)
            {

                Node node = queue.Dequeue();
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
                BackgroundGraphvizColor = new GraphvizColor(SystemColors.Control),
                DPI = 100
            };
            var graphViz = new GraphvizAlgorithm<int, UndirectedEdge<int>>(graph, @".", GraphvizImageType.Gif, graphFormat);
            graphViz.FormatVertex += FormatVertex;
            graphViz.FormatEdge += FormatEdge;
            string dot = graphViz.Generate();
            return GraphVizEngine.RenderImage(graphViz.Generate(), "gif");
        }
    }
}
