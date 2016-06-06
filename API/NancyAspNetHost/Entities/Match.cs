using System;
using System.Collections.Generic;

namespace NancyAspNetHost.Entities
{
    public class Match
    {
        public string MatchId { get; set; }
        public string MatchName { get; set; }
        public DateTime MatchDate { get; set; }
        public List<Team> MatchTeams { get; set; }
    }
}