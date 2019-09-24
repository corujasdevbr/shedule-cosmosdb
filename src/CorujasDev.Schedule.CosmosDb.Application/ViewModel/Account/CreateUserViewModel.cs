using System.ComponentModel.DataAnnotations;

namespace CorujasDev.Schedule.CosmosDb.Application.ViewModel.Account
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }
        public string Avatar { get; set; }

        [Required(ErrorMessage = "Email Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [StringLength(maximumLength: 10, MinimumLength = 6, ErrorMessage = "Password Invalid")]
        public string Password { get; set; }
    }
}
