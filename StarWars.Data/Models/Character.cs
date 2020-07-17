using System;
using System.Collections.Generic;
using System.Text;

namespace StarWars.Data.Models
{
    public class Character : DBNamedModel
    {
        public ICollection<Cast> Casts { get; set; }
        public ICollection<Relationship> Relationships { get; set; }
    }
}
