#region Using
using System.Collections.Generic;
#endregion

namespace WebAdvanced.Sitemap.ViewModels {
    public class IndexingPageModel {
        #region Properties
        public List<ContentTypeRouteSettingsModel> ContentTypeSettings { get; set; }
        public List<CustomRouteSettingsModel> CustomRoutes { get; set; }
        #endregion
    }
}
