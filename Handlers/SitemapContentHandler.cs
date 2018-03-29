#region Using
using System.Linq;
using Orchard.Caching;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using WebAdvanced.Sitemap.Services;
#endregion

namespace WebAdvanced.Sitemap.Handlers {
    public class SitemapContentHandler : ContentHandler {
        public SitemapContentHandler(ISignals signals, IAdvancedSitemapService sitemapService) {
            OnPublished<ContentItem>((ctx, item) => {
                var activeContentTypes = sitemapService.GetIndexSettings()
                    .Where(m => m.IndexForDisplay || m.IndexForXml)
                    .Select(m => m.Name)
                    .ToList();
                if (activeContentTypes.Contains(ctx.ContentItem.ContentType)) {
                    signals.Trigger(Constants.RefreshCache);
                }
            });
        }
    }
}
