namespace EventEchosAPI.Contracts.Events
{
    public class EventRequest
    {
        public string EventName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserLimit { get; set; }
        public string CreatedBy { get; set; }
    }
}
