namespace farmapi.Models
{
    public class ApiResponseModel<T>
    {
        public bool Success { get; set; } = true;
        public string ExceptionDetails { get; set; }
        public string UserExceptionMessage { get; set; }
        public T Data { get; set; }
    }
    public class ApiResponseModel : ApiResponseModel<object>
    {

    }
}