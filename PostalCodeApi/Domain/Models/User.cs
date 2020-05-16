namespace PostalCodeApi.Domain.Models
{
    /// <summary>
    /// Database User model
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; } = Entities.Role.Admin;
    }
}