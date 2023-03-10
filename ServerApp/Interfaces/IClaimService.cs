using System.Security.Claims;

namespace ServerApp.Interfaces;

public interface IClaimService
{
    long GetUserId(ClaimsPrincipal User);
}
