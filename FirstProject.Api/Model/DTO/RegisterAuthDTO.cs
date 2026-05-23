using System.ComponentModel.DataAnnotations;

namespace FirstProject.Api.Model.DTO
{
    public class RegisterAuthDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName  { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string[] roles { get; set; }
    }
}
