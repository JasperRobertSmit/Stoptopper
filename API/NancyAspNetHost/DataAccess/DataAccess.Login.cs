using System;
using System.Linq;

namespace NancyAspNetHost.DataAccess
{
    public partial class DataAccess
    {
        public static string GenerateToken(string username)
        {
            return Guid.NewGuid().ToString();
        }

        public static bool UsernameExist(string username)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                Login login = te.Login.Where(t => t.Username == username).FirstOrDefault();
                return !(login == null || string.IsNullOrWhiteSpace(login.Username));
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

        public static bool Register(string username, string password, string email)
        {
            using (TimechasersEntities te = new TimechasersEntities())
            {
                te.Login.Add(new Login()
                {
                    Username = username,
                    Password = password,
                    Email = email != null ? email : null
                });
                return te.SaveChanges() > 0;
            }
        }
    }
}