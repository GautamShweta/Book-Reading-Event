using MVCAssignment.Validation;
using System.ComponentModel.DataAnnotations;
namespace MVCAssignment.Models
    {
    public class RegisterUserModel
        {
        [UserValidator]
        public string UserName { get; set; }

        [UserValidator]
        [EmailAddress]
        public string EmailId { get; set; }

        [MinLength(5)]
        public string Password { get; set; }
        }
    }