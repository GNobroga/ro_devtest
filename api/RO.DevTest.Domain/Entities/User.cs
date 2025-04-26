using Microsoft.AspNetCore.Identity;

namespace RO.DevTest.Domain.Entities;

/// <summary>
/// Represents a <see cref="IdentityUser"/> int the API
/// </summary>
public class User : IdentityUser {
    /// <summary>
    /// Name of the user
    /// </summary>
    public string Name { get; set; } = string.Empty;

    public DateTime CreationOn { get; set; }
    public DateTime ModifiedOn { get; set; }

    public User() : base() { }
}
