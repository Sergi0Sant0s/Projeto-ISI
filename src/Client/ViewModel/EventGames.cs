using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public class EventGames
    {
        public Event Event { get; set; }
        public IEnumerable<Game> Games { get; set; }
    }
}
