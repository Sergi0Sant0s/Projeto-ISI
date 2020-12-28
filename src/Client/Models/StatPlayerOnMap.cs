using System;
using System.Collections.Generic;

#nullable disable

namespace Client.Models
{
    public partial class StatPlayerOnMap
    {
        public int StatPlayerOnMapId { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public double Adr { get; set; }
        public double Rating { get; set; }
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        public int? MapOfGameId { get; set; }

        public virtual MapOfGame MapOfGame { get; set; }
        public virtual Player Player { get; set; }
        public virtual Team Team { get; set; }
    }
}
