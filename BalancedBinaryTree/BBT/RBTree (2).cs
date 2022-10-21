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
    class RBTree
    {
        private int intCount;
        private RedBlackNode rbTree;
        public static RedBlackNode sentinelNode;
        private RedBlackNode lastNodeFound;

        public RBTree()
        {
            sentinelNode = new RedBlackNode
            {
                Left = null,
                Right = null,
                Parent = null,
                Color = RedBlackNode.BLACK
            };
            rbTree = sentinelNode;
            lastNodeFound = sentinelNode;
        }

        public void Add(int data)
        { 
            int result;
            RedBlackNode node = new RedBlackNode();
            RedBlackNode temp = rbTree;
            while (temp != sentinelNode)
            {
                node.Parent = temp;
                result = data.CompareTo(temp.Data);
                if (result == 0)
                    return;
                    //throw (new Exception("Node with the same data already exists!"));
                if (result > 0)
                    temp = temp.Right;
                else
                    temp = temp.Left;
            }
            node.Data = data;
            node.Left = sentinelNode;
            node.Right = sentinelNode;
            if (node.Parent != null)
            {
                if (node.Data.CompareTo(node.Parent.Data) > 0)
                    node.Parent.Right = node;
                else
                    node.Parent.Left = node;
            }
            else
                rbTree = node;
            RestoreAfterInsert(node);
            lastNodeFound = node;
            intCount += 1;
        }
		private void RestoreAfterInsert(RedBlackNode x)
        {
            RedBlackNode y;
            while (x != rbTree && x.Parent.Color == RedBlackNode.RED)
            {
                if (x.Parent == x.Parent.Parent.Left)
                {
                    y = x.Parent.Parent.Right;
                    if (y != null && y.Color == RedBlackNode.RED)
                    {
                        x.Parent.Color = RedBlackNode.BLACK;
                        y.Color = RedBlackNode.BLACK;
                        x.Parent.Parent.Color = RedBlackNode.RED;
                        x = x.Parent.Parent;
                    }
                    else
                    {
                        if (x == x.Parent.Right)
                        { 
                            x = x.Parent;
                            RotateLeft(x);
                        }
                        x.Parent.Color = RedBlackNode.BLACK;
                        x.Parent.Parent.Color = RedBlackNode.RED;
                        RotateRight(x.Parent.Parent);
                    }
                }
                else
                {
                    y = x.Parent.Parent.Left;
                    if (y != null && y.Color == RedBlackNode.RED)
                    {
                        x.Parent.Color = RedBlackNode.BLACK;
                        y.Color = RedBlackNode.BLACK;
                        x.Parent.Parent.Color = RedBlackNode.RED;
                        x = x.Parent.Parent;
                    }
                    else
                    {
                        if (x == x.Parent.Left)
                        {
                            x = x.Parent;
                            RotateRight(x);
                        }
                        x.Parent.Color = RedBlackNode.BLACK;
                        x.Parent.Parent.Color = RedBlackNode.RED;
                        RotateLeft(x.Parent.Parent);
                    }
                }
            }
            rbTree.Color = RedBlackNode.BLACK;
        }
        public void RotateLeft(RedBlackNode x)
        {
            RedBlackNode y = x.Right;
            x.Right = y.Left;
            if (y.Left != sentinelNode)
                y.Left.Parent = x;
            if (y != sentinelNode)
                y.Parent = x.Parent;
            if (x.Parent != null)
            {
                if (x == x.Parent.Left)
                    x.Parent.Left = y;
                else
                    x.Parent.Right = y;
            }
            else
                rbTree = y;
            y.Left = x;
            if (x != sentinelNode)
                x.Parent = y;
        }
        public void RotateRight(RedBlackNode x)
        {
            RedBlackNode y = x.Left;
            x.Left = y.Right;
            if (y.Right != sentinelNode)
                y.Right.Parent = x;
            if (y != sentinelNode)
                y.Parent = x.Parent;
            if (x.Parent != null)
            {
                if (x == x.Parent.Right)
                    x.Parent.Right = y;
                else
                    x.Parent.Left = y;
            }
            else
                rbTree = y;
            y.Right = x;
            if (x != sentinelNode)
                x.Parent = y;
        }
        public int GetMinData()
        {
            RedBlackNode treeNode = rbTree;
            if (treeNode == null || treeNode == sentinelNode)
                throw (new Exception("Tree is empty"));
            while (treeNode.Left != sentinelNode)
                treeNode = treeNode.Left;
            lastNodeFound = treeNode;
            return treeNode.Data;
        }
        public int GetMaxData()
        {
            RedBlackNode treeNode = rbTree;

            if (treeNode == null || treeNode == sentinelNode)
                throw (new Exception("Tree is empty"));
            while (treeNode.Right != sentinelNode)
                treeNode = treeNode.Right;
            lastNodeFound = treeNode;
            return treeNode.Data;

        }
        public bool IsEmpty()
        {
            return (rbTree == null);
        }
        public void Remove(int data)
        {
            int result;
            RedBlackNode node;
            result = data.CompareTo(lastNodeFound.Data);
            if (result == 0)
                node = lastNodeFound;
            else
            {
                node = rbTree;
                while (node != sentinelNode)
                {
                    result = data.CompareTo(node.Data);
                    if (result == 0)
                        break;
                    if (result < 0)
                        node = node.Left;
                    else
                        node = node.Right;
                }

                if (node == sentinelNode)
                    return;
            }

            Delete(node);

            intCount -= 1;
        }
        private void Delete(RedBlackNode z)
        {
            RedBlackNode x;
            RedBlackNode y;
            if (z.Left == sentinelNode || z.Right == sentinelNode)
                y = z;
            else
            {
                y = z.Right; 
                while (y.Left != sentinelNode)
                    y = y.Left;
            }
            if (y.Left != sentinelNode)
                x = y.Left;
            else
                x = y.Right;
            x.Parent = y.Parent;
            if (y.Parent != null)
                if (y == y.Parent.Left)
                    y.Parent.Left = x;
                else
                    y.Parent.Right = x;
            else
                rbTree = x;

            if (y != z)
            {
                z.Data = y.Data;
            }

            if (y.Color == RedBlackNode.BLACK)
                RestoreAfterDelete(x);

            lastNodeFound = sentinelNode;
        }
		private void RestoreAfterDelete(RedBlackNode x)
        {
            RedBlackNode y;
            while (x != rbTree && x.Color == RedBlackNode.BLACK)
            {
                if (x == x.Parent.Left) 
                {
                    y = x.Parent.Right; 
                    if (y.Color == RedBlackNode.RED)
                    { 
                        y.Color = RedBlackNode.BLACK;
                        x.Parent.Color = RedBlackNode.RED;
                        RotateLeft(x.Parent);
                        y = x.Parent.Right;
                    }
                    if (y.Left.Color == RedBlackNode.BLACK &&
                        y.Right.Color == RedBlackNode.BLACK)
                    {  
                        y.Color = RedBlackNode.RED;
                        x = x.Parent;
                    }
                    else
                    {
                        if (y.Right.Color == RedBlackNode.BLACK)
                        {
                            y.Left.Color = RedBlackNode.BLACK;
                            y.Color = RedBlackNode.RED;
                            RotateRight(y);
                            y = x.Parent.Right;
                        }
                        y.Color = x.Parent.Color;
                        x.Parent.Color = RedBlackNode.BLACK;
                        y.Right.Color = RedBlackNode.BLACK;
                        RotateLeft(x.Parent);
                        x = rbTree;
                    }
                }
                else
                {
                    y = x.Parent.Left;
                    if (y.Color == RedBlackNode.RED)
                    {
                        y.Color = RedBlackNode.BLACK;
                        x.Parent.Color = RedBlackNode.RED;
                        RotateRight(x.Parent);
                        y = x.Parent.Left;
                    }
                    if (y.Right.Color == RedBlackNode.BLACK &&
                        y.Left.Color == RedBlackNode.BLACK)
                    {
                        y.Color = RedBlackNode.RED;
                        x = x.Parent;
                    }
                    else
                    {
                        if (y.Left.Color == RedBlackNode.BLACK)
                        {
                            y.Right.Color = RedBlackNode.BLACK;
                            y.Color = RedBlackNode.RED;
                            RotateLeft(y);
                            y = x.Parent.Left;
                        }
                        y.Color = x.Parent.Color;
                        x.Parent.Color = RedBlackNode.BLACK;
                        y.Left.Color = RedBlackNode.BLACK;
                        RotateRight(x.Parent);
                        x = rbTree;
                    }
                }
            }
            x.Color = RedBlackNode.BLACK;
        }
        public void RemoveMin()
        {
            if (rbTree == null)
                throw (new Exception("Tree is empty"));

            Remove(GetMinData());
        }
        public void RemoveMax()
        {
            if (rbTree == null)
                throw (new Exception("Tree is empty"));

            Remove(GetMaxData());
        }
        public void Clear()
        {
            rbTree = sentinelNode;
            intCount = 0;
        }
        public int Size()
        {
            return intCount;
        }
        public Image Visualize()
        {
            GraphvizColor transparent = new GraphvizColor(Color.Transparent);
            UndirectedGraph<int, UndirectedEdge<int>> graph = new UndirectedGraph<int, UndirectedEdge<int>>();
            if (rbTree is null)
                return null;
            Queue<RedBlackNode> queue = new Queue<RedBlackNode>();
            Queue<int> queueInvis = new Queue<int>();
            queue.Enqueue(rbTree);
            int nextLayer = rbTree.Data;
            int m = -1;
            while (queue.Count > 0)
            {

                RedBlackNode node = queue.Dequeue();
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
        public Image VisualizeSlow()
        {
            GraphvizColor transparent = new GraphvizColor(Color.Transparent);
            UndirectedGraph<int, UndirectedEdge<int>> graph = new UndirectedGraph<int, UndirectedEdge<int>>();
            if (rbTree is null)
                return null;
            Queue<RedBlackNode> queue = new Queue<RedBlackNode>();
            Queue<int> queueInvis = new Queue<int>();
            queue.Enqueue(rbTree);
            int nextLayer = rbTree.Data;
            int m = -1;
            while (queue.Count > 0)
            {

                RedBlackNode node = queue.Dequeue();
                if (node.Left is null || node.Left.Data < 0)
                {
                    graph.AddVerticesAndEdge(new UndirectedEdge<int>(node.Data, --m));
                    queueInvis.Enqueue(m);
                }
                else
                {
                    graph.AddVerticesAndEdge(new UndirectedEdge<int>(node.Data, node.Left.Data));
                    queue.Enqueue(node.Left);
                }
                if (node.Right is null || node.Right.Data < 0)
                {
                    graph.AddVerticesAndEdge(new UndirectedEdge<int>(node.Data, --m));
                    queueInvis.Enqueue(m);
                }
                else
                {
                    graph.AddVerticesAndEdge(new UndirectedEdge<int>(node.Data, node.Right.Data));
                    queue.Enqueue(node.Right);
                }
                if (nextLayer == node.Data && queue.Count != 0)
                {
                        nextLayer = queue.ToArray()[queue.Count - 1].Data;
                    Queue<int> newQueueInvis = new Queue<int>();
                    while (queueInvis.Count > 0)
                    {
                        int currentInvis = queueInvis.Dequeue();
                        graph.AddVerticesAndEdge(new UndirectedEdge<int>(currentInvis, --m));
                        newQueueInvis.Enqueue(m);
                        graph.AddVerticesAndEdge(new UndirectedEdge<int>(currentInvis, --m));
                        newQueueInvis.Enqueue(m);
                    }
                    queueInvis = newQueueInvis;
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
                DPI = 100
            };
            var graphViz = new GraphvizAlgorithm<int, UndirectedEdge<int>>(graph, @".", GraphvizImageType.Gif, graphFormat);
            graphViz.FormatVertex += FormatVertex;
            graphViz.FormatEdge += FormatEdge;
            string dot = graphViz.Generate();
            return GraphVizEngine.RenderImage(graphViz.Generate(), "gif");
        }
        public class RedBlackNode
        {
            public static int RED = 0;
            public static int BLACK = 1;

            public int Data { get; set; } = -1;
            public int Color { get; set; }
            public RedBlackNode Left { get; set; }
            public RedBlackNode Right { get; set; }
            public RedBlackNode Parent { get; set; }
            public RedBlackNode()
            {
                Color = RED;
                Data = -1;
            }
        }
    }
}
