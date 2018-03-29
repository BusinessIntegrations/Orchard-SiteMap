#region Using
using System.Collections.Generic;
#endregion

namespace WebAdvanced.Sitemap.ViewModels {
    public class IndexingPageModel {
        #region Properties
        public List<IndexSettingsModel> ContentTypeSettings { get; set; }
        public List<CustomRouteModel> CustomRoutes { get; set; }
        #endregion
    }
}
