namespace EventEchosAPI.Entities
{
    public class Event : AuditableEntity
    {
        public string EventCode { get; set; }
        public string? EventName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? UserLimit { get; set; }
        public string? EventUrl { get; set; }

        //public List<EventCoodrinator> EventCoodrinators { get; set; }
        //public List<EventGuestImage> EventGuestImages { get; set; }

        //public Event()
        //{
        //    EventCoodrinators = [];
        //    EventGuestImages = [];
        //}


    }
}
