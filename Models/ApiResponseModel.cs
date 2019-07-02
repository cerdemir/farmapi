namespace farmapi.Models
{
    /// <summary>
    /// object for all api responses
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponseModel<T>
    {
        /// <summary>
        /// Success
        /// </summary>
        /// <value></value>
        public bool Success { get; set; } = true;
        /// <summary>
        /// ExceptionDetails
        /// </summary>
        /// <value></value>
        public string ExceptionDetails { get; set; }
        /// <summary>
        /// UserExceptionMessage
        /// </summary>
        /// <value></value>
        public string UserExceptionMessage { get; set; }
        /// <summary>
        /// contained data
        /// </summary>
        /// <value></value>
        public T Data { get; set; }
    }
    /// <summary>
    /// ApiResponseModel
    /// </summary>
    public class ApiResponseModel : ApiResponseModel<object>
    {

    }
}