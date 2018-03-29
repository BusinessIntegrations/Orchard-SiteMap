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
        IEnumerable<CustomRouteModel> GetCustomRoutes();
        IEnumerable<IndexSettingsModel> GetIndexSettings();
        IEnumerable<RouteSettingsModel> GetRoutes();
        XDocument GetSitemapDocument();
        SitemapNode GetSitemapRoot();
        void SetCustomRoutes(IEnumerable<CustomRouteModel> settings);
        void SetIndexSettings(IEnumerable<IndexSettingsModel> settings);
        void SetRoutes(IEnumerable<RouteSettingsModel> settings);
        #endregion
    }
}
