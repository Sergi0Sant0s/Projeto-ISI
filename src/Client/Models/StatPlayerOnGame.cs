using System;
using System.Collections.Generic;

#nullable disable

namespace Client.Models
{
    public partial class StatPlayerOnGame
    {
        public int StatPlayerOnGameId { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public double Adr { get; set; }
        public double Rating { get; set; }
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        public int GameId { get; set; }

        public virtual Game Game { get; set; }
        public virtual Player Player { get; set; }
        public virtual Team Team { get; set; }
    }
}
