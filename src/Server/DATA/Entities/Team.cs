using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace DATA.Entities
{
    public class Team
    {
        public Team()
        {
            Events = new List<Event>();
            GamesTeamA = new List<Game>();
            GamesTeamB = new List<Game>();
            GamesTeamWinner = new List<Game>();
            StatPlayerOnMap = new List<StatPlayerOnMap>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeamId { get; set; }
        [MaxLength(30, ErrorMessage = "Nome da equipa deve ter no maximo 30 caracteres")]
        public string TeamName { get; set; }
        public int TeamRanking { get; set; }
        [MaxLength(30, ErrorMessage = "Nacionalidade deve ter no maximo 30 caracteres")]
        public string TeamNationality { get; set; }


        //Relationship
        [JsonIgnore]
        public virtual ICollection<StatPlayerOnMap> StatPlayerOnMap { get; set; }
        [JsonIgnore]
        public virtual ICollection<Player> Players { get; set; }
        [JsonIgnore]
        public virtual ICollection<Event> Events { get; set; }
        [JsonIgnore]
        public virtual ICollection<Game> GamesTeamA { get; set; }
        [JsonIgnore]
        public virtual ICollection<Game> GamesTeamB { get; set; }
        [JsonIgnore]
        public virtual ICollection<Game> GamesTeamWinner { get; set; }
    }
}
