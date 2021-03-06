﻿#region Using
using System.Linq;
using System.Xml.Linq;
using Orchard.Data;
using Orchard.Localization;
using Orchard.Recipes.Services;
using WebAdvanced.Sitemap.Models;
#endregion

namespace WebAdvanced.Sitemap.Recipes.Builders {
    public class SitemapStep : RecipeBuilderStep {
        private readonly IRepository<SitemapCustomRouteRecord> _customRouteRepository;
        private readonly IRepository<SitemapRouteRecord> _routeRepository;
        private readonly IRepository<SitemapSettingsRecord> _settingsRepository;

        public SitemapStep(IRepository<SitemapRouteRecord> routeRepository,
                           IRepository<SitemapSettingsRecord> settingsRepository,
                           IRepository<SitemapCustomRouteRecord> customRouteRepository) {
            _routeRepository = routeRepository;
            _settingsRepository = settingsRepository;
            _customRouteRepository = customRouteRepository;
        }

        #region Properties
        public override LocalizedString Description => T("Exports Web Advanced sitemap definitions.");
        public override LocalizedString DisplayName => T("Web Advanced Sitemap");
        public override string Name => "WebAdvancedSitemap";
        #endregion

        #region Methods
        public override void Build(BuildContext context) {
            var root = new XElement("WebAdvancedSitemap");
            BuildRoutes(root);
            BuildSettings(root);
            BuildCustomRoutes(root);
            if (root.HasElements) {
                var xElement = context.RecipeDocument.Element("Orchard");
                xElement?.Add(root);
            }
        }

        private void BuildCustomRoutes(XContainer root) {
            var customRouteDefinitions = _customRouteRepository.Table.ToList();
            if (!customRouteDefinitions.Any()) {
                return;
            }
            var customRoutes = new XElement("CustomRoutes");
            root.Add(customRoutes);
            foreach (var customRouteDefinition in customRouteDefinitions.OrderBy(x => x.Url)) {
                customRoutes.Add(new XElement("CustomRoute",
                    new XAttribute("Url", customRouteDefinition.Url),
                    new XAttribute("IndexForDisplay", customRouteDefinition.IndexForDisplay),
                    new XAttribute("IndexForXml", customRouteDefinition.IndexForXml),
                    new XAttribute("UpdateFrequency", customRouteDefinition.UpdateFrequency),
                    new XAttribute("Priority", customRouteDefinition.Priority)));
            }
        }

        private void BuildRoutes(XContainer root) {
            var routeDefinitions = _routeRepository.Table.ToList();
            if (!routeDefinitions.Any()) {
                return;
            }
            var routes = new XElement("Routes");
            root.Add(routes);
            foreach (var routeDefinition in routeDefinitions.OrderBy(x => x.Slug)) {
                routes.Add(new XElement("Route",
                    new XAttribute("Slug", routeDefinition.Slug),
                    new XAttribute("DisplayLevels", routeDefinition.DisplayLevels),
                    new XAttribute("Active", routeDefinition.Active),
                    new XAttribute("DisplayColumn", routeDefinition.DisplayColumn),
                    new XAttribute("Weight", routeDefinition.Weight)));
            }
        }

        private void BuildSettings(XContainer root) {
            var settingsDefinitions = _settingsRepository.Table.ToList();
            if (!settingsDefinitions.Any()) {
                return;
            }
            var settings = new XElement("Settings");
            root.Add(settings);
            foreach (var settingsDefinition in settingsDefinitions.OrderBy(x => x.ContentType)) {
                settings.Add(new XElement("Setting",
                    new XAttribute("ContentType", settingsDefinition.ContentType),
                    new XAttribute("IndexForDisplay", settingsDefinition.IndexForDisplay),
                    new XAttribute("IndexForXml", settingsDefinition.IndexForXml),
                    new XAttribute("UpdateFrequency", settingsDefinition.UpdateFrequency),
                    new XAttribute("Priority", settingsDefinition.Priority)));
            }
        }
        #endregion
    }
}
