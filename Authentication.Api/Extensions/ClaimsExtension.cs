using System.Security.Claims;

namespace Authentication.Api.Extension
{
    public static class ClaimsExtension
    {
        public static string GetId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "Id")?.Value ?? string.Empty;
        }

        public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "email")?.Value ?? string.Empty;
        }

        public static string GetRole(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "role")?.Value ?? string.Empty;
        }
    }
}