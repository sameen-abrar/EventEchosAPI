namespace EventEchosAPI.Entities
{
    public class Guest : RoleCommon
    {
        public int UserId { get; set; }
        public int? ImageCount { get; set; }
        public User User { get; set; }

        public Guest()
        {
            User = new User();
        }
    }
}
