using System;
using System.Collections.Generic;

#nullable disable

namespace Client.Models
{
    public partial class Player
    {
        public Player()
        {
            StatPlayerOnMaps = new List<StatPlayerOnGame>();
        }

        public int PlayerId { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }

        //Social Network
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }

        public int? TeamId { get; set; }
        public Team Team { get; set; }

        //Stat Player On Map
        public virtual List<StatPlayerOnGame> StatPlayerOnMaps { get; set; }
    }
}
