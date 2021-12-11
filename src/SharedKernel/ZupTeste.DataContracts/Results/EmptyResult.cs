namespace ZupTeste.DataContracts.Results
{
    public record EmptyResult
    {
        private EmptyResult() {}
        public static EmptyResult Create() => new();
    }
}