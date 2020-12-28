using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public class EventTeams
    {
        public Event Event { get; set; }
        public IEnumerable<Team> Teams{ get; set; }
    }
}
