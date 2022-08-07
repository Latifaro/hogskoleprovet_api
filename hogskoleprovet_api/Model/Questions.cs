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
        [BsonElement("name")]
        public string? Name { get; set; }
        public string? opta { get; set; }
        public string? optb { get; set; }
        public string? optc { get; set; }
        public string? optd { get; set; }
    }
}
