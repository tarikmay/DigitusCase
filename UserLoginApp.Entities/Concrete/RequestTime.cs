using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using UserLoginApp.Entities.Attributes;
using UserLoginApp.Entities.Interfaces;

namespace UserLoginApp.Entities.Concrete
{
    [BsonCollection("RequestTimes")]
    public class RequestTime : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string RequestUrl { get; set; }
        public double RequestTimeMs { get; set; }
        public DateTime RequestDate => ObjectId.Parse(Id).CreationTime;

    }
}
