using Microsoft.EntityFrameworkCore;
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
    public class StatPlayerOnGame
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StatPlayerOnGameId { get; set; }        

        //STATS
        [Required]
        public int Kills { get; set; }
        [Required]
        public int Deaths { get; set; }
        [Required]
        public double ADR { get; set; }
        [Required]
        public double Rating { get; set; }



        /* RELATIONSHIP */

        //Game
        [Required]
        public int GameId { get; set; }
        [ForeignKey("GameId")]
        [JsonIgnore]
        public Game Game { get; set; }

        //PLAYER
        [Required]
        public int PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        [JsonIgnore]
        public Player Player { get; set; }

        //TEAM
        [Required]
        public int TeamId { get; set; }
        [ForeignKey("TeamId")]
        [JsonIgnore]
        public Team Team { get; set; }
    }
}
