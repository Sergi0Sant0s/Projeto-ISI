using System;
using System.Collections.Generic;

#nullable disable

namespace Client.Models
{
    public partial class Map
    {
        public Map()
        {
            MapOfGames = new HashSet<MapOfGame>();
        }

        public int MapId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<MapOfGame> MapOfGames { get; set; }
    }
}
