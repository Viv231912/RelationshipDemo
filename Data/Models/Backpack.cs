using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;

namespace RelationshipDemo.Data.Models
{
    public class Backpack
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int CharacterId { get; set; }
        [JsonIgnore]
        public Character Character { get; set; }
    }
}
