using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace hogskoleprovet_api.Model
{
    public class Questions
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Opta { get; set; }
        public string? Optb { get; set; }
        public string? Optc { get; set; }
        public string? Optd { get; set; }
    }
}
