using System;
using System.Collections.Generic;

#nullable disable

namespace Client.Models
{
    public partial class Player
    {
        public Player()
        {
            StatPlayerOnMaps = new HashSet<StatPlayerOnMap>();
        }

        public int PlayerId { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }

        public virtual ICollection<StatPlayerOnMap> StatPlayerOnMaps { get; set; }
    }
}
