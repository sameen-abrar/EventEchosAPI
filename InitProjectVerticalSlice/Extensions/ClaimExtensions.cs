using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace EventEchosAPI.Extensions
{
    public static class ClaimExtensions
    {
        public static class CustomClaimTypes
        {
            public const string? UserId = "userId";
            public const string UserName = "userName";
        }
        public static Claim AddUserId(this ClaimsIdentity identity, string? userId)
        {
            return new Claim(CustomClaimTypes.UserId, userId.ToString());
        }
    }
}
