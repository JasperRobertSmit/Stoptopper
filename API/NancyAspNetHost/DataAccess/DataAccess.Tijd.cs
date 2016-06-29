using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace NancyAspNetHost.DataAccess
{
    public partial class DataAccess
    {
        public static List<Tijd> GetAllTijd()
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                return te.Tijd.Include(e => e.Blok).Include(e => e.Ploeg).ToList();
            }
        }

        public static List<Tijd> GetAllTijdByPloegId(int ploegid)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                return te.Tijd.Where(e => e.PloegID == ploegid).Include(e => e.Blok).Include(e => e.Ploeg).ToList();
            }
        }

        public static List<Tijd> GetAllTijdByBlokId(int blokid)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                return te.Tijd.Where(e => e.BlokID == blokid).Include(e => e.Blok).Include(e => e.Ploeg).ToList();
            }
        }

        public static List<Tijd> GetAllTijdByPloegIdBlokId(int ploegid, int blokid)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                return te.Tijd.Where(e => e.PloegID == ploegid && e.BlokID == blokid).Include(e => e.Blok).Include(e => e.Ploeg).ToList();
            }
        }

        public static int AddTijd(int ploegid, int blokid, DateTime time)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                Tijd tijd = te.Tijd.Add(new Tijd()
                {
                    PloegID = ploegid,
                    BlokID = blokid,
                    Tijd1 = time
                });
                te.SaveChanges();
                return tijd.Id;
            }
        }
    }
}