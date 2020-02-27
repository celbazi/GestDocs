using GestDoc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestDoc.Data
{
    public static class DbInitializer
    {
        public static void Initialize(GestDocsContext context)
        {
            if (context.TypeReunions.Any())
            {
                return;   // DB has been seeded
            }
            var typeReunions = new TypeReunion[]
           {
                new TypeReunion { Libelle="tanmawi" },
                 new TypeReunion { Libelle="ri3ai" },
                 new TypeReunion { Libelle="jam3 3aam" }
           };

            foreach (TypeReunion s in typeReunions)
            {
                context.TypeReunions.Add(s);
            }
            context.SaveChanges();

            var adherents = new Adherent[]
          {
                 new Adherent { Nom="nom1",Prenom="prenom1",IsMember =false,Photo="" },
                 new Adherent { Nom="nom2",Prenom="prenom2",IsMember =false,Photo="" },
                 new Adherent { Nom="nom3",Prenom="prenom3",IsMember =false,Photo="" },
                 new Adherent { Nom="nom4",Prenom="prenom4",IsMember =true,Photo="" },
                 new Adherent { Nom="nom5",Prenom="prenom5",IsMember =true,Photo="" },
          };

            foreach (Adherent s in adherents)
            {
                context.Adherents.Add(s);
            }
            context.SaveChanges();
            var reunions = new Reunion[]
        {
                 new Reunion { DateReunion=DateTime.Today, Remarque="",TypeReunionID=typeReunions.Single( i => i.Libelle == "tanmawi").ID },
                 new Reunion { DateReunion=DateTime.Today, Remarque="",TypeReunionID=typeReunions.Single( i => i.Libelle == "ri3ai").ID },
                 new Reunion { DateReunion=DateTime.Today, Remarque="",TypeReunionID=typeReunions.Single( i => i.Libelle == "jam3 3aam").ID },

        };

            foreach (Reunion s in reunions)
            {
                context.Reunions.Add(s);
            }
            context.SaveChanges();
            var documents = new Document[]
        {
                 new Document {URL="" ,ReunionID=reunions.Single( i => i.TypeReunion.Libelle == "tanmawi").ID },
                 new Document {URL="" ,ReunionID=reunions.Single( i => i.TypeReunion.Libelle == "ri3ai").ID },
                 new Document {URL="" ,ReunionID=reunions.Single( i => i.TypeReunion.Libelle == "jam3 3aam").ID },

        };

            foreach (Document s in documents)
            {
                context.Documents.Add(s);
            }
            context.SaveChanges();
            var participations = new Participation[]
        {
                 new Participation { AdherentID=adherents.Single(a=>a.Nom=="nom1").ID , ReunionID=reunions.Single( i => i.TypeReunion.Libelle == "ri3ai").ID},
                 new Participation { AdherentID=adherents.Single(a=>a.Nom=="nom2").ID , ReunionID=reunions.Single( i => i.TypeReunion.Libelle == "tanmawi").ID},
                 new Participation { AdherentID=adherents.Single(a=>a.Nom=="nom2").ID , ReunionID=reunions.Single( i => i.TypeReunion.Libelle == "tanmawi").ID},

        };

            foreach (Document s in documents)
            {
                context.Documents.Add(s);
            }
            context.SaveChanges();
        }
    }
}
