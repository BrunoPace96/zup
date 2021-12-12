using System.ComponentModel.DataAnnotations;

namespace ZupTeste.Core
{
    public abstract partial class EntityBase
    {
        [Required]
        public DateTime CreatedAt { get; private set; }
        
        [Required]
        public DateTime LastUpdatedAt { get; private set; }

        public virtual void Created()
        {
            Id = new Guid();
            CreatedAt = LastUpdatedAt = DateTime.UtcNow;
        }

        public virtual void Updated() =>
            LastUpdatedAt = DateTime.UtcNow;
    }
}