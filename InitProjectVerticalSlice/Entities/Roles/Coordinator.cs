using EventEchosAPI.Entities.Users;

namespace EventEchosAPI.Entities.Roles
{
    public class Coordinator: RoleCommon
    {
        public bool IsAdmin { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public Coordinator()
        {
            User = new User();
        }
    }
}
