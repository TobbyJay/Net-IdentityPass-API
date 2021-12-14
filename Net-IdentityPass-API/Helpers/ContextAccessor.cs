using System.Security.Claims;

namespace Net_IdentityPass_API.Helpers
{
    public class ContextAccessor : IContextAccessor
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public ContextAccessor(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public Guid GetClientId()
        {
            var identity = (ClaimsIdentity)_contextAccessor.HttpContext.User.Identity;

            // Gets list of claims.
            var claim = identity.Claims;

            // Gets userID from claims. Generally it's a  string.
            var loggedInUserId = claim
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            return Guid.Parse(loggedInUserId);
        }
    }
}
