#region Using
using System.Collections.Generic;
#endregion

namespace WebAdvanced.Sitemap.Models {
    public class SitemapNode {
        public SitemapNode(string title, string url = null) {
            Title = title;
            Url = url;
            Children = new Dictionary<string, SitemapNode>();
        }

        #region Properties
        public Dictionary<string, SitemapNode> Children { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        #endregion
    }
}
