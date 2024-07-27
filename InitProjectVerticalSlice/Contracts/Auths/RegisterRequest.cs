namespace EventEchosAPI.Contracts.Auths
{
    public class RegisterRequest
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string UserRoleType { get; set; }
        public bool IsAdmin { get; set; }
    }
}
