namespace farmapi.Models
{
    public class AuthResultModel
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public bool Authenticated { get; set; }
    }
}