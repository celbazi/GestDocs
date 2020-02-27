using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestDoc.Models
{
    public class Participation
    {
        public int ID { get; set; }
        public int ReunionID { get; set; }
        public Reunion Reunion { get; set; }
        public int AdherentID { get; set; }
        public Adherent Adherent { get; set; }
    }
}
