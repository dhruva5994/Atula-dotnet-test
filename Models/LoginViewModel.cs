using System.ComponentModel.DataAnnotations;

namespace PracticalDemo.Models
{
    public class LoginViewModel
    {
        [Key]
        public int LoginID { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
