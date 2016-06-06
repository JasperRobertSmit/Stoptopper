using Nancy;
using NancyAspNetHost.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace NancyAspNetHost.Modules
{
    public class TimeModule : NancyModule
    {
        public TimeModule()
        {
            StaticConfiguration.DisableErrorTraces = false;

            Team team1 = new Team();
            team1.TeamId = Guid.NewGuid().ToString();
            team1.TeamName = "Team 1";
            team1.TeamNumber = 1;

            Team team2 = new Team();
            team2.TeamId = Guid.NewGuid().ToString();
            team2.TeamName = "Team 2";
            team2.TeamNumber = 2;

            Match m = new Match();
            m.MatchDate = DateTime.Now.AddDays(-1);
            m.MatchId = Guid.NewGuid().ToString();
            m.MatchName = "Speedrace 1";
            m.MatchTeams = new List<Team>() { team1, team2 };

            Match m2 = new Match();
            m2.MatchDate = DateTime.Now;
            m2.MatchId = Guid.NewGuid().ToString();
            m2.MatchName = "Speedrace 2";
            m2.MatchTeams = new List<Team>() { team1, team2 };

            MatchResult mr = new MatchResult();
            mr.MatchId = m.MatchId;
            mr.MatchResultId = Guid.NewGuid().ToString();
            mr.MatchTimes = new Dictionary<string, double>();
            mr.MatchTimes.Add(team1.TeamId, 11.45);
            mr.MatchTimes.Add(team2.TeamId, 12.45);

            MatchResult mr2 = new MatchResult();
            mr2.MatchId = m2.MatchId;
            mr2.MatchResultId = Guid.NewGuid().ToString();
            mr2.MatchTimes = new Dictionary<string, double>();
            mr2.MatchTimes.Add(team1.TeamId, 41.45);
            mr2.MatchTimes.Add(team2.TeamId, 42.45);

            // register
            Post["/Register"] = (parameters) =>
            {
                return Register(Request.Form);
            };

            // login
            Post["/Login"] = (parameters) =>
            {
                return Login(Request.Form);
            };

            // time
            Post["/SaveTime"] = (parameters) =>
            {
                return SaveTime(Request.Form);
            };

            // save team
            Post["/SaveTeam"] = (parameters) =>
            {
                return SaveTeam(Request.Form);
            };

            // get team
            Get["/GetTeams"] = (parameters) =>
            {
                return GetTeams();
            };

            Get["/GetMatches"] = parameters => JsonConvert.SerializeObject(new List<Match>() { m, m2 });
            Get["/GetMatchResults"] = parameters => JsonConvert.SerializeObject(new List<MatchResult>() { mr, mr2 });
        }

        private static dynamic SaveTeam(dynamic formdata)
        {
            string teamname = null;
            string teamnumber = null;
            string teamid = Guid.NewGuid().ToString();
            if (formdata != null)
            {
                teamname = formdata["teamname"];
                teamnumber = formdata["teamnumber"];
            }

            // register
            bool success = true;
            string errormessage = "Ok";
            string errorfield = "";
            if (string.IsNullOrWhiteSpace(teamname))
            {
                success = false;
                errormessage = "TeamName is empty!";
                errorfield = "TEAMNAME";
            }
            else if (string.IsNullOrWhiteSpace(teamnumber))
            {
                success = false;
                errormessage = "TeamNumber is empty!";
                errorfield = "TEAMNUMBER";
            }
            else
            {
                try
                {
                    DataAccess.DataAccess.AddTeam(teamid, teamname, Convert.ToInt32(teamnumber));
                }
                catch
                {
                    success = false;
                    errormessage = "TeamNumber is invalid!";
                    errorfield = "TEAMNUMBER";
                }
            }

            // return result
            Result result = new Result();
            result.Success = success;
            result.Message = errormessage;
            result.Data = success ? teamid : "";
            result.Field = errorfield;
            return JsonConvert.SerializeObject(result);
        }

        private static dynamic GetTeams()
        {
            Result result = new Result();
            result.Success = true;
            result.Message = "Ok";
            result.Data = JsonConvert.SerializeObject(DataAccess.DataAccess.GetAllTeam());
            return JsonConvert.SerializeObject(result);
        }

        private static dynamic Register(dynamic formdata)
        {
            string username = null;
            string password = null;
            string email = null;
            if (formdata != null)
            {
                username = formdata["username"];
                password = formdata["password"];
                email = formdata["email"];
            }

            // register
            bool success = true;
            string errormessage = "Ok";
            string errorfield = "";
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
                    DataAccess.DataAccess.Register(username, password, email);
                }
            }

            // return result
            Result result = new Result();
            result.Success = success;
            result.Message = errormessage;
            result.Data = result.Success ? DataAccess.DataAccess.GenerateToken(username) : "";
            result.Field = errorfield;
            return JsonConvert.SerializeObject(result);
        }

        private static dynamic Login(dynamic formdata)
        {
            string username = null;
            string password = null;
            if (formdata != null)
            {
                username = formdata["username"];
                password = formdata["password"]; ;
            }

            // login
            bool success = true;
            string errormessage = "Ok";
            string errorfield = "";
            if (DataAccess.DataAccess.Login(username, password) == null)
            {
                success = false;
                errormessage = "Invalid credentials!";
                errorfield = "USERNAME|PASSWORD";
            }

            // return result
            Result result = new Result();
            result.Success = success;
            result.Message = errormessage;
            result.Data = result.Success ? DataAccess.DataAccess.GenerateToken(username) : "";
            result.Field = errorfield;
            return JsonConvert.SerializeObject(result);
        }

        private static dynamic SaveTime(dynamic formdata)
        {
            string time = null;
            string team = null;
            if (formdata != null)
            {
                time = formdata["time"];
                team = formdata["team"];
            }

            // save the time
            bool success = true;
            string errormessage = "Ok";
            string errorfield = "";
            if (string.IsNullOrWhiteSpace(team))
            {
                success = false;
                errormessage = "Team is empty!";
                errorfield = "TEAM";
            }
            else if (string.IsNullOrWhiteSpace(time))
            {
                success = false;
                errormessage = "Time is empty!";
                errorfield = "TIME";
            }
            else
            {
                try
                {
                    DateTime datetime = new DateTime(long.Parse(time));
                    DataAccess.DataAccess.AddTime(team, datetime);
                }
                catch
                {
                    success = false;
                    errormessage = "Time is invalid!";
                    errorfield = "TIME";
                }
            }

            // return result
            Result result = new Result();
            result.Success = success;
            result.Message = errormessage;
            result.Data = "";
            result.Field = errorfield;
            return JsonConvert.SerializeObject(result);
        }
    }
}