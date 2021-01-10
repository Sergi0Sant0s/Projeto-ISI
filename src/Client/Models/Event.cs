using Client.DataAtributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Client.Models
{
    public partial class Event
    {
        public Event()
        {
            Games = new List<Game>();
        }

        public int EventId { get; set; }
        [Required(ErrorMessage = "Obrigatório")]
        public string EventName { get; set; }
        [Required(ErrorMessage = "Obrigatório")]
        
        public DateTime DateOfStart { get; set; }
        [Required(ErrorMessage = "Obrigatório")]
        [DateLessThan("DateOfStart")]
        public DateTime DateOfEnd { get; set; }

        public virtual List<Game> Games { get; set; }
        public virtual List<Team> Teams { get; set; }

    }
}
