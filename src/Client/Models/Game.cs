using System;
using System.Collections.Generic;

#nullable disable

namespace Client.Models
{
    public partial class Game
    {
        public Game()
        {
            MapOfGames = new List<MapOfGame>();
        }

        public int GameId { get; set; }
        public DateTime GameDate { get; set; }
        public int EventId { get; set; }
        public int TeamAId { get; set; }
        public int TeamBId { get; set; }
        public int? TeamWinnerId { get; set; }

        public Event Event { get; set; }
        public Team TeamA { get; set; }
        public Team TeamB { get; set; }
        public Team? TeamWinner { get; set; }
        public virtual List<MapOfGame> MapOfGames { get; set; }
        public virtual List<StatPlayerOnGame> StatPlayerOnGame { get; set; }
    }
}
