namespace EventEchosAPI.Entities
{
    public class RoleCommon : AuditableEntity
    {
        public string? Name { get; set; }
        public string? Status { get; set; }
    }
}
