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
    }
}
