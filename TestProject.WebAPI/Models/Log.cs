using System;
using System.ComponentModel.DataAnnotations;

namespace TestProject.WebAPI.Models
{
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public int Severity { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
