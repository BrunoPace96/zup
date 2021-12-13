using System.ComponentModel.DataAnnotations;

namespace ZupTeste.Core
{
    public abstract partial class EntityBase
    {
        [Required]
        public DateTime CreatedAt { get; set; }
        
        [Required]
        public DateTime LastUpdatedAt { get; set; }

        public virtual void Created()
        {
            Id = new Guid();
            CreatedAt = LastUpdatedAt = DateTime.UtcNow;
        }

        public virtual void Updated() =>
            LastUpdatedAt = DateTime.UtcNow;
    }
}