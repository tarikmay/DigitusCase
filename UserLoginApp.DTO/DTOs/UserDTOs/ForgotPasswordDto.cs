
using UserLoginApp.DTO.Interfaces;

namespace UserLoginApp.DTO.DTOs.UserDTOs
{
    public class ForgotPasswordDto:IDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
