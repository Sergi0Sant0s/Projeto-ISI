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
        [ForeignKey("EventId")]
        [JsonIgnore]
        public Event Event { get; set; }

        //TEAM A
        [Required]
        public int TeamAId { get; set; }
        [ForeignKey("TeamAId")]
        [JsonIgnore]
        public Team TeamA { get; set; }

        //TEAM B
        [Required]
        public int TeamBId { get; set; }
        [ForeignKey("TeamBId")]
        [JsonIgnore]
        public Team TeamB { get; set; }

        //TEAM WINNER
        public int? TeamWinnerId { get; set; }
        [ForeignKey("TeamWinnerId")]
        [JsonIgnore]
        public Team TeamWinner { get; set; }


        //MAPS OF GAME
        [JsonIgnore]
        public virtual ICollection<MapOfGame> MapOfGame { get; set; }
        [JsonIgnore]
        public virtual ICollection<StatPlayerOnGame> StatPlayerOnGame { get; set; }
    }
}
