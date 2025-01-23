namespace MyWebApp
{
    public class RegisterUserDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Password { get; set; }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class UpdateUserDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Password { get; set; }
    }
    public class CreateNewUserAndLogin
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
