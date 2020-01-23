using System.Security.Claims;

namespace Implementation.Components
{
    public interface ITokenManagerComponent
    {
        string GenerateToken(string username);
        ClaimsPrincipal GetClaimsPrincipal(string token);
        string ValidateToken(string token);
    }
}