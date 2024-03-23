namespace ExpenseTracker.Api.Models
{
    public class SignInModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class SignUpModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserDetailsModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
