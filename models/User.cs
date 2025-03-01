namespace ProjectManagement.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PasswordHash { get; set; }
    }

    public record Get_User_Register(
        string FullName,
        string Email,
        string Phone,
        string PasswordHash
    );

    public record Get_User_Login(
        string Email,
        string PasswordHash
    );
}
