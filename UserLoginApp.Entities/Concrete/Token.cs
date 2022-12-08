using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLoginApp.Entities.Concrete
{
    public class Token
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }
        public string AccessToken { get; set;}
        public string Role { get; set; }
    }
}
