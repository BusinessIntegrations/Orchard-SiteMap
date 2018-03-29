namespace WebAdvanced.Sitemap.Models {
    public class SitemapSettingsRecord {
        #region Properties
        public virtual string ContentType { get; set; }
        public virtual int Id { get; set; }
        public virtual bool IndexForDisplay { get; set; }
        public virtual bool IndexForXml { get; set; }
        public virtual int Priority { get; set; }
        public virtual string UpdateFrequency { get; set; }
        #endregion
    }
}
