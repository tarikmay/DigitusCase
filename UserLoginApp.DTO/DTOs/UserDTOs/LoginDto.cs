using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserLoginApp.DTO.Interfaces;

namespace UserLoginApp.DTO.DTOs.UserDTOs
{
    public class LoginDto:IDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
