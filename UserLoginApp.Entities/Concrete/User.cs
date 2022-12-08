using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using UserLoginApp.Entities.Attributes;
using UserLoginApp.Entities.Interfaces;

namespace UserLoginApp.Entities.Conrete
{
    [BsonCollection("Users")]
    public class User : IEntity
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsConfirm { get; set; } = false;
        public DateTime ConfirmDate { get; set; }
        public string Role { get; set; } = "User";
        public DateTime CreatedAt => ObjectId.Parse(Id).CreationTime;
    };


    
}
