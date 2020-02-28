using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestDoc.Models.ViewModels
{
    public class ParticipantsAssignes
    {
        public int AdherentID { get; set; }
        public string Nom { get; set; }
        public bool Assigned { get; set; }
    }
}
