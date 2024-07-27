using EventEchosAPI.Entities.Common;

namespace EventEchosAPI.Entities.Users
{
    public class UserRole: AuditableEntity
    {
        public string? RoleName { get; set; }
        public string? RoleDescription { get; set; }
        public string? RoleType { get; set; }

        public List<UserRolePermission> UserRolePermissions { get; set; }
    }
}
