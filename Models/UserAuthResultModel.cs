namespace farmapi.Models
{
    public class UserAuthResultModel
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public bool Authenticated { get; set; }
    }
}