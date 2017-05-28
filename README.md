## Avo.AspNet.Identity.MongoDB ##

MongoDB storage provider library for ASP.NET Identity 2.2.1

## Purpose ##

ASP.NET MVC 5 shipped with a new Identity system (in the Microsoft.AspNet.Identity.Core package) in order to support both local login and remote logins via OpenID/OAuth, but only ships with an Entity Framework provider (Microsoft.AspNet.Identity.EntityFramework).

ASP.NET Identity 2.x has matured at version 2.2.1 and the ASP.NET team is now focussed on [3.x for ASP.NET Core](https://github.com/aspnet/Identity/). 

[Tugburk Ugurlu](https://github.com/tugberkugurlu/AspNetCore.Identity.MongoDB) currently maintains a MongoDB provider if you're interested in that.

I did this first as part of a learning process, then thought I'd share it with the community.

I aim to update this project as and when the [MongoDB.Driver for .NET](http://mongodb.github.io/mongo-csharp-driver/) is updated.

Please feel free to use, review and improve.

## Features ##
* Drop-in replacement ASP.NET Identity with MongoDB as the backing store.
* Requires only 1 mongo document type for user storage, while EntityFramework requires 5 tables
* Contains the same IdentityUser class used by the EntityFramework provider in the MVC 5 project template.
* Supports additional profile properties on your application's user model.
* Provides implementations for IUserStore<TUser> in three variants:
    * UserStore<TUser> which acts as the base store class implementing: 
        * IUserStore<TUser>
        * IQueryableUserStore<TUser>
        * IUserEmailStore<TUser>
        * IUserSecurityStampStore<TUser>
        * IUserPhoneNumberStore<TUser>
        * IUserLoginStore<TUser>
        * IUserPasswordStore<TUser, string>
        * IUserLockoutStore<TUser, string>
        * IUserTwoFactorStore<TUser, string>
    * UserClaimStore<TUser> which inherits UserStore<TUser> and implements IUserClaimStore<TUser>. Suitable for use with claims-based identity
    * UserRoleStore<TUser> which inherits UserStore<TUser> and implements IUserRoleStore<TUser>. Suitable for use with roles-based identity.
    * UserStore constructor requires an instance of IMongoCollection<TUser>. This approach was chosen to allow flexibility and configurability.

## Instructions ##
These instructions assume you know how to set up MongoDB within an MVC application.

1. Create a new ASP.NET MVC 5 project, choosing the Individual User Accounts authentication type.
2. Remove the Entity Framework packages and replace with MongoDB Identity:

```NuGet Package Manager Console
Uninstall-Package Microsoft.AspNet.Identity.EntityFramework
Uninstall-Package EntityFramework
Install-Package Avo.AspNet.Identity.MongoDB
```
    
3. In ~/Models/IdentityModels.cs:
    * Remove the namespace: Microsoft.AspNet.Identity.EntityFramework
    * Add the namespace: Avo.AspNet.Identity.MongoDB
	* Remove the ApplicationDbContext class completely.
4. In ~/Controllers/AccountController.cs
    * Remove the namespace: Microsoft.AspNet.Identity.EntityFramework
    * Replace instances of UserStore with your chosen implementation

```C#
public AccountController()
{
    this.UserManager = new UserManager<ApplicationUser>(
        new UserStore<ApplicationUser>(userCollection);
}
```

## Constructor ##
The UserStore does not require connection strings in this implementation as it assumed that the developer would be managing interaction with the MongoDB database. The constructor therefore expects an instance of IMongoCollection<TUser>

```C#
UserStore(IMongoCollection<TUser> userCollection)
```
<code>new UserClaimStore<IdentityUser>(db.GetCollection<IdentityUser>("app_users"))</code>

## Thanks ##

This work was inspired by projects previously supported by [InspectorIT](https://github.com/InspectorIT/MongoDB.AspNet.Identity) and [g0t4](https://github.com/g0t4/aspnet-identity-mongo).

I would like to thank them for the work they've already done, which inspired this project.