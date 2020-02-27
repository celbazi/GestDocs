using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestDoc.Models
{
    public class Adherent
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Photo { get; set; }
        public bool IsMember { get; set; }
        public ICollection<Participation> Participations { get; set; }
    }
}
