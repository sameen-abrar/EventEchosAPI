using EventEchosAPI.Entities;

namespace EventEchosAPI.Entities
{
    public class Admin : RoleCommon
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public Admin()
        {
            User = new User();
        }

    }
}
