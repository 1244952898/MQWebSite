using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 数据结构
{
    class Program
    {
        static void Main(string[] args)
        {


            Sort sort=new Sort();

            int[] ints1 = new int[] {42, 20, 17, 27, 13, 8, 18, 48};
            sort.QuickSort(ints1, 0, 7);

            int[] ints = sort.InsertSort(new int[]{1,7,4,2,9,8,6});

            #region 普鲁姆算法
            NetAdjMatrix<string> netAdjMatrix = new NetAdjMatrix<string>(5);
            netAdjMatrix.SetNode(0, new Node<string>("A"));
            netAdjMatrix.SetNode(1, new Node<string>("B"));
            netAdjMatrix.SetNode(2, new Node<string>("C"));
            netAdjMatrix.SetNode(3, new Node<string>("D"));
            netAdjMatrix.SetNode(4, new Node<string>("E"));

            netAdjMatrix.SetEdge(netAdjMatrix.GetNode(0), netAdjMatrix.GetNode(1), 60);
            netAdjMatrix.SetEdge(netAdjMatrix.GetNode(0), netAdjMatrix.GetNode(2), 100);
            netAdjMatrix.SetEdge(netAdjMatrix.GetNode(0), netAdjMatrix.GetNode(3), 20);
            netAdjMatrix.SetEdge(netAdjMatrix.GetNode(0), netAdjMatrix.GetNode(4), int.MaxValue);

            netAdjMatrix.SetEdge(netAdjMatrix.GetNode(1), netAdjMatrix.GetNode(2), 80);
            netAdjMatrix.SetEdge(netAdjMatrix.GetNode(1), netAdjMatrix.GetNode(3), 95);
            netAdjMatrix.SetEdge(netAdjMatrix.GetNode(1), netAdjMatrix.GetNode(4), int.MaxValue);

            netAdjMatrix.SetEdge(netAdjMatrix.GetNode(1), netAdjMatrix.GetNode(3), int.MaxValue);
            netAdjMatrix.SetEdge(netAdjMatrix.GetNode(2), netAdjMatrix.GetNode(4), 70);

            netAdjMatrix.SetEdge(netAdjMatrix.GetNode(3), netAdjMatrix.GetNode(4), 10);

            netAdjMatrix.Prim();
            #endregion

            #region 狄克斯特拉算法
            DirecNetAdjMatrix<string> direcNetAdjMatrix=new DirecNetAdjMatrix<string>(6);

            direcNetAdjMatrix.SetNode(0, new Node<string>("A"));
            direcNetAdjMatrix.SetNode(1, new Node<string>("B"));
            direcNetAdjMatrix.SetNode(2, new Node<string>("C"));
            direcNetAdjMatrix.SetNode(3, new Node<string>("D"));
            direcNetAdjMatrix.SetNode(4, new Node<string>("E"));
            direcNetAdjMatrix.SetNode(5, new Node<string>("F"));

            //a
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(0), direcNetAdjMatrix.GetNode(0), 0);
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(0), direcNetAdjMatrix.GetNode(1), 2);
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(0), direcNetAdjMatrix.GetNode(2), int.MaxValue);
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(0), direcNetAdjMatrix.GetNode(3), int.MaxValue);
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(0), direcNetAdjMatrix.GetNode(4), int.MaxValue);
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(0), direcNetAdjMatrix.GetNode(5), int.MaxValue);

            //b
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(1), direcNetAdjMatrix.GetNode(0), int.MaxValue);
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(1), direcNetAdjMatrix.GetNode(1), 0);
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(1), direcNetAdjMatrix.GetNode(2), 15);
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(1), direcNetAdjMatrix.GetNode(3), int.MaxValue);
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(1), direcNetAdjMatrix.GetNode(4), int.MaxValue);
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(1), direcNetAdjMatrix.GetNode(5), int.MaxValue);

            //c
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(2), direcNetAdjMatrix.GetNode(0), 5);
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(2), direcNetAdjMatrix.GetNode(1), int.MaxValue);
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(2), direcNetAdjMatrix.GetNode(2), 0);
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(2), direcNetAdjMatrix.GetNode(3), int.MaxValue);
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(2), direcNetAdjMatrix.GetNode(4), int.MaxValue);
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(2), direcNetAdjMatrix.GetNode(5), int.MaxValue);

            //d
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(3), direcNetAdjMatrix.GetNode(0), 30);
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(3), direcNetAdjMatrix.GetNode(1), int.MaxValue);
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(3), direcNetAdjMatrix.GetNode(2), int.MaxValue);
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(3), direcNetAdjMatrix.GetNode(3), 0);
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(3), direcNetAdjMatrix.GetNode(4), 4);
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(3), direcNetAdjMatrix.GetNode(5), 10);

            //e
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(4), direcNetAdjMatrix.GetNode(0), int.MaxValue);
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(4), direcNetAdjMatrix.GetNode(1), 8);
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(4), direcNetAdjMatrix.GetNode(2), int.MaxValue);
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(4), direcNetAdjMatrix.GetNode(3), int.MaxValue);
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(4), direcNetAdjMatrix.GetNode(4), 0);
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(4), direcNetAdjMatrix.GetNode(5), 18);

            //f
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(5), direcNetAdjMatrix.GetNode(0), int.MaxValue);
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(5), direcNetAdjMatrix.GetNode(1), int.MaxValue);
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(5), direcNetAdjMatrix.GetNode(2), 7);
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(5), direcNetAdjMatrix.GetNode(3), int.MaxValue);
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(5), direcNetAdjMatrix.GetNode(4), int.MaxValue);
            direcNetAdjMatrix.SetEdge(direcNetAdjMatrix.GetNode(5), direcNetAdjMatrix.GetNode(5), 0);

            bool[,] pathMatricArr=new bool[6,6];
            int[] shortPathArr = new int[6];
            direcNetAdjMatrix.Dijkstra(ref pathMatricArr, ref shortPathArr, direcNetAdjMatrix.GetNode(0));

            #endregion
        }
    }
}
