using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 数据结构
{
    public class adjListNode<T>
    {
        private int adjvex; //邻接顶点
        private adjListNode<T> next; //下一个邻接表结点

        //邻接顶点属性
        public int Adjvex
        {
            get { return adjvex; }
            set { adjvex = value; }
        }

        //下一个邻接表结点属性
        public adjListNode<T> Next
        {
            get
            {
                return next;
            }
            set
            {
                next = value;
            }
        }

        public adjListNode(int vex)
        {
            adjvex = vex;
            next = null;
        }
    }
}
