using EventEchosAPI.Entities.Users;

namespace EventEchosAPI.Entities.Roles
{
    public class Guest: RoleCommon
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
