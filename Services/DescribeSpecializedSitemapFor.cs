#region Using
using System;
using System.Xml.Linq;
using Orchard.ContentManagement;
#endregion

namespace WebAdvanced.Sitemap.Services {
    public class DescribeSpecializedSitemapFor {
        public DescribeSpecializedSitemapFor(string namespacePrefix) {
            NamespacePrefix = namespacePrefix;
        }

        #region Properties
        public string NamespacePrefix { get; }
        public Func<ContentItem, string, XElement> Process { get; private set; }
        public XNamespace XNamespace { get; private set; }
        #endregion

        #region Methods
        public void Configure(XNamespace xNamespace, Func<ContentItem, string, XElement> process) {
            XNamespace = xNamespace;
            Process = process;
        }
        #endregion
    }
}
