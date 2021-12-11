namespace ZupTeste.DataContracts.Results
{
    public record ValidationErrorResult
    {
        public string Field { get; init; }
        public IList<string> Messages { get; init; }
    }
}