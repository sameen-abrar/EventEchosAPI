using EventEchosAPI.Entities.Common;

namespace EventEchosAPI.Entities.Roles
{
    public class RoleCommon: AuditableEntity
    {
        public string? Name { get; set; }
        public string? Status { get; set; }
    }
}
