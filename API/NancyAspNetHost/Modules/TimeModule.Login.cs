namespace NancyAspNetHost.Modules
{
    public partial class TimeModule
    {
        private static string Register(dynamic formdata, string ipaddress)
        {
            // get the data
            string username = null;
            string password = null;
            string email = null;
            if (formdata != null)
            {
                username = formdata["username"];
                password = formdata["password"];
                email = formdata["email"];
            }

            // validate the data
            bool success = true;
            string errormessage = "Ok";
            string errorfield = "";
            Login login = null;

            if (string.IsNullOrWhiteSpace(username))
            {
                success = false;
                errormessage = "Username is empty!";
                errorfield = "USERNAME";
            }
            else if (string.IsNullOrWhiteSpace(password))
            {
                success = false;
                errormessage = "Password is empty!";
                errorfield = "PASSWORD";
            }
            else if (username.Length >= 255)
            {
                success = false;
                errormessage = "Username is too long!";
                errorfield = "USERNAME";
            }
            else if (password.Length >= 255)
            {
                success = false;
                errormessage = "Password is too long!";
                errorfield = "PASSWORD";
            }
            else
            {
                if (DataAccess.DataAccess.UsernameExist(username))
                {
                    success = false;
                    errormessage = "Username already exists!";
                    errorfield = "USERNAME";
                }
                else
                {
                    login = DataAccess.DataAccess.Register(username, password, email);
                }
            }

            // return result
            return GenerateResult(success, errormessage, success ? DataAccess.DataAccess.GenerateToken(login.Id, ipaddress).ToString() : "", errorfield);
        }

        private static string ValidateUsername(dynamic formdata)
        {
            // get the data
            bool success = true;
            string errormessage = "";
            string field = "";
            string username = null;
            if (formdata != null)
            {
                username = formdata["username"];
            }

            // validate the data
            if (string.IsNullOrWhiteSpace(username))
            {
                success = false;
                errormessage = "User is empty!";
                field = "USERNAME";
            }
            else if (!DataAccess.DataAccess.UsernameExist(username))
            {
                success = false;
                errormessage = "Username is not valid!";
                field = "USERNAME";
            }

            // return result
            return GenerateResult(success, errormessage, "", field);
        }

        private static string Login(dynamic formdata, string ipaddress)
        {
            // get the data
            string username = null;
            string password = null;
            if (formdata != null)
            {
                username = formdata["username"];
                password = formdata["password"]; ;
            }

            // validate the data
            bool success = true;
            string errormessage = "Ok";
            string errorfield = "";
            Login login = DataAccess.DataAccess.Login(username, password);
            if (login == null)
            {
                success = false;
                errormessage = "Invalid password!";
                errorfield = "PASSWORD";
            }

            // return result
            return GenerateResult(success, errormessage, success ? DataAccess.DataAccess.GenerateToken(login.Id, ipaddress).ToString() : "", errorfield);
        }
    }
}