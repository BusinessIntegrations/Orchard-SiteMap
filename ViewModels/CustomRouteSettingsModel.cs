namespace WebAdvanced.Sitemap.ViewModels {
    public class CustomRouteSettingsModel {
        #region Properties
        public bool IndexForDisplay { get; set; }
        public bool IndexForXml { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public string UpdateFrequency { get; set; }
        public string Url { get; set; }
        #endregion
    }
}
