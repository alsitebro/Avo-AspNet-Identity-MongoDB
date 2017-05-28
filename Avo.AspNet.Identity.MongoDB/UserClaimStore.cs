using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MongoDB.Driver;

namespace Avo.AspNet.Identity.MongoDB
{
    /// <summary>
    /// Inherits from base UserStore and implements IUserClaimStore. Suitable for use in claims-based identity
    /// </summary>
    public class UserClaimStore<TUser> : 
        UserStore<TUser>, 
        IUserClaimStore<TUser>
        where TUser : IdentityUser
    {
        public UserClaimStore(IMongoCollection<TUser> userCollection) 
            : base(userCollection){}

        public virtual Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            return Task.Run(() => user.Claims.Select(c => c.ToSecurityClaim()).ToIList());
        }

        public virtual Task AddClaimAsync(TUser user, Claim claim)
        {
            return Task.Run(()=> user.Claims.Add(new IdentityUserClaim(claim.Type, claim.Value)));
        }

        public virtual Task RemoveClaimAsync(TUser user, Claim claim)
        {
            return Task.Run(() =>
            {
                var item = user.Claims.FirstOrDefault(c => c.Type == claim.Type && c.Value == claim.Value);
                user.Claims.Remove(item);
            });
        }
    }
}