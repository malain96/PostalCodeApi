namespace PostalCodeApi.Domain.Services.Communication
{
    public abstract class BaseResponse<T>
    {
        protected BaseResponse(T resource)
        {
            Success = true;
            InternalServerError = false;
            Message = string.Empty;
            Resource = resource;
        }

        protected BaseResponse(string message, bool internalServerError)
        {
            Success = false;
            InternalServerError = internalServerError;
            Message = message;
            Resource = default;
        }

        public bool Success { get; }
        public bool InternalServerError { get; }
        public string Message { get; }
        public T Resource { get; }
    }
}