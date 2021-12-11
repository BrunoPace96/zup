using System.Net;
using Newtonsoft.Json;
using ZupTeste.DataContracts.Results;

namespace ZupTeste.OperationResult.Implementations
{
    public class ApiResult<TResult, TError> where TResult : class
    {
        public TResult Result { get; }
        public TError Error { get; }
        public HttpStatusCode StatusCode { get; }
        public bool IsFail => !IsSuccess;
        public bool IsSuccess { get; }

        private ApiResult(TResult result, HttpStatusCode statusCode)
        {
            Result = result;
            IsSuccess = true;
            StatusCode = statusCode;
        }

        private ApiResult(TError error, HttpStatusCode statusCode)
        {
            Error = error;
            StatusCode = statusCode;
            IsSuccess = false;
        }

        public static async Task<ApiResult<TResult, TError>> SuccessAsync(HttpResponseMessage response)
        {
            if (typeof(TResult) == typeof(EmptyResult))
            {
                var emptyResult = EmptyResult.Create() as TResult;
                return new ApiResult<TResult, TError>(emptyResult, response.StatusCode);
            }

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TResult>(content);
            return new ApiResult<TResult, TError>(result, response.StatusCode);
        }

        public static async Task<ApiResult<TResult, TError>> FailureAsync(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<TError>(content);
            return new ApiResult<TResult, TError>(error, response.StatusCode);
        }
    }
}