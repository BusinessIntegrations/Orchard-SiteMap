#region Using
using Orchard;
#endregion

namespace WebAdvanced.Sitemap.Services {
    public interface ISitemapRouteFilter : IDependency {
        #region Methods
        /// <summary>
        ///     Filter function to disallow certain paths from appearing for display and xml.
        /// </summary>
        /// <param name="path">The relative path of the found content item.</param>
        /// <returns>True if this url should be allowed in the sitemap. False if not.</returns>
        bool AllowUrl(string path);
        #endregion
    }
}
