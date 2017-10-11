using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HowMVCWork.Models
{
    public class Model
    {
        public string Foo { get; set; }

        [UIHint("Template A")]
        [UIHint("Template B", "Mvc")]
        public string Bar { get; set; }

        [UIHint("Template A")]
        public string Baz { get; set; } 
    }


}