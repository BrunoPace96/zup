using ZupTeste.Core.Contracts;

namespace ZupTeste.Core
{
    public abstract partial class EntityBase : IEntityBase
    {
        public Guid Id { get; set; }

        public override int GetHashCode() => 
            Id.GetHashCode() ^ 31;
    }
}