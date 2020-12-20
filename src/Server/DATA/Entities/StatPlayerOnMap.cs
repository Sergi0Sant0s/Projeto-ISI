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
    public class StatPlayerOnMap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StatPlayerOnMapId { get; set; }        

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

        //PLAYER
        [Required]
        public int PlayerId { get; set; }
        [JsonIgnore]
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }

        //TEAM
        [Required]
        public int TeamId { get; set; }
        [JsonIgnore]
        [ForeignKey("TeamId")]
        public Team Team { get; set; }
    }
}
