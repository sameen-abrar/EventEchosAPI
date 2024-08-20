namespace EventEchosAPI.Entities
{
    public class UserRolePermission : AuditableEntity
    {
        public int UserRoleId { get; set; }
        public int PermissionId { get; set; }

        public UserRole UserRole { get; set; }
        public Permission Permission { get; set; }

        public UserRolePermission()
        {
            UserRole = new();
            Permission = new();
        }
    }
}
