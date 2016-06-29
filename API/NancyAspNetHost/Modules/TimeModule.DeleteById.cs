using NancyAspNetHost.Entities;
using Newtonsoft.Json;

namespace NancyAspNetHost.Modules
{
    public partial class TimeModule
    {
        private static string DeleteObject<T>(dynamic parameters, string ipaddress)
        {
            Login login;
            string tokenResult = ValidateToken(parameters.token, ipaddress, out login);
            if (tokenResult != null)
                return tokenResult;

            // get the data
            string id = parameters.id;
            int idint = 0;

            // validate the data
            bool success = true;
            string errormessage = "Ok";
            string errorfield = "";
            string data = "";

            if (string.IsNullOrWhiteSpace(id) || !int.TryParse(id, out idint))
            {
                success = false;
                errormessage = "Id is empty!";
                errorfield = "ID";
            }
            else
            {
                if (typeof(T).ToString() == typeof(Blok).ToString())
                {
                    data = DataAccess.DataAccess.DeleteBlokById(idint).ToString();
                }
                else if (typeof(T).ToString() == typeof(Deelnemer).ToString())
                {
                    data = DataAccess.DataAccess.DeleteDeelnemerById(idint).ToString();
                }
                else if (typeof(T).ToString() == typeof(Event).ToString())
                {
                    data = DataAccess.DataAccess.DeleteEventById(idint).ToString();
                }
                else if (typeof(T).ToString() == typeof(Persoon).ToString())
                {
                    data = DataAccess.DataAccess.DeletePersoonById(idint).ToString();
                }
                else if (typeof(T).ToString() == typeof(Ploeg).ToString())
                {
                    data = DataAccess.DataAccess.DeletePloegById(idint).ToString();
                }
                else if (typeof(T).ToString() == typeof(Veld).ToString())
                {
                    data = DataAccess.DataAccess.DeleteVeldById(idint).ToString();
                }
                else if (typeof(T).ToString() == typeof(Vereniging).ToString())
                {
                    data = DataAccess.DataAccess.DeleteVerenigingById(idint).ToString();
                }
            }

            // return result
            return GenerateResult(success, errormessage, data, errorfield);
        }
    }
}