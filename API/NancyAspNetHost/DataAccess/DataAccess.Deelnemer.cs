using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace NancyAspNetHost.DataAccess
{
    public partial class DataAccess
    {
        public static List<Deelnemer> GetAllDeelnemer()
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                return te.Deelnemer.ToList();
            }
        }

        public static List<Deelnemer> GetAllDeelnemerByPloeg(int ploegid)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                return te.Deelnemer.Join(te.PloegDeelnemer,
                     s => s.Id,
                     c => c.DeelnemerID,
                     (s, c) => new { s, c }).Where(o => o.c.PloegID == ploegid).Select(o => o.s).ToList();
            }
        }

        public static Deelnemer GetDeelnemerById(int id)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                return te.Deelnemer.Where(c => c.Id == id).FirstOrDefault();
            }
        }

        public static bool DeleteDeelnemerById(int id)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                te.PloegDeelnemer.Remove(te.PloegDeelnemer.Where(c => c.DeelnemerID == id).FirstOrDefault());
                te.Deelnemer.Remove(te.Deelnemer.Where(c => c.Id == id).FirstOrDefault());
                return te.SaveChanges() > 0;
            }
        }

        public static int AddDeelnemer(int persoonid, int ploegid, int nummer)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                Deelnemer p = te.Deelnemer.Add(new Deelnemer()
                {
                    PersoonID = persoonid,
                    Nummer = nummer
                });
                te.SaveChanges();
                te.PloegDeelnemer.Add(new PloegDeelnemer()
                {
                    DeelnemerID = p.Id,
                    PloegID = ploegid
                });
                te.SaveChanges();
                return p.Id;
            }
        }
    }
}