using Microsoft.AspNetCore.Identity;
using RO.DevTest.Domain.Entities;
using RO.DevTest.Domain.Enums;

namespace RO.DevTest.Application.Contracts.Infrastructure;


/// <summary>
/// This is a abstraction of the Identity library, creating methods that will interact with 
/// it to create and update users
/// </summary>
public interface IIdentityAbstractor {
    /// <summary>
    /// Finds a <see cref="User"/> through its
    /// ID asynchronously
    /// </summary>
    /// <param name="userId">
    /// The <see cref="User"/>s ID
    /// </param>
    /// <returns>
    /// The <see cref="User"/> if found, <see cref="null"/>
    /// otherwise
    /// </returns>
    Task<User?> FindUserByIdAsync(string userId);


    /// <summary>
    /// Finds a <see cref="User"/> by its username asynchronously.
    /// </summary>
    /// <param name="username">
    /// The username of the <see cref="User"/> to find.
    /// </param>
    /// <returns>
    /// The matching <see cref="User"/> if found; otherwise, <see langword="null"/>.
    /// </returns>
    Task<User?> FindUserByUserNameAsync(string username);

    /// <summary>
    /// Finds a <see cref="User"/> through its
    /// email asynchronously
    /// </summary>
    /// <param name="email">
    /// The <see cref="User"/>s email
    /// </param>
    /// <returns>
    /// The <see cref="User"/> if found, <see cref="null"/>
    /// otherwise
    /// </returns>
    Task<User?> FindUserByEmailAsync(string email);

    /// <summary>
    /// Gets the names of the <see cref="IdentityRole"/>s
    /// that a <see cref="User"/> has asynchronously
    /// </summary>
    /// <param name="user">
    /// The <see cref="User"/> to get the <see cref="IdentityRole"/>s
    /// </param>
    /// <returns>
    /// A <see cref="IList{T}"/> with the names of the roles
    /// </returns>
    Task<IList<string>> GetUserRolesAsync(User user);

    /// <summary>
    /// Signs in a <see cref="User"/> asynchronously in a non
    /// persistent way. The <see cref="User"/>'s account is not
    /// locked if failed
    /// </summary>
    /// <param name="user">
    /// The <see cref="User"/> to sign in
    /// </param>
    /// <param name="password">
    /// The <see cref="User"/>'s password
    /// </param>
    /// <returns>
    /// A <see cref="SignInResult"/>
    /// </returns>
    Task<SignInResult> PasswordSignInAsync(User user, string password);

    /// <summary>
    /// Creates a <see cref="User"/> asynchronously an returns
    /// the <see cref="IdentityResult"/> of it
    /// </summary>
    /// <param name="user">
    /// The <see cref="User"/> to be added
    /// </param>
    /// <param name="password">
    /// The plain text of the password to be used to hash it
    /// </param>
    /// <returns>
    /// The <see cref="IdentityResult"/>
    /// </returns>
    Task<IdentityResult> CreateUserAsync(User user, string password);

    /// <summary>
    /// Adds a <see cref="User"/> to a <see cref="IdentityRole"/>
    /// asynchronously. If the role doesn't exist, it will be created.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    Task<IdentityResult> AddToRoleAsync(User user, UserRoles role);

    /// <summary>
    /// Deletes a <see cref="ApplicationUser"/> from the database
    /// </summary>
    /// <param name="user">
    /// The <see cref="ApplicationUser"/> to be deleted
    /// </param>
    /// <returns>
    /// A <see cref="Task{IdentityResult}"/>
    /// </returns>
    Task<IdentityResult> DeleteUser(User user);


    /// <summary>
    /// Resets the password of the specified <see cref="User"/> using a generated reset token.
    /// </summary>
    /// <param name="user">
    /// The <see cref="User"/> whose password will be reset.
    /// </param>
    /// <param name="newPassword">
    /// The new password to be set for the user.
    /// </param>
    /// <returns>
    /// A <see cref="Task{Boolean}"/> indicating whether the password reset was successful.
    /// </returns>
    Task<IdentityResult> ResetUserPasswordAsync(User user, string newPassword);

    /// <summary>
    /// Updates the specified <see cref="User"/> in the database.
    /// </summary>
    /// <param name="user">
    /// The <see cref="User"/> entity with updated information to be saved.
    /// </param>
    /// <returns>
    /// A <see cref="Task{IdentityResult}"/> indicating whether the update operation succeeded.
    /// </returns>
    Task<IdentityResult> UpdateUserAsync(User user);

    /// <summary>
    /// Removes all roles assigned to the specified user.
    /// </summary>
    /// <param name="user">The user from whom the roles will be removed.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains 
    /// <c>true</c> if any roles were removed successfully; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the <paramref name="user"/> parameter is <c>null</c>.
    /// </exception>
    Task<bool> RemoveRolesFromUser(User user);

}
