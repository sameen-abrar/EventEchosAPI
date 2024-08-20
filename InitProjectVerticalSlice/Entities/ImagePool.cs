namespace EventEchosAPI.Entities
{
    public class ImagePool:AuditableEntity
    {
        public string? Code { get; set; }
        public int? EventId { get; set; }
        public Event? Event { get; set; }
        public List<ImageData>? ImageDatas { get; set; }
    }
}
