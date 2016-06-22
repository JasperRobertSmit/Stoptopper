using System;

namespace NancyAspNetHost.Modules
{
    public partial class TimeModule
    {
        private static string SaveBlok(dynamic formdata, string ipaddress)
        {
            Login login;
            string tokenResult = ValidateToken(formdata["token"], ipaddress, out login);
            if (tokenResult != null)
                return tokenResult;

            // get data
            string startTijd = null;
            string eindTijd = null;
            string evenementId = null;
            string nummer = null;
            int evenementidint = 0;
            int nummerint = 0;
            if (formdata != null)
            {
                startTijd = formdata["starttijd"];
                eindTijd = formdata["eindtijd"];
                evenementId = formdata["evenementid"];
                nummer = formdata["nummer"];
            }

            // validate data
            bool success = true;
            string errormessage = "Ok";
            string errorfield = "";
            int blokid = 0;

            if (string.IsNullOrWhiteSpace(startTijd))
            {
                success = false;
                errormessage = "StartTijd is empty!";
                errorfield = "STARTTIJD";
            }
            else if (string.IsNullOrWhiteSpace(eindTijd))
            {
                success = false;
                errormessage = "EindTijd is empty!";
                errorfield = "EINDTIJD";
            }
            else if (string.IsNullOrWhiteSpace(evenementId) || !int.TryParse(evenementId, out evenementidint))
            {
                success = false;
                errormessage = "EvenementId is empty!";
                errorfield = "EVENEMENTID";
            }
            else if (string.IsNullOrWhiteSpace(nummer) || !int.TryParse(nummer, out nummerint))
            {
                success = false;
                errormessage = "Nummer is empty!";
                errorfield = "NUMMER";
            }
            else
            {
                DateTime starttijddatetime = ValidateDate(ref success, ref errormessage, ref errorfield, startTijd, "StartTijd is invalid!", "STARTTIJD");
                DateTime eindtijddatetime = ValidateDate(ref success, ref errormessage, ref errorfield, eindTijd, "EindTijd is invalid!", "EINDTIJD");

                if (starttijddatetime != DateTime.MinValue && eindtijddatetime != DateTime.MinValue)
                {
                    // add blok
                    blokid = DataAccess.DataAccess.AddBlok(eindtijddatetime, starttijddatetime, evenementidint, nummerint);
                }
            }

            // return result
            return GenerateResult(success, errormessage, success ? blokid.ToString() : "", errorfield);
        }

        private static string SaveDeelnemer(dynamic formdata, string ipaddress)
        {
            Login login;
            string tokenResult = ValidateToken(formdata["token"], ipaddress, out login);
            if (tokenResult != null)
                return tokenResult;

            // get data
            string persoonid = null;
            string ploegid = null;
            string nummer = null;
            int persoonidint = 0;
            int ploegidint = 0;
            int nummerint = 0;
            if (formdata != null)
            {
                persoonid = formdata["persoonid"];
                ploegid = formdata["ploegid"];
                nummer = formdata["nummer"];
            }

            // validate data
            bool success = true;
            string errormessage = "Ok";
            string errorfield = "";
            int deelnemerid = 0;

            if (string.IsNullOrWhiteSpace(persoonid) || !int.TryParse(persoonid, out persoonidint))
            {
                success = false;
                errormessage = "PersoonId is empty!";
                errorfield = "PERSOONID";
            }
            else if (string.IsNullOrWhiteSpace(ploegid) || !int.TryParse(ploegid, out ploegidint))
            {
                success = false;
                errormessage = "PloegId is empty!";
                errorfield = "PLOEGID";
            }
            else if (string.IsNullOrWhiteSpace(nummer) || !int.TryParse(nummer, out nummerint))
            {
                success = false;
                errormessage = "Nummer is empty!";
                errorfield = "NUMMER";
            }
            else
            {
                // add deelnemer
                deelnemerid = DataAccess.DataAccess.AddDeelnemer(persoonidint, ploegidint, nummerint);
            }

            // return result
            return GenerateResult(success, errormessage, success ? deelnemerid.ToString() : "", errorfield);
        }

        private static string SaveEvent(dynamic formdata, string ipaddress)
        {
            Login login;
            string tokenResult = ValidateToken(formdata["token"], ipaddress, out login);
            if (tokenResult != null)
                return tokenResult;

            // get data
            string afkorting = "";
            string einddatum = "";
            string startdatum = "";
            string guid = "";
            string afstand = "";
            string naam = "";
            string plaats = "";
            string organisatorid = "";
            int afstandint = 0;
            int organisatoridint = 0;
            if (formdata != null)
            {
                afkorting = formdata["afkorting"];
                einddatum = formdata["einddatum"];
                startdatum = formdata["startdatum"];
                guid = formdata["guid"];
                afstand = formdata["afstand"];
                naam = formdata["naam"];
                plaats = formdata["plaats"];
                organisatorid = formdata["organisatorid"];
            }

            // validate data
            bool success = true;
            string errormessage = "Ok";
            string errorfield = "";
            int eventid = 0;

            if (string.IsNullOrWhiteSpace(afkorting))
            {
                success = false;
                errormessage = "Afkorting is empty!";
                errorfield = "AFKORTING";
            }
            else if (string.IsNullOrWhiteSpace(einddatum))
            {
                success = false;
                errormessage = "EindDatum is empty!";
                errorfield = "EINDDATUM";
            }
            else if (string.IsNullOrWhiteSpace(startdatum))
            {
                success = false;
                errormessage = "StartDatum is empty!";
                errorfield = "STARTDATUM";
            }
            else if (string.IsNullOrWhiteSpace(guid))
            {
                success = false;
                errormessage = "Guid is empty!";
                errorfield = "GUID";
            }
            else if (string.IsNullOrWhiteSpace(afstand))
            {
                success = false;
                errormessage = "Afstand is empty!";
                errorfield = "AFSTAND";
            }
            else if (string.IsNullOrWhiteSpace(naam))
            {
                success = false;
                errormessage = "Naam is empty!";
                errorfield = "NAAM";
            }
            else if (string.IsNullOrWhiteSpace(plaats))
            {
                success = false;
                errormessage = "Plaats is empty!";
                errorfield = "PLAATS";
            }
            else if (string.IsNullOrWhiteSpace(organisatorid))
            {
                success = false;
                errormessage = "OrganisatorId is empty!";
                errorfield = "ORGANISATORID";
            }
            else
            {
                if (!int.TryParse(afstand, out afstandint))
                {
                    success = false;
                    errormessage = "Afstand is invalid!";
                    errorfield = "AFSTAND";
                }
                else if (!int.TryParse(organisatorid, out organisatoridint))
                {
                    success = false;
                    errormessage = "OrganisatorId is invalid!";
                    errorfield = "ORGANISATORID";
                }
                else
                {
                    DateTime starttijddatetime = ValidateDate(ref success, ref errormessage, ref errorfield, startdatum, "StartDatum is invalid!", "STARTDATUM");
                    DateTime eindtijddatetime = ValidateDate(ref success, ref errormessage, ref errorfield, einddatum, "EindDatum is invalid!", "EINDDATUM");

                    if (starttijddatetime != DateTime.MinValue && eindtijddatetime != DateTime.MinValue)
                    {
                        // add the event
                        eventid = DataAccess.DataAccess.AddEvent(afkorting, eindtijddatetime, starttijddatetime, guid, afstandint, naam, plaats, organisatoridint);
                    }
                }
            }

            // return result
            return GenerateResult(success, errormessage, success ? eventid.ToString() : "", errorfield);
        }

        private static string SavePersoon(dynamic formdata, string ipaddress)
        {
            Login login;
            string tokenResult = ValidateToken(formdata["token"], ipaddress, out login);
            if (tokenResult != null)
                return tokenResult;

            // get data
            string geboortedatum = null;
            string knrbid = null;
            string verenigingsid = null;
            string naam = null;
            int verenigingsidint = 0;
            if (formdata != null)
            {
                geboortedatum = formdata["geboortedatum"];
                knrbid = formdata["knrbid"];
                verenigingsid = formdata["verenigingsid"];
                naam = formdata["naam"];
            }

            // validate data
            bool success = true;
            string errormessage = "Ok";
            string errorfield = "";
            int persoonid = 0;

            if (string.IsNullOrWhiteSpace(geboortedatum))
            {
                success = false;
                errormessage = "GeboorteDatum is empty!";
                errorfield = "GEBOORTEDATUM";
            }
            else if (string.IsNullOrWhiteSpace(knrbid))
            {
                success = false;
                errormessage = "knrbid is empty!";
                errorfield = "KNRBID";
            }
            else if (string.IsNullOrWhiteSpace(verenigingsid) || !int.TryParse(verenigingsid, out verenigingsidint))
            {
                success = false;
                errormessage = "VerenigingsId is empty!";
                errorfield = "VERENIGINGSID";
            }
            else
            {
                DateTime geboortedatumdatetime = ValidateDate(ref success, ref errormessage, ref errorfield, geboortedatum, "GeboorteDatum is invalid!", "GEBOORTEDATUM");

                if (geboortedatumdatetime != DateTime.MinValue)
                {
                    // add the persoon
                    persoonid = DataAccess.DataAccess.AddPersoon(geboortedatumdatetime, knrbid, verenigingsidint, naam);
                }
            }

            // return result
            return GenerateResult(success, errormessage, success ? persoonid.ToString() : "", errorfield);
        }

        private static string SavePloeg(dynamic formdata, string ipaddress)
        {
            Login login;
            string tokenResult = ValidateToken(formdata["token"], ipaddress, out login);
            if (tokenResult != null)
                return tokenResult;

            // get data
            string bootnaam = null;
            string naam = null;
            string rugnummer = null;
            string veldid = null;
            string verenigingsid = null;
            int veldidint = 0;
            int verenigingsidint = 0;
            if (formdata != null)
            {
                bootnaam = formdata["bootnaam"];
                naam = formdata["naam"];
                rugnummer = formdata["rugnummer"];
                veldid = formdata["veldid"];
                verenigingsid = formdata["verenigingsid"];
            }

            // validate data
            bool success = true;
            string errormessage = "Ok";
            string errorfield = "";
            int ploegid = 0;

            if (string.IsNullOrWhiteSpace(bootnaam))
            {
                success = false;
                errormessage = "BootNaam is empty!";
                errorfield = "BOOTNAAM";
            }
            else if (string.IsNullOrWhiteSpace(naam))
            {
                success = false;
                errormessage = "Naam is empty!";
                errorfield = "NAAM";
            }
            else if (string.IsNullOrWhiteSpace(rugnummer))
            {
                success = false;
                errormessage = "RugNummer is empty!";
                errorfield = "RUGNUMMER";
            }
            else if (string.IsNullOrWhiteSpace(veldid) || !int.TryParse(veldid, out veldidint))
            {
                success = false;
                errormessage = "VeldId is empty!";
                errorfield = "VELDID";
            }
            else if (string.IsNullOrWhiteSpace(verenigingsid) || !int.TryParse(verenigingsid, out verenigingsidint))
            {
                success = false;
                errormessage = "VerenigingsId is empty!";
                errorfield = "VERENIGINGSID";
            }
            else
            {
                // add the ploeg
                ploegid = DataAccess.DataAccess.AddPloeg(bootnaam, naam, rugnummer, veldidint, verenigingsidint);
            }

            // return result
            return GenerateResult(success, errormessage, success ? ploegid.ToString() : "", errorfield);
        }

        private static string SaveTijd(dynamic formdata, string ipaddress)
        {
            Login login;
            string tokenResult = ValidateToken(formdata["token"], ipaddress, out login);
            if (tokenResult != null)
                return tokenResult;

            // get data
            string time = null;
            string ploegid = null;
            string blokid = null;
            int ploegidint = 0;
            int blokidint = 0;
            if (formdata != null)
            {
                time = formdata["time"];
                ploegid = formdata["ploegid"];
                blokid = formdata["blokid"];
            }

            // validate data
            bool success = true;
            string errormessage = "Ok";
            string errorfield = "";

            if (string.IsNullOrWhiteSpace(ploegid) || !int.TryParse(ploegid, out ploegidint))
            {
                success = false;
                errormessage = "PloegId is empty!";
                errorfield = "PLOEGID";
            }
            else if (string.IsNullOrWhiteSpace(time))
            {
                success = false;
                errormessage = "Time is empty!";
                errorfield = "TIME";
            }
            else if (string.IsNullOrWhiteSpace(blokid) || !int.TryParse(blokid, out blokidint))
            {
                success = false;
                errormessage = "BlokId is empty!";
                errorfield = "BLOKID";
            }
            else
            {
                DateTime datetime = ValidateDate(ref success, ref errormessage, ref errorfield, time, "Time is invalid!", "TIME");

                if (datetime != DateTime.MinValue)
                {
                    // add the time
                    DataAccess.DataAccess.AddTijd(ploegidint, blokidint, datetime);
                }
            }

            // return result
            return GenerateResult(success, errormessage, "", errorfield);
        }

        private static string SaveVeld(dynamic formdata, string ipaddress)
        {
            Login login;
            string tokenResult = ValidateToken(formdata["token"], ipaddress, out login);
            if (tokenResult != null)
                return tokenResult;

            // get data
            string afkorting = null;
            string naam = null;
            string blokid = null;
            int blokidint = 0;
            if (formdata != null)
            {
                afkorting = formdata["afkorting"];
                naam = formdata["naam"];
                blokid = formdata["blokid"];
            }

            // validate data
            bool success = true;
            string errormessage = "Ok";
            string errorfield = "";
            int veldid = 0;

            if (string.IsNullOrWhiteSpace(afkorting))
            {
                success = false;
                errormessage = "Afkorting is empty!";
                errorfield = "AFKORTING";
            }
            else if (string.IsNullOrWhiteSpace(naam))
            {
                success = false;
                errormessage = "Naam is empty!";
                errorfield = "NAAM";
            }
            else if (string.IsNullOrWhiteSpace(blokid) || !int.TryParse(blokid, out blokidint))
            {
                success = false;
                errormessage = "BlokId is empty!";
                errorfield = "BLOKID";
            }
            else
            {
                // add veld
                veldid = DataAccess.DataAccess.AddVeld(afkorting, naam, blokidint);
            }

            // return result
            return GenerateResult(success, errormessage, success ? veldid.ToString() : "", errorfield);
        }

        private static string SaveVereniging(dynamic formdata, string ipaddress)
        {
            Login login;
            string tokenResult = ValidateToken(formdata["token"], ipaddress, out login);
            if (tokenResult != null)
                return tokenResult;

            // get data
            string afkorting = null;
            string naam = null;
            if (formdata != null)
            {
                afkorting = formdata["afkorting"];
                naam = formdata["naam"];
            }

            // validate data
            bool success = true;
            string errormessage = "Ok";
            string errorfield = "";
            int verenigingid = 0;

            if (string.IsNullOrWhiteSpace(afkorting))
            {
                success = false;
                errormessage = "Afkorting is empty!";
                errorfield = "AFKORTING";
            }
            else if (string.IsNullOrWhiteSpace(naam))
            {
                success = false;
                errormessage = "Naam is empty!";
                errorfield = "NAAM";
            }
            else
            {
                // add the vereniging
                verenigingid = DataAccess.DataAccess.AddVereniging(afkorting, naam);
            }

            // return result
            return GenerateResult(success, errormessage, success ? verenigingid.ToString() : "", errorfield);
        }
    }
}