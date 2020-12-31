using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Client.Models
{
    public partial class Team
    {
        public Team()
        {
            Players = new List<Player>();
        }

        public int TeamId { get; set; }
        [Required(ErrorMessage = "Obrigatório")]
        [MaxLength(30,ErrorMessage = "O nome da equipa deve ter no maximo 30 caracteres")]
        public string TeamName { get; set; }
        [Required(ErrorMessage = "Obrigatório")]
        public int TeamRanking { get; set; }
        [Required(ErrorMessage = "Obrigatório")]
        [MaxLength(30, ErrorMessage = "O nome da equipa deve ter no maximo 30 caracteres")]
        public string TeamNationality { get; set; }


        //Relationship
        public virtual ICollection<Player> Players { get; set; }
    }
}
