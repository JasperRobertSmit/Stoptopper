using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System;

namespace NancyAspNetHost.DataAccess
{
    public partial class DataAccess
    {
        public static List<Blok> GetAllBlok()
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                return te.Blok.Include(e => e.Veld).ToList();
            }
        }

        public static List<Blok> GetAllBlokByEventId(int eventid)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                return te.Blok.Where(c => c.EvenementID == eventid).Include(e => e.Veld).ToList();
            }
        }

        public static Blok GetBlokById(int id)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                return te.Blok.Where(c => c.Id == id).Include(e => e.Veld).FirstOrDefault();
            }
        }

        public static bool DeleteBlokById(int id)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                te.Blok.Remove(te.Blok.Where(c => c.Id == id).Include(e => e.Veld).FirstOrDefault());
                return te.SaveChanges() > 0;
            }
        }

        public static int AddBlok(DateTime eindtijd, DateTime starttijd, int evenementid, int nummer)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                Blok blok = te.Blok.Add(new Blok()
                {
                    EindTijd = eindtijd,
                    StartTijd = starttijd,
                    EvenementID = evenementid,
                    Nummer = nummer,
                    Complete = false
                });
                te.SaveChanges();
                return blok.Id;
            }
        }
    }
}