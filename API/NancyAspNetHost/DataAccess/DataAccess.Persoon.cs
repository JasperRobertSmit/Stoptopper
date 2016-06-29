using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System;

namespace NancyAspNetHost.DataAccess
{
    public partial class DataAccess
    {
        public static List<Persoon> GetAllPersoon()
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                return te.Persoon.ToList();
            }
        }

        public static List<Persoon> GetAllPersoonByVerenigingId(int verenigingid)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                return te.Persoon.Where(c => c.VerenigingID == verenigingid).ToList();
            }
        }


        public static List<Persoon> GetAllPersoonByDeelnemerId(int deelnemerid)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                return te.Persoon.Join(te.Deelnemer,
                     s => s.Id,
                     c => c.PersoonID,
                     (s, c) => new { s, c }).Where(o => o.c.Id == deelnemerid).Select(o => o.s).ToList();
            }
        }

        public static Persoon GetPersoonById(int id)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                return te.Persoon.Where(c => c.Id == id).FirstOrDefault();
            }
        }

        public static bool DeletePersoonById(int id)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                te.Persoon.Remove(te.Persoon.Where(c => c.Id == id).FirstOrDefault());
                return te.SaveChanges() > 0;
            }
        }

        public static int AddPersoon(DateTime geboortedatum, string knrbid, int verenigingid, string naam)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                Persoon persoon = te.Persoon.Add(new Persoon()
                {
                   GeboorteDatum = geboortedatum,
                   KNRBID = knrbid,
                   VerenigingID = verenigingid,
                   Naam = naam
                });
                te.SaveChanges();
                return persoon.Id;
            }
        }
    }
}