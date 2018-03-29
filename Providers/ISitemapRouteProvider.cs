#region Using
using System.Collections.Generic;
using Orchard;
using WebAdvanced.Sitemap.Models;
#endregion

namespace WebAdvanced.Sitemap.Providers {
    public interface ISitemapRouteProvider : IDependency {
        #region Properties
        int Priority { get; }
        #endregion

        #region Methods
        IEnumerable<SitemapRoute> GetDisplayRoutes();
        IEnumerable<SitemapRoute> GetXmlRoutes();
        #endregion
    }
}
