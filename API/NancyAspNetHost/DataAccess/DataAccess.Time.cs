using System;

namespace NancyAspNetHost.DataAccess
{
    public partial class DataAccess
    {
        public static bool AddTime(string team, DateTime time)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                te.Time.Add(new Time()
                {
                   Team = team,
                   Time1 = time
                });
                return te.SaveChanges() > 0;
            }
        }
    }
}