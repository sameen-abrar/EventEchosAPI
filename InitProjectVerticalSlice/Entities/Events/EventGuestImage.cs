using EventEchosAPI.Entities.Common;
using EventEchosAPI.Entities.Roles;

namespace EventEchosAPI.Entities.Events
{
    public class EventGuestImage: AuditableEntity
    {
        public int EventId { get; set; }
        public int GuestId { get; set; }
        public int ImageDataId { get; set; }

        public Event Event { get; set; }
        public Guest Guest { get; set; }
        public ImageData ImageData { get; set; }

        public EventGuestImage()
        {
            Event = new();
            Guest = new();   
            ImageData = new();
        }
    }
}
