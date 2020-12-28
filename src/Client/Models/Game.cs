using System;
using System.Collections.Generic;

#nullable disable

namespace Client.Models
{
    public partial class Game
    {
        public Game()
        {
            MapOfGames = new HashSet<MapOfGame>();
        }

        public int GameId { get; set; }
        public DateTime GameDate { get; set; }
        public int EventId { get; set; }
        public int TeamAid { get; set; }
        public int TeamBid { get; set; }
        public int? TeamWinnerId { get; set; }

        public virtual Event Event { get; set; }
        public virtual Team TeamA { get; set; }
        public virtual Team TeamB { get; set; }
        public virtual Team TeamWinner { get; set; }
        public virtual ICollection<MapOfGame> MapOfGames { get; set; }
    }
}
