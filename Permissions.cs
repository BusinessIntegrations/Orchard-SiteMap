#region Using
using System.Collections.Generic;
using Orchard.Environment.Extensions.Models;
using Orchard.Security.Permissions;
#endregion

namespace WebAdvanced.Sitemap {
    public class Permissions : IPermissionProvider {
        public static readonly Permission ManageSitemap = new Permission {
            Description = "Manage sitemap",
            Name = "ManageSitemap"
        };

        #region IPermissionProvider Members
        public IEnumerable<PermissionStereotype> GetDefaultStereotypes() {
            return new[] {
                new PermissionStereotype {
                    Name = "Administrator",
                    Permissions = new[] {ManageSitemap}
                }
            };
        }

        public IEnumerable<Permission> GetPermissions() {
            return new[] {ManageSitemap};
        }

        public virtual Feature Feature { get; set; }
        #endregion
    }
}
