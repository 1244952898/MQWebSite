using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mq.model.viewentity
{
    public class LoginValidateEntity:JsonBaseEntity
    {
        public String Key
        {
            get { return Guid.NewGuid().ToString("N"); }
        }

        public string ToUrl { get; set; }
    }
}
