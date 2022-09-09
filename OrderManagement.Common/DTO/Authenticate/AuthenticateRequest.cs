using System.ComponentModel.DataAnnotations;

namespace OrderManagement.Common.DTO.Authenticate
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
