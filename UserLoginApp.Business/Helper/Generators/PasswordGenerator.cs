using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLoginApp.Business.Helper.Generators
{
    public static  class PasswordGenerator
    {
        public static string GenerateNewPasswordCode(int length)
        {
            char[] Chars = new char[] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
            string _password = string.Empty;
            Random Random = new Random();

            for (int a = 0; a < length; a++)
            {
                _password += Chars[Random.Next(0, Chars.Length)];
            };
            return (_password);
        }
    }
}
