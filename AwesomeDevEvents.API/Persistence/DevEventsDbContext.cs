using AwesomeDevEvents.API.Entities;

namespace AwesomeDevEvents.API.Persistence
{
    public class DevEventsDbContext
    {
        public List <DevEvents> DevEvents { get; set; }

        public DevEventsDbContext()
        {
            DevEvents = new List<DevEvents>();
        }
    }
}
