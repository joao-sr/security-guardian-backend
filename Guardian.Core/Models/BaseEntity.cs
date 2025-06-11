
namespace Guardian.Domain.Models
{
    public class BaseEntity
    {
        // empty base entity that all others will inherit from
        public int Id { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string ModifiedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime DeletedDate { get; set; }
        public string? DeletedBy { get; set; }
        public bool isDeleted { get; set; }
    }
}
