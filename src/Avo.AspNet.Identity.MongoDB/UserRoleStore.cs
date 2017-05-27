using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MongoDB.Driver;

namespace Avo.AspNet.Identity.MongoDB
{
    /// <summary>
    /// Inherits from base UserStore and implements IUserRoleStore. Suitable for use in roles-based identity
    /// </summary>
    public class UserRoleStore<TUser> : 
        UserStore<TUser>, 
        IUserRoleStore<TUser>
        where TUser: IdentityUser
    {
        public UserRoleStore(IMongoCollection<TUser> userCollection) 
            : base(userCollection) {}

        public virtual Task AddToRoleAsync(TUser user, string roleName)
        {
            return Task.Run(() => user.Roles.Add(roleName));
        }

        public virtual Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            return Task.Run(() =>user.Roles.Remove(roleName));
        }

        public virtual Task<IList<string>> GetRolesAsync(TUser user)
        {
            return Task.Run(() => (IList<string>)user.Roles);
        }

        public virtual Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            return Task.Run(() => user.Roles.Contains(roleName));
        }
    }
}