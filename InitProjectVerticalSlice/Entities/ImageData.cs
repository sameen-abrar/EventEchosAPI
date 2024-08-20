namespace EventEchosAPI.Entities
{
    public class ImageData:  AuditableEntity
    {
        public string? ImageId { get; set; }
        public string? UserId { get; set; }
        public string? ImageMetaData { get; set; }
        public string? ImageBase64 { get; set; }
        public string? ImageUrl { get; set; }
        public string? BitmapImage { get; set; }
        public int? ImagePoolId { get; set; }
        public ImagePool? ImagePool { get; set; }

        //public List<EventGuestImage> EventGuestImages { get; set; }
    }
}
