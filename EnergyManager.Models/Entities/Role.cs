using Microsoft.AspNetCore.Identity;

namespace EnergyManager.Models.Entities
{

    public class Role : IdentityRole
    {        
        /// <summary>
        /// Initializes a new instance of <see cref="Role"/>.
        /// </summary>
        /// <param name="roleName">The role name.</param>       
        public Role(string roleName) : base(roleName)
        {
        }

        /// <summary>
        /// Navigation property for the users in this role.
        /// </summary>
        public virtual ICollection<IdentityUserRole<string>> Users { get; set; }

        /// <summary>
        /// Navigation property for claims in this role.
        /// </summary>
        public virtual ICollection<IdentityRoleClaim<string>> Claims { get; set; }
    }

}
