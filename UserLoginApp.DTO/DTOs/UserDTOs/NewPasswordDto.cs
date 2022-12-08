using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserLoginApp.DTO.Interfaces;

namespace UserLoginApp.DTO.DTOs.UserDTOs
{
    public class NewPasswordDto:IDto
    {
        public string Username { get; set; }
        public string NewPassword { get; set; }
        public string PasswordCode { get; set; }
    }
}
