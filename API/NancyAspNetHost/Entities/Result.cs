namespace NancyAspNetHost.Entities
{
    public class Result
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ErrorField { get; set; }
        public string Data { get; set; }
    }
}