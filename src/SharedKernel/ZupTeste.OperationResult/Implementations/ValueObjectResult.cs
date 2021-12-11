namespace ZupTeste.OperationResult.Implementations
{
    public sealed class ValueObjectResult<TResult> : 
        Result<TResult, List<string>>
    {
        private ValueObjectResult(
            bool success,
            TResult value,
            List<string> errors
        ) : base(success, value, errors) {}

        private static ValueObjectResult<TResult> Fail(List<string> errors) => 
            new(false, default, errors);

        private static ValueObjectResult<TResult> Ok(TResult value) =>
            new(true, value, default);

        public static implicit operator ValueObjectResult<TResult>(List<string> errors) => 
            Fail(errors);

        public static implicit operator ValueObjectResult<TResult>(TResult value) =>
            Ok(value);
        
        public static implicit operator TResult(ValueObjectResult<TResult> result) =>
            result.Value;
    }
}