using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserLoginApp.DTO.Interfaces;

namespace UserLoginApp.DTO.DTOs.UserDTOs
{
    public class UserRegisterDto:IDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

    }
}
