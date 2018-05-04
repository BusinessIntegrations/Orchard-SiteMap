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

        private static readonly Permission[] permissions = {ManageSitemap};

        #region IPermissionProvider Members
        public IEnumerable<PermissionStereotype> GetDefaultStereotypes() {
            return new[] {
                new PermissionStereotype {
                    Name = "Administrator",
                    Permissions = permissions
                }
            };
        }

        public IEnumerable<Permission> GetPermissions() {
            return permissions;
        }

        public virtual Feature Feature { get; set; }
        #endregion
    }
}
