using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestDoc.Models
{
    public class Document
    {
        public int ID { get; set; }
        public string URL { get; set; }
        public byte[] Content { get; set; }
        public int ReunionID { get; set; }
        public Reunion Reunion { get; set; }
        public string MimeType { get; set; }
    }
}
