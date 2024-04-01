using EventEchosAPI.Entities.Common;

namespace EventEchosAPI.Entities.Events
{
    public class Event: AuditableEntity
    {
        public string EventCode { get; set; }
        public string? EventName { get; set; }
        public DateTime? EventDate { get; set; }
        public List<EventCoodrinator> EventCoodrinators { get; set; }
        public List<EventGuestImage> EventGuestImages { get; set; }

        public Event()
        {
            EventCoodrinators = [];
            EventGuestImages = [];
        }

        
    }
}
