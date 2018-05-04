namespace WebAdvanced.Sitemap {
    public static class Constants {
        public const string AreaName = "WebAdvanced.Sitemap";
        public const string ControllerName = "Admin";
        public const string NotAllowedToManageSitemap = "Not allowed to manage sitemap";
        public const string WeeklyUpdateFrequency = "weekly";
        public static readonly string ContentDefinitionManagerCacheKey = "ContentDefinitionManager";
        public static readonly string CustomRouteSettingsCacheKey = $"{AreaName}.CustomRoutes";
        public static readonly string CustomRouteSettingsCacheTrigger = $"{AreaName}.CustomRoutes";
        public static readonly string DisplayRouteSettingsCacheKey = $"{AreaName}.Routes";
        public static readonly string DisplayRouteSettingsCacheTrigger = $"{AreaName}.Refresh";
        public static readonly string ContentTypeRouteSettingsCacheKey = $"{AreaName}.IndexSettings";
        public static readonly string ContentTypeRouteSettingsCacheTrigger = $"{AreaName}.IndexSettings";
        public static readonly string XmlDocumentCacheKey = "sitemap.xml";
        public static readonly string XmlDocumentCacheTrigger = $"{AreaName}.XmlRefresh";

        /// <summary>
        ///     Data cached by this key is also released by the <see cref="DisplayRouteSettingsCacheTrigger" />
        /// </summary>
        public static readonly string SitemapNodeRootCacheKey = $"{AreaName}.Root";
    }
}
