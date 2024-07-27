namespace EventEchosAPI.Contracts.Images
{
    public class ImageUploadRequest
    {
        public string? UserId { get; set; }
        public IFormFile Image { get; set; }
    }
}
