using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MongoDB.Driver;

namespace Avo.AspNet.Identity.MongoDB
{
    /// <summary>
    /// Suitable if you want to manage roles in your application
    /// </summary>
    public class RoleStore<TRole>
        : IRoleStore<TRole>,IQueryableRoleStore<TRole, string>
        where TRole : IdentityRole
    {
        private readonly IMongoCollection<TRole> _rolesCollection;

        public RoleStore(IMongoCollection<TRole> rolesCollection)
        {
            _rolesCollection = rolesCollection;
        }

        public Task CreateAsync(TRole role)
        {
            return Task.Run(() => _rolesCollection.InsertOneAsync(role));
        }

        public Task UpdateAsync(TRole role)
        {
            return _rolesCollection.ReplaceOneAsync(r => r.Id == role.Id, role);
        }

        public Task DeleteAsync(TRole role)
        {
            return _rolesCollection.DeleteOneAsync(r => r.Id == role.Id);
        }

        public Task<TRole> FindByIdAsync(string roleId)
        {
            return _rolesCollection.Find(r => r.Id == roleId).FirstOrDefaultAsync();
        }

        public Task<TRole> FindByNameAsync(string roleName)
        {
            return _rolesCollection.Find(r => r.Name == roleName).FirstOrDefaultAsync();
        }

        public void Dispose()
        {}

        public IQueryable<TRole> Roles => _rolesCollection.AsQueryable();
    }
}