#region Using
using Orchard;
#endregion

namespace WebAdvanced.Sitemap.Services {
    public interface ISpecializedSitemapProvider : IDependency {
        #region Methods
        void Describe(DescribeSpecializedSitemapProviderContext providerContext);
        #endregion
    }
}
