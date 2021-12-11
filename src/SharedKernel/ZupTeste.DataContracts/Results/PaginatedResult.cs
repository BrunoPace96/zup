namespace ZupTeste.DataContracts.Results
{
    public record PaginatedResult<TEntity>
    {
        public int CurrentPage { get; init; }
        public int TotalItems { get; init; }
        public int TotalPages { get; init; }
        public List<TEntity> Items { get; init; }
    }
}