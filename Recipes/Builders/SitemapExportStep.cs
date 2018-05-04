#region Using
using System.Linq;
using System.Xml.Linq;
using Orchard.Data;
using Orchard.Localization;
using Orchard.Recipes.Services;
using WebAdvanced.Sitemap.Models;
#endregion

namespace WebAdvanced.Sitemap.Recipes.Builders {
    public class SitemapExportStep : RecipeBuilderStep {
        private readonly IRepository<SitemapCustomRouteRecord> _customRouteRepository;
        private readonly IRepository<SitemapRouteRecord> _routeRepository;
        private readonly IRepository<SitemapSettingsRecord> _settingsRepository;

        public SitemapExportStep(IRepository<SitemapRouteRecord> routeRepository,
                                 IRepository<SitemapSettingsRecord> settingsRepository,
                                 IRepository<SitemapCustomRouteRecord> customRouteRepository) {
            _routeRepository = routeRepository;
            _settingsRepository = settingsRepository;
            _customRouteRepository = customRouteRepository;
        }

        #region Properties
        public override LocalizedString Description => T("Exports Web Advanced sitemap definitions.");
        public override LocalizedString DisplayName => T("Web Advanced Sitemap");
        public override string Name => RecipeConstants.WebAdvancedSitemapName;
        #endregion

        #region Methods
        public override void Build(BuildContext context) {
            var root = new XElement(RecipeConstants.WebAdvancedSitemapName);
            BuildRoutes(root);
            BuildSettings(root);
            BuildCustomRoutes(root);
            if (root.HasElements) {
                var xElement = context.RecipeDocument.Element(RecipeConstants.OrchardElementName);
                xElement?.Add(root);
            }
        }

        private void BuildCustomRoutes(XContainer root) {
            var customRouteDefinitions = _customRouteRepository.Table.ToList();
            if (!customRouteDefinitions.Any()) {
                return;
            }

            var customRoutes = new XElement(RecipeConstants.CustomRoutesElementName);
            root.Add(customRoutes);
            foreach (var customRouteDefinition in customRouteDefinitions.OrderBy(x => x.Url)) {
                customRoutes.Add(new XElement(RecipeConstants.CustomRouteElementName,
                    new XAttribute(RecipeConstants.CustomRouteUrlName, customRouteDefinition.Url),
                    new XAttribute(RecipeConstants.CustomRouteIndexForDisplayName, customRouteDefinition.IndexForDisplay),
                    new XAttribute(RecipeConstants.CustomRouteIndexForXmlName, customRouteDefinition.IndexForXml),
                    new XAttribute(RecipeConstants.CustomRouteUpdateFrequencyName, customRouteDefinition.UpdateFrequency),
                    new XAttribute(RecipeConstants.CustomRoutePriorityName, customRouteDefinition.Priority)));
            }
        }

        private void BuildRoutes(XContainer root) {
            var routeDefinitions = _routeRepository.Table.ToList();
            if (!routeDefinitions.Any()) {
                return;
            }

            var routes = new XElement(RecipeConstants.RoutesElementName);
            root.Add(routes);
            foreach (var routeDefinition in routeDefinitions.OrderBy(x => x.Slug)) {
                routes.Add(new XElement(RecipeConstants.RouteElementName,
                    new XAttribute(RecipeConstants.RouteSlugName, routeDefinition.Slug),
                    new XAttribute(RecipeConstants.RouteDisplayLevelsName, routeDefinition.DisplayLevels),
                    new XAttribute(RecipeConstants.RouteActiveName, routeDefinition.Active),
                    new XAttribute(RecipeConstants.RouteDisplayColumnName, routeDefinition.DisplayColumn),
                    new XAttribute(RecipeConstants.RouteWeightName, routeDefinition.Weight)));
            }
        }

        private void BuildSettings(XContainer root) {
            var settingsDefinitions = _settingsRepository.Table.ToList();
            if (!settingsDefinitions.Any()) {
                return;
            }

            var settings = new XElement(RecipeConstants.SettingsElementName);
            root.Add(settings);
            foreach (var settingsDefinition in settingsDefinitions.OrderBy(x => x.ContentType)) {
                settings.Add(new XElement(RecipeConstants.SettingElementName,
                    new XAttribute(RecipeConstants.SettingsContentTypeName, settingsDefinition.ContentType),
                    new XAttribute(RecipeConstants.SettingsIndexForDisplayName, settingsDefinition.IndexForDisplay),
                    new XAttribute(RecipeConstants.SettingsIndexForXmlName, settingsDefinition.IndexForXml),
                    new XAttribute(RecipeConstants.SettingsUpdateFrequencyName, settingsDefinition.UpdateFrequency),
                    new XAttribute(RecipeConstants.SettingsPriorityName, settingsDefinition.Priority)));
            }
        }
        #endregion
    }
}
