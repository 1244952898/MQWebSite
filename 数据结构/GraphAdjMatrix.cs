using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 数据结构
{
    public class GraphAdjMatrix<T> : IGraph<T>
    {
        private Node<T>[] nodes; //顶点数组
        private int numEdges; //边的数目
        private int[,] matrix; //邻接矩阵数组

        public GraphAdjMatrix(int n)
        {
            nodes = new Node<T>[n];
            matrix = new int[n, n];
            numEdges = 0;
        }

        public int NumEdges
        {
            get { return numEdges; }
            set { numEdges = value; }
        }

        public Node<T> GetNode(int index)
        {
            return nodes[index];
        }

        public void SetNode(int index, Node<T> node)
        {
            nodes[index] = node;
        }

        public int GetMatrix(int index1, int index2)
        {
            return matrix[index1, index2];
        }

        public void SetMatrix(int index1, int index2)
        {
            matrix[index1, index2] = 1;
        }

        public int GetNumOfVertex()
        {
            return numEdges;
        }

        public bool IsNode(Node<T> v)
        {
            foreach (var node in nodes)
            {
                if (v.Equals(node))
                {
                    return true;
                }
            }
            return false;
        }

        public int GetIndex(Node<T> v)
        {
            int i = -1;
            for (i = 0; i < nodes.Length; i++)
            {
                if (v.Equals(nodes[i]))
                {
                    return i;
                }
            }
            return i;
        }


        public int GetNumOfEdge()
        {
            throw new NotImplementedException();
        }

        public void SetEdge(Node<T> v1, Node<T> v2, int v)
        {
            if (!IsNode(v1) || !IsNode(v2))
            {
                Console.WriteLine("Node is not belong to Graph!");
                return;
            }
            //不是无向图
            if (v != 1)
            {
                Console.WriteLine("Weight is not right!");
                return;
            }
            matrix[GetIndex(v1), GetIndex(v2)] = v;
            matrix[GetIndex(v2), GetIndex(v1)] = v;
            numEdges++;
        }

        public void DelEdge(Node<T> v1, Node<T> v2)
        {
            if (!IsNode(v1) || !IsNode(v2))
            {
                Console.WriteLine("Node is not belong to Graph!");
                return;
            }
            if (matrix[GetIndex(v1), GetIndex(v2)] == 1)
            {
                matrix[GetIndex(v1), GetIndex(v2)] = 0;
                matrix[GetIndex(v2), GetIndex(v1)] = 0;
                numEdges--;
            }
        }

        public bool IsEdge(Node<T> v1, Node<T> v2)
        {
            //v1或v2不是图的顶点
            if (!IsNode(v1) || !IsNode(v2))
            {
                Console.WriteLine("Node is not belong to Graph!");
                return false;
            }
            //顶点v1与v2之间存在边
            if (matrix[GetIndex(v1), GetIndex(v2)] == 1)
            {
                return true;
            }
            else //不存在边
            {
                return false;
            }
        }
    }
}
