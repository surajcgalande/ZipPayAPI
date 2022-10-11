using System;

namespace TestProject.WebAPI.DTO
{
    public class LogDTO
    {
        public int Id { get; set; }
        public int Severity { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
