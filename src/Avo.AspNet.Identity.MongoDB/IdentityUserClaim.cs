using System.Security.Claims;

namespace Avo.AspNet.Identity.MongoDB
{
    public class IdentityUserClaim
    {
        public IdentityUserClaim(){}

        public IdentityUserClaim(string claimType, string claimValue)
        {
            Type = claimType;
            Value = claimValue;
        }

        public string Type { get; set; }
        public string Value { get; set; }

        public Claim ToSecurityClaim()
        {
            return new Claim(Type, Value);
        }
    }
}