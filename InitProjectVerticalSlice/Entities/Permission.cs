namespace EventEchosAPI.Entities
{
    public class Permission : AuditableEntity
    {
        public string? PermissionName { get; set; }
        public string? PermissionDescription { get; set; }
        public string? PermissionType { get; set; }

        public List<UserRolePermission> UserRolePermissions { get; set; }

        public Permission()
        {
            UserRolePermissions = [];
        }
    }
}
