using Client.DataAtributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Client.Models
{
    public partial class Event
    {
        public Event()
        {
            Games = new HashSet<Game>();
        }

        public int EventId { get; set; }
        [Required(ErrorMessage = "Obrigatório")]
        public string EventName { get; set; }
        [Required(ErrorMessage = "Obrigatório")]
        
        public DateTime DateOfStart { get; set; }
        [Required(ErrorMessage = "Obrigatório")]
        [DateLessThan("DateOfStart")]
        public DateTime DateOfEnd { get; set; }

        public virtual ICollection<Game> Games { get; set; }
        public virtual ICollection<Team> Teams { get; set; }

    }
}
