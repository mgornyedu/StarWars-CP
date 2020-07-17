using System;
using System.Collections.Generic;
using System.Text;

namespace StarWars.Data.Models
{
    public class V_Character : DBNamedModel
    {
        public string[] Friends { get; set; }
        public string[] Episodes { get; set; }
    }
}
