#region Using
using System.Collections.Generic;
#endregion

namespace WebAdvanced.Sitemap.Services {
    public class DescribeSpecializedSitemapProviderContext {
        public DescribeSpecializedSitemapProviderContext() {
            Describes = new Dictionary<string, DescribeSpecializedSitemapFor>();
        }

        #region Properties
        internal Dictionary<string, DescribeSpecializedSitemapFor> Describes { get; }
        #endregion

        #region Methods
        public DescribeSpecializedSitemapFor For(string namespacePrefix) {
            if (!Describes.TryGetValue(namespacePrefix, out var describeFor)) {
                describeFor = new DescribeSpecializedSitemapFor(namespacePrefix);
                Describes[namespacePrefix] = describeFor;
            }
            return describeFor;
        }
        #endregion
    }
}
