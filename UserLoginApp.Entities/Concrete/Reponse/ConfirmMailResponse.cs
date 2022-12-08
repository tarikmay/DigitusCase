
using UserLoginApp.Entities.Conrete;

namespace UserLoginApp.Entities.Concrete.Reponse
{
    public class ConfirmMailResponse:User
    {
        private DateTime _createdAt;
        public ConfirmMailResponse()
        {
            _createdAt = CreatedAt;
        }
        public bool Error { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt =>Error ? _createdAt : DateTime.MinValue;
    }
}
