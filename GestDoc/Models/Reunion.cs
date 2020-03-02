using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestDoc.Models
{
    public class Reunion
    {
        public int ID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateReunion { get; set; }
        public string Remarque { get; set; }
        public int TypeReunionID { get; set; }
        public TypeReunion TypeReunion { get; set; }
        public ICollection<Document> Documents { get; set; }
        public ICollection<Participation> Participants { get; set; }
    }
}
