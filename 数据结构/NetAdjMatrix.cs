using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 数据结构
{
    public class NetAdjMatrix<T> : IGraph<T>
    {
        private Node<T>[] nodes; //顶点数组
        private int numEdges; //边的数目
        private int[,] matrix; //邻接矩阵数组

        //构造器
        public NetAdjMatrix(int n)
        {
            nodes = new Node<T>[n];
            matrix = new int[n, n];
            numEdges = 0;
        }
        //获取索引为index的顶点的信息
        public Node<T> GetNode(int index)
        {
            return nodes[index];
        }

        //设置索引为index的顶点的信息
        public void SetNode(int index, Node<T> v)
        {
            nodes[index] = v;
        }
        //边的数目属性
        public int NumEdges
        {
            get
            {
                return numEdges;
            }
            set
            {
                numEdges = value;
            }
        }

        //获取matrix[index1, index2]的值
        public int GetMatrix(int index1, int index2)
        {
            return matrix[index1, index2];
        }
        //设置matrix[index1, index2]的值
        public void SetMatrix(int index1, int index2, int v)
        {
            matrix[index1, index2] = v;
        }
        //获取顶点的数目
        public int GetNumOfVertex()
        {
            return nodes.Length;
        }
        //获取边的数目
        public int GetNumOfEdge()
        {
            return numEdges;
        }

        //v是否是无向网的顶点
        public bool IsNode(Node<T> v)
        {
            //遍历顶点数组
            foreach (Node<T> nd in nodes)
            {
                //如果顶点nd与v相等，则v是图的顶点，返回true
                if (v.Equals(nd))
                {
                    return true;
                }
            }
            return false;
        }

        //获得顶点v在顶点数组中的索引
        public int GetIndex(Node<T> v)
        {
            int i = -1;
            //遍历顶点数组
            for (i = 0; i < nodes.Length; ++i)
            {
                //如果顶点nd与v相等，则v是图的顶点，返回索引值
                if (nodes[i].Equals(v))
                {
                    return i;
                }
            }
            return i;
        }
        //在顶点v1、v2之间添加权值为v的边
        public void SetEdge(Node<T> v1, Node<T> v2, int v)
        {
            //v1或v2不是无向网的顶点
            if (!IsNode(v1) || !IsNode(v2))
            {
                Console.WriteLine("Node is not belong to Graph!");
                return;
            }
            //矩阵是对称矩阵
            matrix[GetIndex(v1), GetIndex(v2)] = v;
            matrix[GetIndex(v2), GetIndex(v1)] = v;
            ++numEdges;
        }

        //删除v1和v2之间的边
        public void DelEdge(Node<T> v1, Node<T> v2)
        {
            //v1或v2不是无向网的顶点
            if (!IsNode(v1) || !IsNode(v2))
            {
                Console.WriteLine("Node is not belong to Graph!");
                return;
            }
            //v1和v2之间存在边
            if (matrix[GetIndex(v1), GetIndex(v2)] != int.MaxValue)
            {
                //矩阵是对称矩阵
                matrix[GetIndex(v1), GetIndex(v2)] = int.MaxValue;
                matrix[GetIndex(v2), GetIndex(v1)] = int.MaxValue;
                --numEdges;
            }
        }

        //判断v1和v2之间是否存在边
        public bool IsEdge(Node<T> v1, Node<T> v2)
        {
            //v1或v2不是无向网的顶点
            if (!IsNode(v1) || !IsNode(v2))
            {
                Console.WriteLine("Node is not belong to Graph!");
                return false;
            }
            //v1和v2之间存在边
            if (matrix[GetIndex(v1), GetIndex(v2)] != int.MaxValue)
            {
                return true;
            }
            else //v1和v2之间不存在边
            {
                return false;
            }
        }

        //public int[] Prim()
        //{
        //    int[] lowcost = new int[nodes.Length]; //权值数组
        //    int[] closevex = new int[nodes.Length]; //顶点数组
        //    int mincost = int.MaxValue; //最小权值

        //    //辅助数组初始化
        //    for (int i = 1; i < nodes.Length; ++i)
        //    {
        //        lowcost[i] = matrix[0, i];
        //        closevex[i] = 0;
        //    }

        //    //某个顶点加入集合U
        //    lowcost[0] = 0;
        //    closevex[0] = 0;
        //    for (int i = 0; i < nodes.Length; ++i)
        //    {
        //        int k = 1;
        //        int j = 1;
        //        //选取权值最小的边和相应的顶点
        //        while (j < nodes.Length)
        //        {
        //            if (lowcost[j] < mincost && lowcost[j] != 0)
        //            {
        //                k = j;
        //            }
        //            ++j;
        //        }
        //        //新顶点加入集合U
        //        lowcost[k] = 0;
        //        //重新计算该顶点到其余顶点的边的权值
        //        for (j = 1; j < nodes.Length; ++j)
        //        {
        //            if (matrix[k, j] < lowcost[j])
        //            {
        //                lowcost[j] = matrix[k, j];
        //                closevex[j] = k;
        //            }
        //        }
        //    }
        //    return closevex;

        //}

        public int[] Prim()
        {
            int[] lowcost = new int[nodes.Length];   //权值数组 保存权值的数组
            int[] closevex = new int[nodes.Length];  //顶点数组 保存 相应各个顶点的数组
            int mincost = int.MaxValue;              //最小权值 默认是 int的最大值 表示无穷大

            //辅助数组初始化   对摸个  权值数组赋值   保存 最小值
            for (int i = 1; i < nodes.Length; ++i)
            {
                lowcost[i] = matrix[0, i];
                closevex[i] = 0;
            }

            //某个顶点加入集合U 
            lowcost[0] = 0;
            closevex[0] = 0;        //判断最小的权值通过的顶点的循环就此开始
            for (int i = 1; i < nodes.Length; ++i)
            {
                int k = 1;
                int j = 1;

                //选取权值最小的边和相应的顶点 
                int minSize = int.MaxValue;     
                while (j < nodes.Length)
                {
                   // if (addvnew[j] == -1 && lowcost[j] < min)
                    if (lowcost[j] < mincost && lowcost[j] != 0)
                    {
                        if (minSize > lowcost[j])
                        {
                            minSize = lowcost[j];
                            k = j;
                        }
                    }
                    ++j;
                }

                //新顶点加入集合U 
                lowcost[k] = 0;
                closevex[i] = k;
                //重新计算该顶点到其余顶点的边的权值    
                for (j = 1; j < nodes.Length; ++j)
                {
                    if (matrix[k, j]>0&&matrix[k, j] < lowcost[j])
                    {
                        lowcost[j] = matrix[k, j];
                        
                    }
                }
            }

            return closevex;
        } //我们明显的看出来，由于用到了双重循环，其算法的时间的复杂度是O(n^2)
    }
}
