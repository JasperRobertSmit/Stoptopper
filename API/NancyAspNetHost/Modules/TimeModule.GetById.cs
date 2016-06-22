using Newtonsoft.Json;

namespace NancyAspNetHost.Modules
{
    public partial class TimeModule
    {
        private static string GetObject<T>(dynamic parameters, string ipaddress)
        {
            Login login;
            string tokenResult = ValidateToken(parameters.token, ipaddress, out login);
            if (tokenResult != null)
                return tokenResult;

            // get data
            string id = parameters.id;
            int idint = 0;

            // validate data
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
                    data = JsonConvert.SerializeObject(DataAccess.DataAccess.GetBlokById(idint));
                }
                else if (typeof(T).ToString() == typeof(Deelnemer).ToString())
                {
                    data = JsonConvert.SerializeObject(DataAccess.DataAccess.GetDeelnemerById(idint));
                }
                else if (typeof(T).ToString() == typeof(Event).ToString())
                {
                    data = JsonConvert.SerializeObject(DataAccess.DataAccess.GetEventById(idint));
                }
                else if (typeof(T).ToString() == typeof(Persoon).ToString())
                {
                    data = JsonConvert.SerializeObject(DataAccess.DataAccess.GetPersoonById(idint));
                }
                else if (typeof(T).ToString() == typeof(Ploeg).ToString())
                {
                    data = JsonConvert.SerializeObject(DataAccess.DataAccess.GetPloegById(idint));
                }
                else if (typeof(T).ToString() == typeof(Veld).ToString())
                {
                    data = JsonConvert.SerializeObject(DataAccess.DataAccess.GetVeldById(idint));
                }
                else if (typeof(T).ToString() == typeof(Vereniging).ToString())
                {
                    data = JsonConvert.SerializeObject(DataAccess.DataAccess.GetVerenigingById(idint));
                }
            }

            // return result
            return GenerateResult(success, errormessage, data, errorfield);
        }
    }
}