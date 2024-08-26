using System.Security.Claims;

namespace shopping_project_api.services
{
    public interface ITokenService
    {
        string GenerateToken(IEnumerable<Claim> claims);
    }
}
