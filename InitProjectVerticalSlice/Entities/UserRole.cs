namespace EventEchosAPI.Entities
{
    public class UserRole : AuditableEntity
    {
        public string? RoleName { get; set; }
        public string? RoleDescription { get; set; }
        public string? RoleType { get; set; }

        public List<UserRolePermission> UserRolePermissions { get; set; }
    }
}
