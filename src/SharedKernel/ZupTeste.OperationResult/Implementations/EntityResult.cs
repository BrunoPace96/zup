namespace ZupTeste.OperationResult.Implementations
{
    public sealed class EntityResult<TResult> : 
        Result<TResult, Dictionary<string, List<string>>>
    {
        private EntityResult(
            bool success,
            TResult value,
            Dictionary<string, List<string>> errors
        ) : base(success, value, errors) {}

        private static EntityResult<TResult> Fail(Dictionary<string, List<string>> errors) => 
            new(false, default, errors);

        private static EntityResult<TResult> Ok(TResult value) =>
            new(true, value, default);

        public static implicit operator EntityResult<TResult>(Dictionary<string, List<string>> errors) => 
            Fail(errors);

        public static implicit operator EntityResult<TResult>(TResult value) =>
            Ok(value);
            
        public static implicit operator TResult(EntityResult<TResult> value) =>
            value.Value;
    }
}