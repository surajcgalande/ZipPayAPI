namespace TestProject.WebAPI.Common
{
    public class ApiResponseFormat
    {
        public ApiResponseFormat(string type, int statusCode, string message = "", object data = null, bool error = false)
        {
            Status = new StatusResponseFormat(type, statusCode, message, error);
            Data = data;
        }

        public StatusResponseFormat Status { get; set; }
        public object Data { get; set; }


    }
}
