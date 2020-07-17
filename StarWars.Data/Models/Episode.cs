using System;
using System.Collections.Generic;
using System.Text;

namespace StarWars.Data.Models
{
    public class Episode : DBNamedModel
    {
        public ICollection<Cast> Casts { get; set; }
    }
}
