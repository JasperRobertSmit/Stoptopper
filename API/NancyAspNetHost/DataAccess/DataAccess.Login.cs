using System;
using System.Linq;

namespace NancyAspNetHost.DataAccess
{
    public partial class DataAccess
    {
        public static bool UsernameExist(string username)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                Login login = te.Login.Where(t => t.Username == username).FirstOrDefault();
                return !(login == null || string.IsNullOrWhiteSpace(login.Username));
            }
        }

        public static Login GetLoginById(int id)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                return te.Login.Where(t => t.Id == id).FirstOrDefault();
            }
        }

        public static Login Login(string username, string password)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                Login login = te.Login.Where(t => t.Username == username && t.Password == password).FirstOrDefault();
                if (login == null || login.Id == 0)
                    return null;
                else
                    return login;
            }
        }

        public static Login Register(string username, string password, string email)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                Login login = te.Login.Add(new Login()
                {
                    Username = username,
                    Password = password,
                    Email = email != null ? email : null
                });
                te.SaveChanges();
                return login;
            }
        }
    }
}