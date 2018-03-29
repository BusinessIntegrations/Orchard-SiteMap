#region Using
using System.Collections.Generic;
#endregion

namespace WebAdvanced.Sitemap.ViewModels {
    public class DisplaySettingsPageModel {
        #region Properties
        public bool AutoLayout { get; set; }
        public List<RouteSettingsModel> Routes { get; set; }
        #endregion
    }
}
