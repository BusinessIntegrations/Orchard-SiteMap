#region Using
using WebAdvanced.Sitemap.Models;
#endregion

namespace WebAdvanced.Sitemap.Recipes {
    public static class RecipeConstants {
        public const string CustomRouteElementName = "CustomRoute";
        public const string CustomRouteIndexForDisplayName = nameof(SitemapCustomRouteRecord.IndexForDisplay);
        public const string CustomRouteIndexForXmlName = nameof(SitemapCustomRouteRecord.IndexForXml);
        public const string CustomRoutePriorityName = nameof(SitemapCustomRouteRecord.Priority);
        public const string CustomRoutesElementName = "CustomRoutes";
        public const string CustomRouteUpdateFrequencyName = nameof(SitemapCustomRouteRecord.UpdateFrequency);
        public const string CustomRouteUrlName = nameof(SitemapCustomRouteRecord.Url);
        public const string OrchardElementName = "Orchard";
        public const string RouteActiveName = nameof(SitemapRouteRecord.Active);
        public const string RouteDisplayColumnName = nameof(SitemapRouteRecord.DisplayColumn);
        public const string RouteDisplayLevelsName = nameof(SitemapRouteRecord.DisplayLevels);
        public const string RouteElementName = "Route";
        public const string RoutesElementName = "Routes";
        public const string RouteSlugName = nameof(SitemapRouteRecord.Slug);
        public const string RouteWeightName = nameof(SitemapRouteRecord.Weight);
        public const string SettingElementName = "Setting";
        public const string SettingsContentTypeName = nameof(SitemapSettingsRecord.ContentType);
        public const string SettingsElementName = "Settings";
        public const string SettingsIndexForDisplayName = nameof(SitemapSettingsRecord.IndexForDisplay);
        public const string SettingsIndexForXmlName = nameof(SitemapSettingsRecord.IndexForXml);
        public const string SettingsPriorityName = nameof(SitemapSettingsRecord.Priority);
        public const string SettingsUpdateFrequencyName = nameof(SitemapSettingsRecord.UpdateFrequency);
        public const string WebAdvancedSitemapName = "WebAdvancedSitemap";
    }
}
