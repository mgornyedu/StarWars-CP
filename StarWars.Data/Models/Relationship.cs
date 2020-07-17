using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StarWars.Data.Models
{
    public class Relationship
    {
        public int CharacterId { get; set; }
        [ForeignKey(nameof(CharacterId))]
        [JsonIgnore]
        public Character Character { get; set; }
        public int FriendId { get; set; }
        [ForeignKey(nameof(FriendId))]
        [JsonIgnore]
        public Character Friend { get; set; }
    }
}
