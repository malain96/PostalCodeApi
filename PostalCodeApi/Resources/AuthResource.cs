namespace PostalCodeApi.Resources
{
    public class AuthResource
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public bool IsAdmin { get; set; }
        public string Token { get; set; }
    }
}