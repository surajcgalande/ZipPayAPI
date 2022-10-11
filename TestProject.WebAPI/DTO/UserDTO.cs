using System.ComponentModel.DataAnnotations;

namespace TestProject.WebAPI.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The user name is required")]
        public string Name { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailId { get; set; }

        [DataType(DataType.Currency)]
        public double MonthlySalary { get; set; }

        [DataType(DataType.Currency)]
        public double MonthlyExpense { get; set; }
    }
}
