using System.ComponentModel.DataAnnotations;

namespace ZupTeste.Core
{
    public abstract partial class EntityBase
    {
        public bool IsDeleted { get; private set;  }
        public DateTime? DeletedAt { get; private set; }
        
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

        public virtual void Deleted()
        {
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
        }
    }
}