namespace PostalCodeApi.Domain.Services.Communication
{
    public abstract class BaseResponse<T>
    {
        protected BaseResponse(T resource, int statusCode)
        {
            Success = true;
            StatusCode = statusCode;
            Message = string.Empty;
            Resource = resource;
        }

        protected BaseResponse(string message, int statusCode)
        {
            Success = false;
            StatusCode = statusCode;
            Message = message;
            Resource = default;
        }

        public bool Success { get; }
        public int StatusCode { get; }
        public string Message { get; }
        public T Resource { get; }
    }
}