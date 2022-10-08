namespace TestProject.WebAPI.Common
{
    public class StatusResponseFormat
    {
        public StatusResponseFormat(string type, int code, string message, bool error)
        {
            Type = type;
            Code = code;
            Message = message;
            Error = error;
        }
        public string Type { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public bool Error { get; set; }
    }
}
