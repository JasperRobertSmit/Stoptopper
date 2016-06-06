using System.Collections.Generic;

namespace NancyAspNetHost.Entities
{
    public class MatchResult
    {
        public string MatchResultId { get; set; }
        public string MatchId { get; set; }
        public Dictionary<string, double> MatchTimes { get; set; }
    }
}