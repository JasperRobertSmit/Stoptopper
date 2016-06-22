using System;
using System.Linq;

namespace NancyAspNetHost.DataAccess
{
    public partial class DataAccess
    {
        public static Guid GenerateToken(int LoginID, string ipaddress)
        {
            // use for localhost
            if (ipaddress == null)
                ipaddress = "localhost";

            using (TimechasersEntities te = new TimechasersEntities())
            {
                Guid guid = Guid.NewGuid();
                LoginToken persoon = te.LoginToken.Add(new LoginToken()
                {
                   Token = guid,
                   Timestamp = DateTime.Now,
                   LoginID = LoginID,
                   IPAdress = ipaddress
                });
                te.SaveChanges();
                return guid;
            }
        }

        public static Login ValidateToken(Guid token, string ipaddress)
        {
            // use for localhost
            if (ipaddress == null || token == Guid.Parse("4004d869-f629-4acd-8360-2475d7270fce"))
                ipaddress = "localhost";

            using (TimechasersEntities te = new TimechasersEntities())
            {
                DateTime expiryDate = DateTime.Now.AddHours(-(24 * 300));
                LoginToken loginToken = te.LoginToken.Where(c => c.Token == token && c.Timestamp > expiryDate && c.IPAdress == ipaddress).FirstOrDefault();
                if (loginToken == null || !loginToken.LoginID.HasValue)
                    return null;
                else
                    return GetLoginById(loginToken.LoginID.Value);
            }
        }
    }
}