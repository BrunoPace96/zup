using System.Collections;
using ZupTeste.OperationResult.Messages;

namespace ZupTeste.OperationResult
{
    public class Result
    {
        public bool Success { get; }
        public bool Failure => !Success;

        protected Result(bool success)
        {
            Success = success;
        }

        protected static void ThrowsInvalidErrorMessage(string message) => 
            throw new Exception(message);
    }

    public class Result<TErrors> : Result
    {
        private readonly TErrors _errors;

        public TErrors Errors
        {
            get
            {
                if (Success)
                    ThrowsInvalidErrorMessage(ErrorMessages.ErrorsAccessOnSuccessResult);

                return _errors;
            }
        }

        internal Result(bool success, TErrors errors) : base(success)
        {
            if (!success)
            {
                var (invalid, message) = errors switch
                {
                    string s when string.IsNullOrEmpty(s) => 
                        Tuple.Create(true, ErrorMessages.EmptyErrorOnFailureResult),
                    null or IList { Count: 0 } or IDictionary { Count: 0 } =>
                        Tuple.Create(true, ErrorMessages.EmptyErrorOnFailureResult),
                    _ => Tuple.Create(false, string.Empty)
                };

                if (invalid)
                    ThrowsInvalidErrorMessage(message);
            }

            _errors = errors;
        }
    }

    public class Result<TValue, TErrors> : Result<TErrors>
    {
        private readonly TValue _value;

        public TValue Value
        {
            get
            {
                if (Failure)
                    throw new Exception(ErrorMessages.ValueAccessOnFailureResult);

                return _value;
            }
        }

        internal Result(bool success, TValue value, TErrors errors) : base(success, errors)
        {
            if (success && value == null)
                ThrowsInvalidErrorMessage(ErrorMessages.EmptyValueOnFailureResult);

            _value = value;
        }
    }
}
