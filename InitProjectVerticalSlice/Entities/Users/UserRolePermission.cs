using EventEchosAPI.Entities.Common;

namespace EventEchosAPI.Entities.Users
{
    public class UserRolePermission: AuditableEntity
    {
        public int UserRoleId { get; set; }
        public int PermissionId  { get; set; }

        public UserRole UserRole { get; set; }
        public Permission Permission  { get; set; }

        public UserRolePermission()
        {
            this.UserRole = new();
            this.Permission = new();
        }
    }
}
