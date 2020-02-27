using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestDoc.Models
{
    public class TypeReunion
    {
        public int ID { get; set; }
        public string Libelle { get; set; }
        public ICollection<Reunion> Reunions { get; set; }
    }
}
