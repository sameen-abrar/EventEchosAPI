using EventEchosAPI.Entities.Common;
using EventEchosAPI.Entities.Roles;

namespace EventEchosAPI.Entities.Events
{
    public class EventCoodrinator: AuditableEntity
    {
        public int EventId { get; set; }
        public int HostId { get; set; }

        public Coordinator Coordinator { get; set; }
        public Event Event { get; set; }

        public EventCoodrinator()
        {
            Coordinator = new();
            Event = new();
        }
    }
}
