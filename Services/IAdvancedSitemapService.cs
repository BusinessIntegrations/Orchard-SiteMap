#region Using
using System.Collections.Generic;
using System.Xml.Linq;
using Orchard;
using WebAdvanced.Sitemap.Models;
using WebAdvanced.Sitemap.ViewModels;
#endregion

namespace WebAdvanced.Sitemap.Services {
    public interface IAdvancedSitemapService : IDependency {
        #region Methods
        void DeleteCustomRoute(string url);
        IEnumerable<ContentTypeRouteSettingsModel> GetContentTypeRouteSettings();
        IEnumerable<CustomRouteSettingsModel> GetCustomRouteSettings();
        IEnumerable<DisplayRouteSettingsModel> GetDisplayRouteSettings();
        XDocument GetSitemapDocument();
        SitemapNode GetSitemapRoot();
        void RefreshCache();
        void ReleaseDisplayRouteSettingsCache();
        void SetContentTypeRouteSettings(IEnumerable<ContentTypeRouteSettingsModel> settings);
        void SetCustomRouteSettings(IEnumerable<CustomRouteSettingsModel> settings);
        void SetDisplayRouteSettings(IEnumerable<DisplayRouteSettingsModel> settings);
        #endregion
    }
}
