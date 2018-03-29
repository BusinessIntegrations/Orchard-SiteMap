namespace WebAdvanced.Sitemap.Models {
    public class SitemapCustomRouteRecord {
        #region Properties
        public virtual int Id { get; set; }
        public virtual bool IndexForDisplay { get; set; }
        public virtual bool IndexForXml { get; set; }
        public virtual int Priority { get; set; }
        public virtual string UpdateFrequency { get; set; }
        public virtual string Url { get; set; }
        #endregion
    }
}
