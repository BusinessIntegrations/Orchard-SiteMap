#region Using
using System.Collections.Generic;
using System.Linq;
using Orchard.Autoroute.Models;
using Orchard.ContentManagement;
using Orchard.Core.Common.Models;
using Orchard.Data;
using WebAdvanced.Sitemap.Models;
#endregion

namespace WebAdvanced.Sitemap.Providers.Impl {
    public class ContentRouteProvider : ISitemapRouteProvider {
        private readonly IContentManager _contentManager;
        private readonly IRepository<SitemapSettingsRecord> _sitemapSettings;

        public ContentRouteProvider(IRepository<SitemapSettingsRecord> sitemapSettings, IContentManager contentManager) {
            _sitemapSettings = sitemapSettings;
            _contentManager = contentManager;
        }

        #region ISitemapRouteProvider Members
        public IEnumerable<SitemapRoute> GetDisplayRoutes() {
            // Get all active content types
            var types = _sitemapSettings.Fetch(q => q.IndexForDisplay)
                .ToDictionary(k => k.ContentType, v => v);
            if (types.Any()) {
                var contents = _contentManager.Query(VersionOptions.Published, types.Keys.ToArray())
                    .List();
                return contents.Select(c => GetSitemapRoute(types, c))
                    .AsEnumerable();
            }
            return new List<SitemapRoute>();
        }

        public IEnumerable<SitemapRoute> GetXmlRoutes() {
            // Get all active content types
            var types = _sitemapSettings.Fetch(q => q.IndexForXml)
                .ToDictionary(k => k.ContentType, v => v);
            if (types.Any()) {
                var contents = _contentManager.Query(VersionOptions.Published, types.Keys.ToArray())
                    .List();
                return contents.Select(c => GetSitemapRoute(types, c))
                    .AsEnumerable();
            }
            return new List<SitemapRoute>();
        }

        public int Priority => 10;
        #endregion

        #region Methods
        private SitemapRoute GetSitemapRoute(IReadOnlyDictionary<string, SitemapSettingsRecord> types, ContentItem c) {
            var autoroutePart = c.As<AutoroutePart>();
            return new SitemapRoute {
                Priority = types[c.ContentType]
                    .Priority,
                Title = _contentManager.GetItemMetadata(c)
                    .DisplayText,
                UpdateFrequency = types[c.ContentType]
                    .UpdateFrequency,
                Url = autoroutePart.PromoteToHomePage
                    ? ""
                    : autoroutePart.Path,
                ContentItem = c,
                LastUpdated = c.Has<CommonPart>()
                    ? c.As<CommonPart>()
                        .ModifiedUtc
                    : null
            };
        }
        #endregion
    }
}
