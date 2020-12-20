using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DATA.Entities
{
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GameId { get; set; }

        [Required]
        public DateTime GameDate { get; set; }


        /* RELATIONSHIP */

        //EVENT
        [Required]
        public int EventId { get; set; }
        [JsonIgnore]
        [ForeignKey("EventId")]
        public Event Event { get; set; }

        //TEAM A
        [Required]
        public int TeamAId { get; set; }
        [JsonIgnore]
        [ForeignKey("TeamAId")]
        public Team TeamA { get; set; }

        //TEAM B
        [Required]
        public int TeamBId { get; set; }
        [JsonIgnore]
        [ForeignKey("TeamBId")]
        public Team TeamB { get; set; }

        //TEAM WINNER
        public int? TeamWinnerId { get; set; }
        [JsonIgnore]
        [ForeignKey("TeamWinnerId")]
        public Team? TeamWinner { get; set; }


        //MAPS OF GAME
        [JsonIgnore]
        public virtual ICollection<MapOfGame> MapOfGame { get; set; }
    }
}
