#region Using
using System.Linq;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using WebAdvanced.Sitemap.Services;
#endregion

namespace WebAdvanced.Sitemap.Handlers {
    public class SitemapContentHandler : ContentHandler {
        public SitemapContentHandler(IAdvancedSitemapService sitemapService) {
            OnPublished<ContentItem>((ctx, item) => {
                var activeContentTypes = sitemapService.GetContentTypeRouteSettings()
                    .Where(m => m.IndexForDisplay || m.IndexForXml)
                    .Select(m => m.Name)
                    .ToList();
                if (activeContentTypes.Contains(ctx.ContentItem.ContentType)) {
                    sitemapService.ReleaseDisplayRouteSettingsCache();
                }
            });
        }
    }
}
