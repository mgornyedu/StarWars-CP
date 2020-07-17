using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StarWars.Data.Models
{
    public class Cast
    {
        public int EpisodeId { get; set; }
        [ForeignKey(nameof(EpisodeId))]
        [JsonIgnore]
        public Episode Episode { get; set; }
        public int CharacterId { get; set; }
        [ForeignKey(nameof(CharacterId))]
        [JsonIgnore]
        public Character Character { get; set; }
    }
}
