namespace ZupTeste.OperationResult.Implementations
{
    public sealed class OperationResult<TResult> : 
        Result<TResult, List<string>>
    {
        private OperationResult(
            bool success,
            TResult value,
            List<string> errors
        ) : base(success, value, errors) {}

        private static OperationResult<TResult> Fail(List<string> errors) => 
            new(false, default, errors);

        private static OperationResult<TResult> Ok(TResult value) =>
            new(true, value, default);

        public static implicit operator OperationResult<TResult>(List<string> errors) => 
            Fail(errors);
        
        public static implicit operator OperationResult<TResult>(string error) => 
            Fail(new List<string> { error });

        public static implicit operator OperationResult<TResult>(TResult value) =>
            Ok(value);
    }
}