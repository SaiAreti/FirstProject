using System.ComponentModel.DataAnnotations;

namespace FirstProject.Api.Model.DTO
{
    public class loginDetailsDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }    


    }
}
