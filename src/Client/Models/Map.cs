using System;
using System.Collections.Generic;

#nullable disable

namespace Client.Models
{
    public partial class Map
    {
        public Map()
        {
            Games = new List<Game>();
        }

        public int MapId { get; set; }
        public string Description { get; set; }

        public virtual List<Game> Games { get; set; }
    }
}
