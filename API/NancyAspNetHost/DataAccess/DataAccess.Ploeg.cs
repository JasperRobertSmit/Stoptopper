using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace NancyAspNetHost.DataAccess
{
    public partial class DataAccess
    {
        public static List<Ploeg> GetAllPloeg()
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                return te.Ploeg.Include(e => e.PloegDeelnemer).ToList();
            }
        }

        public static List<Ploeg> GetAllPloegByVeldId(int veldid)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                return te.Ploeg.Where(c => c.VeldID == veldid).Include(e => e.PloegDeelnemer).ToList();
            }
        }

        public static List<Ploeg> GetAllPloegByVerenigingId(int verenigingid)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                return te.Ploeg.Where(c => c.VerenigingID == verenigingid).Include(e => e.PloegDeelnemer).ToList();
            }
        }

        public static Ploeg GetPloegById(int id)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                return te.Ploeg.Where(c => c.Id == id).Include(e => e.PloegDeelnemer).FirstOrDefault();
            }
        }

        public static bool DeletePloegById(int id)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                te.Ploeg.Remove(te.Ploeg.Where(c => c.Id == id).Include(e => e.PloegDeelnemer).FirstOrDefault());
                return te.SaveChanges() > 0;
            }
        }

        public static int AddPloeg(string bootnaam, string naam, string rugnummer, int veldid, int verenigingid)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                Ploeg ploeg = te.Ploeg.Add(new Ploeg()
                {
                   Bootnaam = bootnaam,
                   Naam = naam,
                   Rugnummer = rugnummer,
                   VeldID = veldid,
                   VerenigingID = verenigingid
                });
                te.SaveChanges();
                return ploeg.Id;
            }
        }
    }
}