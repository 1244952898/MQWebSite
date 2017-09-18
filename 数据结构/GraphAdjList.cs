using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 数据结构
{
    public class GraphAdjList<T> : IGraph<T>
    {
        //邻接表数组
        private VexNode<T>[] adjList;
        private int[] visited;

        //索引器
        public VexNode<T> this[int index]
        {
            get
            {
                return adjList[index];
            }
            set
            {
                adjList[index] = value;
            }
        }

        //构造器
        public GraphAdjList(Node<T>[] nodes)
        {
            adjList = new VexNode<T>[nodes.Length];
            for (int i = 0; i < nodes.Length; ++i)
            {
                adjList[i].Data = nodes[i];
                adjList[i].FirstAdj = null;
            }

            //以下为添加的代码
            visited = new int[adjList.Length];
            for (int i = 0; i < visited.Length; ++i)
            {
                visited[i] = 0;
            }
        }

        //无向图的深度优先遍历算法的实现如下
        public void DFS()
        {
            for (int i = 0; i < visited.Length; ++i)
            {
                if (visited[i] == 0)
                {
                    DFSAL(i);
                }
            }
        }

        //从某个顶点出发进行深度优先遍历
        public void DFSAL(int i)
        {
            visited[i] = 1;
            adjListNode<T> p = adjList[i].FirstAdj;
            while (p != null)
            {
                if (visited[p.Adjvex] == 0)
                {
                    DFSAL(p.Adjvex);
                }
                p = p.Next;
            }
        }

        public void BFS()
        {
            for (int i = 0; i < visited.Length; ++i)
            {
                if (visited[i] == 0)
                {
                    //BFSAL(i);
                }
            }
        }

        //从某个顶点出发进行广度优先遍历
        //public void BFSAL(int i)
        //{
        //    visited[i] = 1;
        //    CSeqQueue<int> cq = new CSeqQueue<int>(visited.Length);
        //    cq.In(i);
        //    while (!cq.IsEmpty())
        //    {
        //        int k = cq.Out();
        //        adjListNode<T> p = adjList[k].FirstAdj;
        //        while (p != null)
        //        {
        //            if (visited[p.Adjvex] == 0)
        //            {
        //                visited[p.Adjvex] = 1;
        //                cq.In(p.Adjvex);
        //            }
        //            p = p.Next;
        //        }
        //    }
        //}

        public int GetNumOfVertex()
        {
            return adjList.Length;
        }

        public int GetNumOfEdge()
        {
            int i = 0;
            foreach (VexNode<T> nd in adjList)
            {
                adjListNode<T> p = nd.FirstAdj;
                while (p != null)
                {
                    i++;
                    p = p.Next;
                }
            }
            return i / 2;
        }

        //判断v是否是图的顶点
        public bool IsNode(Node<T> v)
        {
            return adjList.Any(vexNode => v.Equals(vexNode.Data));
        }

        //获取顶点v在邻接表数组中的索引
        public int GetIndex(Node<T> v)
        {
            int i = -1;
            for (i = 0; i < adjList.Length; i++)
            {
                if (v.Equals(adjList[i].Data))
                {
                    return i;
                }
            }
            return i;
        }

        public void SetEdge(Node<T> v1, Node<T> v2, int v)
        {
            //v1或v2不是图的顶点或者v1和v2之间存在边
            if (!IsNode(v1) || !IsNode(v2) || IsEdge(v1, v2))
            {
                Console.WriteLine("Node is not belong to Graph!");
                return;
            }
            //权值不对
            if (v != 1)
            {
                Console.WriteLine("Weight is not right!");
                return;
            }
            //处理顶点v1的邻接表
            adjListNode<T> p = new adjListNode<T>(GetIndex(v2))
            {
                Next = adjList[GetIndex(v1)].FirstAdj
            };
            adjList[GetIndex(v1)].FirstAdj = p;

            //处理顶点v2的邻接表
            p = new adjListNode<T>(GetIndex(v1))
            {
                Next = adjList[GetIndex(v2)].FirstAdj
            };
            adjList[GetIndex(v2)].FirstAdj = p;

        }

        public void DelEdge(Node<T> v1, Node<T> v2)
        {
            if (!IsNode(v1) || !IsNode(v2))
            {
                Console.WriteLine("不是点");
                return;
            }
            if (IsEdge(v1, v2))
            {
                adjListNode<T> p = adjList[GetIndex(v1)].FirstAdj;
                adjListNode<T> pre = null;
                while (p != null)
                {
                    if (p.Adjvex != GetIndex(v2))
                    {
                        pre = p;
                        p = p.Next;
                    }
                }
                pre.Next = p.Next;
                p = adjList[GetIndex(v2)].FirstAdj;
                pre = null;
                while (p != null)
                {
                    if (p.Adjvex != GetIndex(v1))
                    {
                        pre = p;
                        p = p.Next;
                    }
                }
                pre.Next = p.Next;
            }
        }

        public bool IsEdge(Node<T> v1, Node<T> v2)
        {
            if (!IsNode(v1) || !IsNode(v2))
            {
                Console.WriteLine("不是点");
                return false;
            }

            adjListNode<T> p = adjList[GetIndex(v1)].FirstAdj;
            while (p != null)
            {
                if (p.Adjvex == GetIndex(v2))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
