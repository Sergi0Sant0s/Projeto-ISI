using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DATA.Entities
{
    public class Event
    {
        public Event()
        {
            Teams = new List<Team>();
            Games = new List<Game>();
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventId { get; set; }
        [Required]
        [MaxLength(30,ErrorMessage = "EventName deve ter no maximo 30 caracteres")]
        public string EventName { get; set; }
        [Required]                                      
        public DateTime DateOfStart { get; set; }       
        [Required]                                      
        public DateTime DateOfEnd { get; set; }


        /* RELATIONSHIP */
        [JsonIgnore]
        public virtual ICollection<Team> Teams { get; set; }
        [JsonIgnore]
        public virtual ICollection<Game> Games { get; set; }
    }
}
