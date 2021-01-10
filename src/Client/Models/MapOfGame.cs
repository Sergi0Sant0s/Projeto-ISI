using System;
using System.Collections.Generic;

#nullable disable

namespace Client.Models
{
    public partial class MapOfGame
    {
        public int MapOfGameId { get; set; }
        public int? TeamAresult { get; set; }
        public int? TeamBresult { get; set; }
        public int MapaId { get; set; }
        public int GameId { get; set; }

        public virtual Game Game { get; set; }
        public virtual Map Mapa { get; set; }
    }
}
