namespace PostalCodeApi.Resources
{
    /// <summary>
    /// Response of the authenticate route
    /// </summary>
    public class AuthResource
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}