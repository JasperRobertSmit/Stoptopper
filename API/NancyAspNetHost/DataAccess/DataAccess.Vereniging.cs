using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace NancyAspNetHost.DataAccess
{
    public partial class DataAccess
    {
        public static List<Vereniging> GetAllVereniging()
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                return te.Vereniging.Include(e => e.Ploeg).Include(e => e.Event).Include(e => e.Persoon).ToList();
            }
        }

        public static Vereniging GetVerenigingById(int id)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                return te.Vereniging.Where(c => c.Id == id).Include(e => e.Ploeg).Include(e => e.Event).Include(e => e.Persoon).FirstOrDefault();
            }
        }

        public static bool DeleteVerenigingById(int id)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                te.Vereniging.Remove(te.Vereniging.Where(c => c.Id == id).Include(e => e.Ploeg).Include(e => e.Event).Include(e => e.Persoon).FirstOrDefault());
                return te.SaveChanges() > 0;
            }
        }

        public static int AddVereniging(string afkorting, string naam)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                Vereniging vereniging = te.Vereniging.Add(new Vereniging()
                {
                    Afkorting = afkorting,
                    Naam = naam
                });
                te.SaveChanges();
                return vereniging.Id;
            }
        }
    }
}