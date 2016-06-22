using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System;

namespace NancyAspNetHost.DataAccess
{
    public partial class DataAccess
    {
        public static List<Event> GetAllEvent()
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                return te.Event.ToList();
            }
        }

        public static List<Event> GetAllEventByLoginId(int loginid)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                return te.Event.Where(c => c.LoginID == loginid).ToList();
            }
        }

        public static Event GetEventById(int id)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                return te.Event.Where(c => c.Id == id).FirstOrDefault();
            }
        }

        public static bool DeleteEventById(int id)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                te.Event.Remove(te.Event.Where(c => c.Id == id).FirstOrDefault());
                return te.SaveChanges() > 0;
            }
        }
        
        public static Event GetEventByGuid(string eventGuid)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                return te.Event.Where(t => t.EventGuid == eventGuid).FirstOrDefault();
            }
        }

        public static int AddEvent(string afkorting, DateTime eindDatum, DateTime startDatum, string guid, int afstand, string naam, string plaats, int organisatorID)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                Event sportevent = te.Event.Add(new Event()
                {
                    Afkorting = afkorting,
                    EindDatum = eindDatum,
                    StartDatum = startDatum,
                    Afstand = afstand,
                    Naam = naam,
                    Plaats = plaats,
                    OrganisatorID = organisatorID,
                    EventGuid = guid
                });
                te.SaveChanges();
                return sportevent.Id;
            }
        }
    }
}