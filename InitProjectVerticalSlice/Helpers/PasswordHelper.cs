using BC = BCrypt.Net.BCrypt;

namespace EventEchosAPI.Helpers
{
    public class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            return BC.HashPassword(password, 12);
        }

        public static bool VerifyPassword(string password, string hash)
        {
            return BC.Verify(password, hash);
        }
    }
}
