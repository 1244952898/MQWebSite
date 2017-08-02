using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mq.model.viewentity
{
    public class JsonUploadFileEntity : JsonBaseEntity
    {
        public string FileUrl { get; set; }
        public string ExtensionField { get; set; }
        public string FileName { get; set; }
        public string OldFileName { get; set; }
    }
}
