using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Event
{
    class EventRepository
    {
        Task<Event> NewEventAsync(string name, string description);
    }
}
