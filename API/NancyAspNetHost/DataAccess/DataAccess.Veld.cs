using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace NancyAspNetHost.DataAccess
{
    public partial class DataAccess
    {
        public static List<Veld> GetAllVeld()
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                return te.Veld.Include(e => e.Ploeg).ToList();
            }
        }

        public static List<Veld> GetAllVeldByBlokId(int blokid)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                return te.Veld.Where(c => c.BlokID == blokid).Include(e => e.Ploeg).ToList();
            }
        }

        public static Veld GetVeldById(int id)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                return te.Veld.Include(e => e.Ploeg).Where(c => c.Id == id).FirstOrDefault();
            }
        }

        public static bool DeleteVeldById(int id)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                te.Veld.Remove(te.Veld.Include(e => e.Ploeg).Where(c => c.Id == id).FirstOrDefault());
                return te.SaveChanges() > 0;
            }
        }

        public static int AddVeld(string afkorting, string naam, int blokid)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                Veld veld = te.Veld.Add(new Veld()
                {
                    Afkorting = afkorting,
                    ANaam = naam,
                    BlokID = blokid
                });
                te.SaveChanges();
                return veld.Id;
            }
        }
    }
}