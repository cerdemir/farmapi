namespace farmapi.Models
{
    public class ApiResponseModel<T>
    {
        public bool Success { get; set; } = true;
        public string ErrorMessage { get; set; }
        public string UserErrorMessage { get; set; }
        public T Data { get; set; }
    }
}