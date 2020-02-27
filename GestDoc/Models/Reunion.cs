using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestDoc.Models
{
    public class Reunion
    {
        public int ID { get; set; }
        public DateTime DateReunion { get; set; }
        public string Remarque { get; set; }
        public int TypeReunionID { get; set; }
        public TypeReunion TypeReunion { get; set; }
        public ICollection<Document> Documents { get; set; }
        public ICollection<Participation> Participants { get; set; }
    }
}
