using EventEchosAPI.Entities.Common;
using EventEchosAPI.Entities.Users;

namespace EventEchosAPI.Entities.Roles
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
