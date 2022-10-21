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
    class LazyBalancedBinarySearchTree<T> where T : IComparable<T>, IConvertible
    {
        internal class Node<TT> where TT : IComparable<TT>, IConvertible
        {
            public Node(TT value)
            {
                Value = value;
            }
            public Node(TT value, Node<TT> parent)
            {
                Value = value;
                Parent = parent;
            }

            public TT Value { get; internal set; }
            internal Node<TT> Parent { get; set; }
            internal Node<TT> Right { get; set; }
            internal Node<TT> Left { get; set; }
            public int Height { get; internal set; }
            internal bool Remove(Node<TT> node)
            {
                Node<TT> curNode;
                //Если удаляем корень
                if (node == this)
                {
                    if (node.Right != null)
                    {
                        curNode = node.Right;
                    }
                    else curNode = node.Left;
                    while (curNode.Left != null)
                    {
                        curNode = curNode.Left;
                    }
                    Node<TT> temp = curNode;
                    Remove(temp);
                    node.Value = temp.Value;
                    return true;
                }
                //Удаление листьев
                if (node.Left == null && node.Right == null && node.Parent != null)
                {
                    if (node == node.Parent.Left)
                        node.Parent.Left = null;
                    else
                    {
                        node.Parent.Right = null;
                    }
                    return true;
                }
                //Удаление узла, имеющего левое поддерево, но не имеющее правого поддерева
                if (node.Left != null && node.Right == null)
                {
                    //Меняем родителя
                    node.Left.Parent = node.Parent;
                    if (node == node.Parent.Left)
                    {
                        node.Parent.Left = node.Left;
                    }
                    else if (node == node.Parent.Right)
                    {
                        node.Parent.Right = node.Left;
                    }
                    return true;
                }
                //Удаление узла, имеющего правое поддерево, но не имеющее левого поддерева
                if (node.Left == null && node.Right != null)
                {
                    //Меняем родителя
                    node.Right.Parent = node.Parent;
                    if (node == node.Parent.Left)
                    {
                        node.Parent.Left = node.Right;
                    }
                    else if (node == node.Parent.Right)
                    {
                        node.Parent.Right = node.Right;
                    }
                    return true;
                }
                //Удаляем узел, имеющий поддеревья с обеих сторон
                if (node.Right != null && node.Left != null)
                {
                    curNode = node.Right;
                    while (curNode.Left != null)
                    {
                        curNode = curNode.Left;
                    }
                    //Если самый левый элемент является первым потомком
                    if (curNode.Parent == node)
                    {
                        curNode.Left = node.Left;
                        node.Left.Parent = curNode;
                        curNode.Parent = node.Parent;
                        if (node == node.Parent.Left)
                        {
                            node.Parent.Left = curNode;
                        }
                        else if (node == node.Parent.Right)
                        {
                            node.Parent.Right = curNode;
                        }
                        return true;
                    }
                    //Если самый левый элемент НЕ является первым потомком
                    else
                    {
                        if (curNode.Right != null)
                        {
                            curNode.Right.Parent = curNode.Parent;
                        }
                        curNode.Parent.Left = curNode.Right;
                        curNode.Right = node.Right;
                        curNode.Left = node.Left;
                        node.Left.Parent = curNode;
                        node.Right.Parent = curNode;
                        curNode.Parent = node.Parent;
                        if (node == node.Parent.Left)
                        {
                            node.Parent.Left = curNode;
                        }
                        else if (node == node.Parent.Right)
                        {
                            node.Parent.Right = curNode;
                        }
                        return true;
                    }
                }
                return false;
            }
            internal void Balance()
            {
                int leftH = 0;
                int rightH = 0;
                if (!(Left is null))
                {
                    Left.Balance();
                    leftH = Left.Height;
                }
                if (!(Right is null))
                {
                    Right.Balance();
                    rightH = Right.Height;
                }
                if (leftH - rightH > 1)
                {
                    //правый поворот
                    Node<TT> left = Left;
                    Left = left.Right;
                    left.Right = this;
                    CalculateHeight();
                    left.CalculateHeight();
                    if (Value.CompareTo(Parent.Value) > 0)
                    {
                        Parent.Right = left;
                    }
                    else
                    {
                        Parent.Left = left;
                    }
                    left.Parent = Parent;
                    Parent = left;
                }
                else if (rightH - leftH > 1)
                {
                    //левый поворот
                    Node<TT> right = Right;
                    Right = right.Left;
                    right.Left = this;
                    CalculateHeight();
                    right.CalculateHeight();
                    if (Value.CompareTo(Parent.Value) > 0)
                    {
                        Parent.Right = right;
                    }
                    else
                    {
                        Parent.Left = right;
                    }
                    right.Parent = Parent;
                    Parent = right;
                }
                CalculateHeight();
            }
            internal void CalculateHeight()
            {
                int leftH = Left is null ? 0 : Left.Height;
                int rightH = Right is null ? 0 : Right.Height;
                Height = (leftH > rightH ? leftH : rightH) + 1;
            }
        }

        public Node<T> Root { get; internal set; }

        internal void BalanceRoot()
        {
            int leftH = 0;
            int rightH = 0;
            if (!(Root.Left is null))
            {
                Root.Left.Balance();
                leftH = Root.Left.Height + 1;
            }
            if (!(Root.Right is null))
            {
                Root.Right.Balance();
                rightH = Root.Right.Height + 1;
            }
            if (leftH - rightH > 1)
            {
                //правый поворот
                Node<T> left = Root.Left;
                Root.Left = left.Right;
                left.Right = Root;
                Root.CalculateHeight();
                left.CalculateHeight();
                Root.Parent = left;
                left.Right.Parent = left;
                Root = left;
                Root.Parent = null;
            }
            else if (rightH - leftH > 1)
            {
                //левый поворот
                Node<T> right = Root.Right;
                Root.Right = right.Left;
                right.Left = Root;
                Root.CalculateHeight();
                right.CalculateHeight();
                Root.Parent = right;
                right.Left.Parent = right;
                Root = right;
                Root.Parent = null;
            }
            Root.CalculateHeight();
        }

        public bool Remove(T value)
        {
            if (Root is null)
            {
                return false;
            }
            //Проверяем, существует ли данный узел
            Node<T> node = Find(value);
            if (node == null)
            {
                //Если узла не существует, вернем false
                return false;
            }
            return Root.Remove(node);
        }
        public void Add(T value)
        {
            if (Root is null)
            {
                Root = new Node<T>(value);
            }
            Node<T> next = Root;
            while (true)
            {
                if (next.Value.CompareTo(value) == 0)
                {
                    return;
                }
                if (next.Value.CompareTo(value) > 0)
                {
                    if (next.Left == null)
                    {
                        next.Left = new Node<T>(value, next);
                        return;
                    }
                    next = next.Left;
                }
                else
                {
                    if (next.Right == null)
                    {
                        next.Right = new Node<T>(value, next);
                        return;
                    }
                    next = next.Right;
                }
            }
        }
        public Node<T> Find(T value)
        {
            BalanceRoot();
            Node<T> next = Root;
            while (!(next is null) || (next.Value.CompareTo(value) == 0))
            {
                if (next.Value.CompareTo(value) > 0)
                {
                    next = next.Left;
                }
                else
                {
                    next = next.Right;
                }
            }
            return next;
        }
        public string LevelOrderTraversal()
        {
            string result = "";
            if (Root == null)
                return result;
            BalanceRoot();
            var queue = new Queue<Node<T>>();
            queue.Enqueue(Root);
            Node<T> nextLayer = Root;
            while (queue.Count > 0)
            {

                Node<T> node = queue.Dequeue();
                result += node.Value + " ";
                if (node.Left != null)
                    queue.Enqueue(node.Left);
                if (node.Right != null)
                    queue.Enqueue(node.Right);
                if ((nextLayer == node) && (queue.Count != 0))
                {
                    nextLayer = queue.ToArray()[queue.Count - 1];
                    result += "\n";
                }
            }
            return result;
        }
        public Image Visualize()
        {
            T zero = (T)Convert.ChangeType(0, typeof(T));
            GraphvizColor transparent = new GraphvizColor(Color.Transparent);
            UndirectedGraph<T, UndirectedEdge<T>> graph = new UndirectedGraph<T, UndirectedEdge<T>>();
            if (Root is null)
                return null;
            BalanceRoot();
            var queue = new Queue<Node<T>>();
            queue.Enqueue(Root);
            Node<T> nextLayer = Root;
            int m = 0;
            while (queue.Count > 0)
            {

                Node<T> node = queue.Dequeue();
                if (node.Left is null)
                {
                    graph.AddVerticesAndEdge(new UndirectedEdge<T>(node.Value, (T)Convert.ChangeType(--m, typeof(T))));
                }
                else
                {
                    graph.AddVerticesAndEdge(new UndirectedEdge<T>(node.Value, node.Left.Value));
                    queue.Enqueue(node.Left);
                }
                if (node.Right is null)
                {
                    graph.AddVerticesAndEdge(new UndirectedEdge<T>(node.Value, (T)Convert.ChangeType(--m, typeof(T))));
                }
                else
                {
                    graph.AddVerticesAndEdge(new UndirectedEdge<T>(node.Value, node.Right.Value));
                    queue.Enqueue(node.Right);
                }

                if ((nextLayer == node) && (queue.Count != 0))
                {
                    nextLayer = queue.ToArray()[queue.Count - 1];
                }
            }
            void FormatVertex(object sender1, FormatVertexEventArgs<T> e)
            {
                e.VertexFormatter.Label = e.Vertex.ToString();
                e.VertexFormatter.Shape = GraphvizVertexShape.Circle;
                e.VertexFormatter.BottomLabel = e.Vertex.ToString();
                e.VertexFormatter.Font = new GraphvizFont("Times-Roman", 16);
                if (e.Vertex.CompareTo(zero) < 0)
                {
                    e.VertexFormatter.FillColor = e.VertexFormatter.StrokeColor = e.VertexFormatter.FontColor = transparent;
                }
                else
                {
                    e.VertexFormatter.FillColor = e.VertexFormatter.StrokeColor = e.VertexFormatter.FontColor = GraphvizColor.Black;
                }
            }
            void FormatEdge(object sender1, FormatEdgeEventArgs<T, UndirectedEdge<T>> e)
            {
                e.EdgeFormatter.Font = new GraphvizFont("Times-Roman", 16);
                if (e.Edge.Target.CompareTo(zero) < 0)
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
            var graphViz = new GraphvizAlgorithm<T, UndirectedEdge<T>>(graph, @".", GraphvizImageType.Gif, graphFormat);
            graphViz.FormatVertex += FormatVertex;
            graphViz.FormatEdge += FormatEdge;
            string dot = graphViz.Generate();
            return GraphVizEngine.RenderImage(graphViz.Generate(), "gif");
        }
    }
}
