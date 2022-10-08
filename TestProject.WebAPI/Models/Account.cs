using System.ComponentModel.DataAnnotations;

namespace TestProject.WebAPI.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }

        [DataType(DataType.Currency)]
        public double Balance { get; set; }
        public int UserId { get; set; }

    }
}
