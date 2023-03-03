using ServerApp.Interfaces;
using System.Security.Claims;

namespace ServerApp.Services
{
    public class ClaimService : IClaimService
    {
        public long GetUserId(ClaimsPrincipal User)
        {
            string? userIdStr = User.Claims.FirstOrDefault(x => x.Type == "id")?.Value; 
            return Convert.ToInt64(userIdStr);
        }
    }
}
