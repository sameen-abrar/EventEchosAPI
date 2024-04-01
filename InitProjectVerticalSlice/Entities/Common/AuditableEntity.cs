using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EventEchosAPI.Entities.Common
{
    public class AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        // For extra fields required in the future
        public string? Attribute1 { get; set; }
        public string? Attribute2 { get; set; }
        public string? Attribute3 { get; set; }
        public string? Attribute4 { get; set; }
        public string? Attribute5 { get; set; }
        public string? Attribute6 { get; set; }
    }
}
