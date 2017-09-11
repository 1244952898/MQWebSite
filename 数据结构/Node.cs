using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 数据结构
{
    public class Node<T>
    {
        private T data;

        public T Data
        {
            get { return data; }
            set { data = value; }
        }

        public Node(T v)
        {
            data = v;
        }
    }
}
