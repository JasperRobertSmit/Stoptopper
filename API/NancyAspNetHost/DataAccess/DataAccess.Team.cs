using System.Collections.Generic;
using System.Linq;

namespace NancyAspNetHost.DataAccess
{
    public partial class DataAccess
    {
        public static bool AddTeam(string teamid, string teamname, int teamnumber)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                te.Team.Add(new Team()
                {
                    TeamId = teamid,
                    TeamName = teamname,
                    TeamNumber = teamnumber
                });
                return te.SaveChanges() > 0;
            }
        }

        public static List<Team> GetAllTeam()
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                return te.Team.ToList();
            }
        }
    }
}