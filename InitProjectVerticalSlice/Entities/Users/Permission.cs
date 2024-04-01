using EventEchosAPI.Entities.Common;

namespace EventEchosAPI.Entities.Users
{
    public class Permission : AuditableEntity
    {
        public string? PermissionName { get; set; }
        public string? PermissionDescription  { get; set; }
        public string? PermissionType { get; set; }
    }
}
