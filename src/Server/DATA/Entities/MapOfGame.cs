using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DATA.Entities
{
    public class MapOfGame
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MapOfGameId { get; set; }
        public int? TeamAResult { get; set; }
        public int? TeamBResult { get; set; }


        /* RELATIONSHIP */

        //MAP
        [Required]
        public int MapaId { get; set; }
        [ForeignKey("MapaId")]
        [JsonIgnore]
        public Map Mapa { get; set; }

        //GAME
        public int GameId { get; set; }
        [ForeignKey("GameId")]
        [JsonIgnore]
        public Game Game { get; set; }
    }
}
