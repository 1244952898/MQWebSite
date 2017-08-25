using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CreateKey
{
    public class Product
    {
        public string Name { get;set; }
        public string Description { get; set; }

        public void Validate2()
        {
            Requier(o => o.Name, "aaaaaaa");
            Requier(o => o.Description, "bbbbbb");
        }

        private string Requier(Expression<Func<Product, string>> condition,string outString)
        {
            return outString;
        }
    }
}
