using System;
using System.Collections.Generic;
using System.Text;

namespace StarWars.Data.Models
{
    public abstract class DBNamedModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
