using NancyAspNetHost.Entities;
using Newtonsoft.Json;

namespace NancyAspNetHost.Modules
{
    public partial class TimeModule
    {
        private static string GetEventsByGuid(dynamic parameters, string ipaddress)
        {
            Login login;
            string tokenResult = ValidateToken(parameters.token, ipaddress, out login);
            if (tokenResult != null)
                return tokenResult;

            // return result
            return GenerateResult(true, "", JsonConvert.SerializeObject(DataAccess.DataAccess.GetAllEventByGuid(parameters.guid)), "");
        }

        private static string GetEventsByLoginId(dynamic parameters, string ipaddress)
        {
            Login login;
            string tokenResult = ValidateToken(parameters.token, ipaddress, out login);
            if (tokenResult != null)
                return tokenResult;

            // return result
            return GenerateResult(true, "", JsonConvert.SerializeObject(DataAccess.DataAccess.GetAllEventByLoginId(login.Id)), "");
        }

        private static string GetBlokkenByEventId(dynamic parameters, string ipaddress)
        {
            Login login;
            string tokenResult = ValidateToken(parameters.token, ipaddress, out login);
            if (tokenResult != null)
                return tokenResult;

            int id;
            string idResult = ValidateId(parameters.id, out id);
            if (idResult != null)
                return idResult;

            // return result
            return GenerateResult(true, "", JsonConvert.SerializeObject(DataAccess.DataAccess.GetAllBlokByEventId(id)), "");
        }

        private static string GetVeldenByBlokId(dynamic parameters, string ipaddress)
        {
            Login login;
            string tokenResult = ValidateToken(parameters.token, ipaddress, out login);
            if (tokenResult != null)
                return tokenResult;

            int id;
            string idResult = ValidateId(parameters.id, out id);
            if (idResult != null)
                return idResult;

            // return result
            return GenerateResult(true, "", JsonConvert.SerializeObject(DataAccess.DataAccess.GetAllVeldByBlokId(id)), "");
        }

        private static string GetPloegenByVeldId(dynamic parameters, string ipaddress)
        {
            Login login;
            string tokenResult = ValidateToken(parameters.token, ipaddress, out login);
            if (tokenResult != null)
                return tokenResult;

            int id;
            string idResult = ValidateId(parameters.id, out id);
            if (idResult != null)
                return idResult;

            // return result
            return GenerateResult(true, "", JsonConvert.SerializeObject(DataAccess.DataAccess.GetAllPloegByVeldId(id)), "");
        }

        private static string GetPloegenByVerenigingId(dynamic parameters, string ipaddress)
        {
            Login login;
            string tokenResult = ValidateToken(parameters.token, ipaddress, out login);
            if (tokenResult != null)
                return tokenResult;

            int id;
            string idResult = ValidateId(parameters.id, out id);
            if (idResult != null)
                return idResult;

            // return result
            return GenerateResult(true, "", JsonConvert.SerializeObject(DataAccess.DataAccess.GetAllPloegByVerenigingId(id)), "");
        }

        private static string GetDeelnemersByPloegId(dynamic parameters, string ipaddress)
        {
            Login login;
            string tokenResult = ValidateToken(parameters.token, ipaddress, out login);
            if (tokenResult != null)
                return tokenResult;

            int id;
            string idResult = ValidateId(parameters.id, out id);
            if (idResult != null)
                return idResult;

            // return result
            return GenerateResult(true, "", JsonConvert.SerializeObject(DataAccess.DataAccess.GetAllDeelnemerByPloeg(id)), "");
        }

        private static string GetPersoonByDeelnemerId(dynamic parameters, string ipaddress)
        {
            Login login;
            string tokenResult = ValidateToken(parameters.token, ipaddress, out login);
            if (tokenResult != null)
                return tokenResult;

            int id;
            string idResult = ValidateId(parameters.id, out id);
            if (idResult != null)
                return idResult;

            // return result
            return GenerateResult(true, "", JsonConvert.SerializeObject(DataAccess.DataAccess.GetAllPersoonByDeelnemerId(id)), "");
        }

        private static string GetPersoonByVerenigingId(dynamic parameters, string ipaddress)
        {
            Login login;
            string tokenResult = ValidateToken(parameters.token, ipaddress, out login);
            if (tokenResult != null)
                return tokenResult;

            int id;
            string idResult = ValidateId(parameters.id, out id);
            if (idResult != null)
                return idResult;

            // return result
            return GenerateResult(true, "", JsonConvert.SerializeObject(DataAccess.DataAccess.GetAllPersoonByVerenigingId(id)), "");
        }

        private static string GetTijdByPloegId(dynamic parameters, string ipaddress)
        {
            Login login;
            string tokenResult = ValidateToken(parameters.token, ipaddress, out login);
            if (tokenResult != null)
                return tokenResult;

            int id;
            string idResult = ValidateId(parameters.id, out id);
            if (idResult != null)
                return idResult;

            // return result
            return GenerateResult(true, "", JsonConvert.SerializeObject(DataAccess.DataAccess.GetAllTijdByPloegId(id)), "");
        }

        private static string GetTijdByBlokId(dynamic parameters, string ipaddress)
        {
            Login login;
            string tokenResult = ValidateToken(parameters.token, ipaddress, out login);
            if (tokenResult != null)
                return tokenResult;

            int id;
            string idResult = ValidateId(parameters.id, out id);
            if (idResult != null)
                return idResult;

            // return result
            return GenerateResult(true, "", JsonConvert.SerializeObject(DataAccess.DataAccess.GetAllTijdByBlokId(id)), "");
        }

        private static string GetTijdByPloegIdBlokId(dynamic parameters, string ipaddress)
        {
            Login login;
            string tokenResult = ValidateToken(parameters.token, ipaddress, out login);
            if (tokenResult != null)
                return tokenResult;

            int ploegId;
            string ploegIdResult = ValidateId(parameters.ploegid, out ploegId);
            if (ploegIdResult != null)
                return ploegIdResult;

            int blokId;
            string blokIdResult = ValidateId(parameters.blokid, out blokId);
            if (blokIdResult != null)
                return blokIdResult;

            // return result
            return GenerateResult(true, "", JsonConvert.SerializeObject(DataAccess.DataAccess.GetAllTijdByPloegIdBlokId(ploegId, blokId)), "");
        }
    }
}