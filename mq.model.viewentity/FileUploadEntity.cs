using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mq.model.viewentity
{
    public class FileUploadEntity : JsonBaseEntity
    {
        public string Attach { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
    }
}
