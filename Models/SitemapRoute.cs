#region Using
using System;
using Orchard.ContentManagement;
#endregion

namespace WebAdvanced.Sitemap.Models {
    public class SitemapRoute {
        #region Properties
        public ContentItem ContentItem { get; set; }
        public DateTime? LastUpdated { get; set; }

        /// <summary>
        ///     Ignored for display
        /// </summary>
        public int Priority { get; set; }

        public string Title { get; set; }

        /// <summary>
        ///     Ignored for display
        /// </summary>
        public string UpdateFrequency { get; set; }

        public string Url { get; set; }

        /// <summary>
        ///     The relative route that the url should be structured by in the sitemap tree.
        ///     Providers can use this to explicitly structure a route tree that does not correspond to the actual urls.
        ///     Very useful for, eg. routes that are absolute.
        /// </summary>
        public string UrlAlias { get; set; }
        #endregion
    }
}
