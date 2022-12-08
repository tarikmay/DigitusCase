using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserLoginApp.DataAccess.Interfaces;
using UserLoginApp.DTO.DTOs.UserDTOs;
using UserLoginApp.Entities.Concrete;
using UserLoginApp.Entities.Concrete.Reponse;
using UserLoginApp.Entities.Conrete;

namespace UserLoginApp.Business.Interfaces
{
    public interface IUserService : IGenericServiceMD<User>
    {
        Token Login(LoginDto loginDto);
        User Register(User user);
        ConfirmMailResponse ConfirmUser(string id);
        ForgotPasswordResponse ForgotPassword(ForgotPasswordDto forgotPasswordDto);
        NewPasswordResponse NewPassword(NewPasswordDto newPasswordDto);

        IEnumerable<User> GetRegisteredUsers();
        IEnumerable<User> GetRegisteredUsersByDay(int day);
        IEnumerable<User> GetNotConfirmedUsersByDay(int day);

    }
}
