using Nancy;
using NancyAspNetHost.Entities;
using Newtonsoft.Json;
using System;

namespace NancyAspNetHost.Modules
{
    public partial class TimeModule : NancyModule
    {
        public TimeModule()
        {
            StaticConfiguration.DisableErrorTraces = false;

            /* USER */
            Post["/Register"] = (parameters) =>
            {
                return Register(Request.Form, Request.UserHostAddress);
            };
            Post["/ValidateUsername"] = (parameters) =>
            {
                return ValidateUsername(Request.Form);
            };
            Post["/Login"] = (parameters) =>
            {
                return Login(Request.Form, Request.UserHostAddress);
            };

            /* SAVE */
            Post["/blok"] = (parameters) =>
            {
                return SaveBlok(Request.Form, Request.UserHostAddress);
            };
            Post["/deelnemer"] = (parameters) =>
            {
                return SaveDeelnemer(Request.Form, Request.UserHostAddress);
            };
            Post["/event"] = (parameters) =>
            {
                return SaveEvent(Request.Form, Request.UserHostAddress);
            };
            Post["/persoon"] = (parameters) =>
            {
                return SavePersoon(Request.Form, Request.UserHostAddress);
            };
            Post["/ploeg"] = (parameters) =>
            {
                return SavePloeg(Request.Form, Request.UserHostAddress);
            };
            Post["/SaveTijd"] = (parameters) =>
            {
                return SaveTijd(Request.Form, Request.UserHostAddress);
            };
            Post["/veld"] = (parameters) =>
            {
                return SaveVeld(Request.Form, Request.UserHostAddress);
            };
            Post["/vereniging"] = (parameters) =>
            {
                return SaveVereniging(Request.Form, Request.UserHostAddress);
            };

            /* GET */
            Get["/blok/{id}/{token}"] = (parameters) =>
            {
                return GetObject<Blok>(parameters, Request.UserHostAddress);
            };
            Get["/deelnemer/{id}/{token}"] = (parameters) =>
            {
                return GetObject<Deelnemer>(parameters, Request.UserHostAddress);
            };
            Get["/event/{id}/{token}"] = (parameters) =>
            {
                return GetObject<Event>(parameters, Request.UserHostAddress);
            };
            Get["/persoon/{id}/{token}"] = (parameters) =>
            {
                return GetObject<Persoon>(parameters, Request.UserHostAddress);
            };
            Get["/ploeg/{id}/{token}"] = (parameters) =>
            {
                return GetObject<Ploeg>(parameters, Request.UserHostAddress);
            };
            Get["/veld/{id}/{token}"] = (parameters) =>
            {
                return GetObject<Veld>(parameters, Request.UserHostAddress);
            };
            Get["/vereniging/{id}/{token}"] = (parameters) =>
            {
                return GetObject<Vereniging>(parameters, Request.UserHostAddress);
            };

            /* GET ALL */
            Get["/event/all/user/{token}"] = (parameters) =>
            {
                return GetEventsByLoginId(parameters, Request.UserHostAddress);
            };
            Get["/blok/all/event/{id}/{token}"] = (parameters) =>
            {
                return GetBlokkenByEventId(parameters, Request.UserHostAddress);
            };
            Get["/veld/all/blok/{id}/{token}"] = (parameters) =>
            {
                return GetVeldenByBlokId(parameters, Request.UserHostAddress);
            };
            Get["/ploeg/all/veld/{id}/{token}"] = (parameters) =>
            {
                return GetPloegenByVeldId(parameters, Request.UserHostAddress);
            };
            Get["/ploeg/all/vereniging/{id}/{token}"] = (parameters) =>
            {
                return GetPloegenByVerenigingId(parameters, Request.UserHostAddress);
            };
            Get["/deelnemer/all/ploeg/{id}/{token}"] = (parameters) =>
            {
                return GetDeelnemersByPloegId(parameters, Request.UserHostAddress);
            };
            Get["/persoon/all/deelnemer/{id}/{token}"] = (parameters) =>
            {
                return GetPersoonByDeelnemerId(parameters, Request.UserHostAddress);
            };
            Get["/persoon/all/vereniging/{id}/{token}"] = (parameters) =>
            {
                return GetPersoonByVerenigingId(parameters, Request.UserHostAddress);
            };
            Get["/tijd/all/ploeg/{id}/{token}"] = (parameters) =>
            {
                return GetTijdByPloegId(parameters, Request.UserHostAddress);
            };
            Get["/tijd/all/blok/{id}/{token}"] = (parameters) =>
            {
                return GetTijdByBlokId(parameters, Request.UserHostAddress);
            };
            Get["/tijd/all/ploeg/blok/{ploegid}/{blokid}/{token}"] = (parameters) =>
            {
                return GetTijdByPloegIdBlokId(parameters, Request.UserHostAddress);
            };

            /* DELETE */
            Delete["/blok/{id}/{token}"] = (parameters) =>
            {
                return DeleteObject<Blok>(parameters, Request.UserHostAddress);
            };
            Delete["/deelnemer/{id}/{token}"] = (parameters) =>
            {
                return DeleteObject<Deelnemer>(parameters, Request.UserHostAddress);
            };
            Delete["/event/{id}/{token}"] = (parameters) =>
            {
                return DeleteObject<Event>(parameters, Request.UserHostAddress);
            };
            Delete["/persoon/{id}/{token}"] = (parameters) =>
            {
                return DeleteObject<Persoon>(parameters, Request.UserHostAddress);
            };
            Delete["/ploeg/{id}/{token}"] = (parameters) =>
            {
                return DeleteObject<Ploeg>(parameters, Request.UserHostAddress);
            };
            Delete["/veld/{id}/{token}"] = (parameters) =>
            {
                return DeleteObject<Veld>(parameters, Request.UserHostAddress);
            };
            Delete["/vereniging/{id}/{token}"] = (parameters) =>
            {
                return DeleteObject<Vereniging>(parameters, Request.UserHostAddress);
            };
        }
        
        private static string ValidateToken(string token, string ipaddress, out Login login)
        {
            // validate the token
            bool tokenValid = true;
            if (string.IsNullOrWhiteSpace(token))
            {
                tokenValid = false;
            }
            Guid tokenGuid;
            if(!Guid.TryParse(token, out tokenGuid))
            {
                tokenValid = false;
            }
            if (tokenGuid != null && tokenGuid != Guid.Empty)
            {
                login = DataAccess.DataAccess.ValidateToken(tokenGuid, ipaddress);
            }
            else
            {
                login = null;
            }
            if(login == null)
            {
                tokenValid = false;
            }

            if (!tokenValid)
            {
                return GenerateResult(false, "Token is invalid!", "", "TOKEN");
            }
            return null;
        }

        private static string ValidateId(string idstring, out int id)
        {
            // validate the id
            bool idvalid = true;
            if (string.IsNullOrWhiteSpace(idstring))
            {
                idvalid = false;
            }

            if (!int.TryParse(idstring, out id))
            {
                idvalid = false;
                id = 0;
            }

            if (!idvalid)
            {
                return GenerateResult(false, "Id is invalid!", "", "ID");
            }
            return null;
        }

        private static DateTime ValidateDate(ref bool success, ref string errormessage, ref string errorfield, string ticks, string errmessage, string errfield)
        {
            try
            {
                return new DateTime(long.Parse(ticks));
            }
            catch
            {
                success = false;
                errormessage = errmessage;
                errorfield = errfield;
                return DateTime.MinValue;
            }
        }

        private static string GenerateResult(bool success, string error, string data, string errorfield)
        {
            Result result = new Result();
            result.Success = success;
            result.Message = error;
            result.Data = data;
            result.ErrorField = errorfield;
            return JsonConvert.SerializeObject(result);
        }
    }
}