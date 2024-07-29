using System.ComponentModel.DataAnnotations;

namespace MediConnectHub.DTOS
{
    public class LoginUserDto
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string password { get; set; }

    }
}
