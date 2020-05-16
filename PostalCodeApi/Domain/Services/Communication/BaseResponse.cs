namespace PostalCodeApi.Domain.Services.Communication
{
    public abstract class BaseResponse<T>
    {

        /// <summary>
        /// Creates a success response
        /// </summary>
        /// <param name="resource">Object of type T</param>
        /// <param name="statusCode">Success status code</param>
        protected BaseResponse(T resource, int statusCode)
        {
            Success = true;
            StatusCode = statusCode;
            Message = string.Empty;
            Resource = resource;
        }

        /// <summary>
        /// Creates an error response
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="statusCode">Error status code</param>
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