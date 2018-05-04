#region Using
using System.Collections.Generic;
using System.Linq;
using Orchard.Data;
using WebAdvanced.Sitemap.Extensions;
using WebAdvanced.Sitemap.Models;
#endregion

namespace WebAdvanced.Sitemap.Providers.Impl {
    /// <summary>
    ///     Supplies custom routes as defined in admin area
    /// </summary>
    public class CustomRouteProvider : ISitemapRouteProvider {
        private readonly IRepository<SitemapCustomRouteRecord> _customRoutes;

        public CustomRouteProvider(IRepository<SitemapCustomRouteRecord> customRoutes) {
            _customRoutes = customRoutes;
        }

        #region ISitemapRouteProvider Members
        public IEnumerable<SitemapRoute> GetDisplayRoutes() {
            return _customRoutes.Fetch(q => q.IndexForDisplay)
                .Select(r => new SitemapRoute {
                    Url = r.Url,
                    Title = r.Url.UrlToTitle()
                })
                .AsEnumerable();
        }

        public IEnumerable<SitemapRoute> GetXmlRoutes() {
            return _customRoutes.Fetch(q => q.IndexForXml)
                .Select(r => new SitemapRoute {
                    Url = r.Url,
                    Title = r.Url.UrlToTitle(),
                    Priority = r.Priority,
                    UpdateFrequency = r.UpdateFrequency
                })
                .AsEnumerable();
        }

        public int Priority => 20;
        #endregion
    }
}
