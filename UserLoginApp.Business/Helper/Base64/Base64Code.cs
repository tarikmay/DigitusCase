using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLoginApp.Business.Helper.Base64
{
    public static class Base64Code
    {
        public static string Base64Encode(string plainText)
        {
            try
            {
                var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                return Convert.ToBase64String(plainTextBytes);
            }
            catch (Exception)
            {

                return null;
            }
           
        }
        public static string Base64Decode(string base64EncodedData)
        {
            try
            {
                var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
                return Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch (Exception)
            {

                return null;
            }
           
        }
    }
}
