#region Using
using System;
using System.Xml.Linq;
using Orchard.Data;
using Orchard.Logging;
using Orchard.Recipes.Models;
using Orchard.Recipes.Services;
using WebAdvanced.Sitemap.Models;
#endregion

// ReSharper disable PossibleNullReferenceException
namespace WebAdvanced.Sitemap.Recipes.Executors {
    public class SitemapImportStep : RecipeExecutionStep {
        private readonly IRepository<SitemapCustomRouteRecord> _customRouteRepository;
        private readonly IRepository<SitemapRouteRecord> _routeRepository;
        private readonly IRepository<SitemapSettingsRecord> _settingsRepository;

        public SitemapImportStep(IRepository<SitemapRouteRecord> routeRepository,
                                 IRepository<SitemapSettingsRecord> settingsRepository,
                                 IRepository<SitemapCustomRouteRecord> customRouteRepository,
                                 RecipeExecutionLogger logger)
            : base(logger) {
            _routeRepository = routeRepository;
            _settingsRepository = settingsRepository;
            _customRouteRepository = customRouteRepository;
        }

        #region Properties
        public override string Name => RecipeConstants.WebAdvancedSitemapName;
        #endregion

        #region Methods
        public override void Execute(RecipeExecutionContext context) {
            ProcessRoutes(context.RecipeStep.Step);
            ProcessSettings(context.RecipeStep.Step);
            ProcessCustomRoutes(context.RecipeStep.Step);
        }

        private SitemapCustomRouteRecord GetOrCreateCustomRouteDefinition(string url) {
            var customRouteDefinition = _customRouteRepository.Get(x => x.Url == url);
            if (customRouteDefinition == null) {
                customRouteDefinition = new SitemapCustomRouteRecord {
                    Url = url
                };
                _customRouteRepository.Create(customRouteDefinition);
            }

            return customRouteDefinition;
        }

        private SitemapRouteRecord GetOrCreateRouteDefinition(string slug) {
            var routeDefinition = _routeRepository.Get(x => x.Slug == slug);
            if (routeDefinition == null) {
                routeDefinition = new SitemapRouteRecord {
                    Slug = slug
                };
                _routeRepository.Create(routeDefinition);
            }

            return routeDefinition;
        }

        private SitemapSettingsRecord GetOrCreateSettingDefinition(string contentType) {
            var settingDefinition = _settingsRepository.Get(x => x.ContentType == contentType);
            if (settingDefinition == null) {
                settingDefinition = new SitemapSettingsRecord {
                    ContentType = contentType
                };
                _settingsRepository.Create(settingDefinition);
            }

            return settingDefinition;
        }

        private void ProcessCustomRoutes(XContainer root) {
            var customRoutesDefinitionsElement = root.Element(RecipeConstants.CustomRoutesElementName);
            if (customRoutesDefinitionsElement == null) {
                return;
            }

            foreach (var customRouteDefinitionElement in customRoutesDefinitionsElement.Elements()) {
                var customRouteUrl = customRouteDefinitionElement.Attribute(RecipeConstants.CustomRouteUrlName)
                    .Value;
                Logger.Information("Importing custom route '{0}'.", customRouteUrl);
                try {
                    var customRouteDefinition = GetOrCreateCustomRouteDefinition(customRouteUrl);
                    customRouteDefinition.IndexForDisplay = bool.Parse(customRouteDefinitionElement
                        .Attribute(RecipeConstants.CustomRouteIndexForDisplayName)
                        .Value);
                    customRouteDefinition.IndexForXml = bool.Parse(customRouteDefinitionElement
                        .Attribute(RecipeConstants.CustomRouteIndexForXmlName)
                        .Value);
                    customRouteDefinition.UpdateFrequency = customRouteDefinitionElement.Attribute(RecipeConstants.CustomRouteUpdateFrequencyName)
                        .Value;
                    customRouteDefinition.Priority = int.Parse(customRouteDefinitionElement.Attribute(RecipeConstants.CustomRoutePriorityName)
                        .Value);
                }
                catch (Exception ex) {
                    Logger.Error(ex, "Error while importing custom route '{0}'.", customRouteUrl);
                    throw;
                }
            }
        }

        private void ProcessRoutes(XContainer root) {
            var routeDefinitionsElement = root.Element(RecipeConstants.RouteElementName);
            if (routeDefinitionsElement == null) {
                return;
            }

            foreach (var routeDefinitionElement in routeDefinitionsElement.Elements()) {
                var routeSlug = routeDefinitionElement.Attribute(RecipeConstants.RouteSlugName)
                    .Value;
                Logger.Information("Importing route '{0}'.", routeSlug);
                try {
                    var routeDefinition = GetOrCreateRouteDefinition(routeSlug);
                    routeDefinition.DisplayLevels = int.Parse(routeDefinitionElement.Attribute(RecipeConstants.RouteDisplayLevelsName)
                        .Value);
                    routeDefinition.Active = bool.Parse(routeDefinitionElement.Attribute(RecipeConstants.RouteActiveName)
                        .Value);
                    routeDefinition.DisplayColumn = int.Parse(routeDefinitionElement.Attribute(RecipeConstants.RouteDisplayColumnName)
                        .Value);
                    routeDefinition.Weight = int.Parse(routeDefinitionElement.Attribute(RecipeConstants.RouteWeightName)
                        .Value);
                }
                catch (Exception ex) {
                    Logger.Error(ex, "Error while importing route '{0}'.", routeSlug);
                    throw;
                }
            }
        }

        private void ProcessSettings(XContainer root) {
            var settingsDefinitionsElement = root.Element(RecipeConstants.SettingElementName);
            if (settingsDefinitionsElement == null) {
                return;
            }

            foreach (var settingDefinitionElement in settingsDefinitionsElement.Elements()) {
                var settingContentType = settingDefinitionElement.Attribute(RecipeConstants.SettingsContentTypeName)
                    .Value;
                Logger.Information("Importing settings '{0}'.", settingContentType);
                try {
                    var settingDefinition = GetOrCreateSettingDefinition(settingContentType);
                    settingDefinition.IndexForDisplay = bool.Parse(settingDefinitionElement.Attribute(RecipeConstants.SettingsIndexForDisplayName)
                        .Value);
                    settingDefinition.IndexForXml = bool.Parse(settingDefinitionElement.Attribute(RecipeConstants.SettingsIndexForXmlName)
                        .Value);
                    settingDefinition.UpdateFrequency = settingDefinitionElement.Attribute(RecipeConstants.SettingsUpdateFrequencyName)
                        .Value;
                    settingDefinition.Priority = int.Parse(settingDefinitionElement.Attribute(RecipeConstants.SettingsPriorityName)
                        .Value);
                }
                catch (Exception ex) {
                    Logger.Error(ex, "Error while importing setting '{0}'.", settingContentType);
                    throw;
                }
            }
        }
        #endregion
    }
}
