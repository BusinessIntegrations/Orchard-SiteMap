#region Using
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Orchard;
using Orchard.Caching;
using Orchard.DisplayManagement;
using Orchard.Localization;
using Orchard.UI.Notify;
using WebAdvanced.Sitemap.Services;
using WebAdvanced.Sitemap.ViewModels;
#endregion

namespace WebAdvanced.Sitemap.Controllers {
    public class AdminController : Controller {
        private readonly INotifier _notifier;
        private readonly IOrchardServices _services;
        private readonly ISignals _signals;
        private readonly IAdvancedSitemapService _sitemapService;

        public AdminController(IAdvancedSitemapService sitemapService,
                               IShapeFactory shapeFactory,
                               INotifier notifier,
                               IOrchardServices services,
                               ISignals signals) {
            _sitemapService = sitemapService;
            _notifier = notifier;
            _services = services;
            _signals = signals;
            Shape = shapeFactory;
            T = NullLocalizer.Instance;
        }

        #region Properties
        public dynamic Shape { get; set; }
        public Localizer T { get; set; }
        #endregion

        #region Methods
        public ActionResult Cache() {
            if (!_services.Authorizer.Authorize(Permissions.ManageSitemap, T(Constants.NotAllowedToManageSitemap))) {
                return new HttpUnauthorizedResult();
            }

            return View();
        }

        public ActionResult DisplaySettings() {
            if (!_services.Authorizer.Authorize(Permissions.ManageSitemap, T(Constants.NotAllowedToManageSitemap))) {
                return new HttpUnauthorizedResult();
            }

            var routes = _sitemapService.GetDisplayRouteSettings();
            var model = new DisplaySettingsPageModel {
                AutoLayout = false,
                Routes = routes.ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult DisplaySettings(DisplaySettingsPageModel model) {
            if (!_services.Authorizer.Authorize(Permissions.ManageSitemap, T(Constants.NotAllowedToManageSitemap))) {
                return new HttpUnauthorizedResult();
            }

            _sitemapService.SetDisplayRouteSettings(model.Routes);
            _services.Notifier.Add(NotifyType.Information, T("Saved Sitemap display layout"));
            return RedirectToAction("DisplaySettings");
        }

        public ActionResult GetNewCustomRouteForm() {
            var emptyModel = new CustomRouteSettingsModel {
                IndexForDisplay = false,
                IndexForXml = false,
                Name = string.Empty,
                Priority = 3,
                UpdateFrequency = Constants.WeeklyUpdateFrequency,
                Url = string.Empty
            };
            return PartialView("PartialCustomRouteEditor", emptyModel);
        }

        public ActionResult Indexing() {
            if (!_services.Authorizer.Authorize(Permissions.ManageSitemap, T(Constants.NotAllowedToManageSitemap))) {
                return new HttpUnauthorizedResult();
            }

            var typeSettings = _sitemapService.GetContentTypeRouteSettings();
            var customRoutes = _sitemapService.GetCustomRouteSettings();
            var model = new IndexingPageModel {
                ContentTypeSettings = typeSettings.OrderBy(q => q.DisplayName)
                    .ToList(),
                CustomRoutes = customRoutes.ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Indexing(IndexingPageModel model) {
            if (!_services.Authorizer.Authorize(Permissions.ManageSitemap, T(Constants.NotAllowedToManageSitemap))) {
                return new HttpUnauthorizedResult();
            }

            if (model.CustomRoutes == null) {
                model.CustomRoutes = new List<CustomRouteSettingsModel>();
            }

            _sitemapService.SetContentTypeRouteSettings(model.ContentTypeSettings);
            _sitemapService.SetCustomRouteSettings(model.CustomRoutes);
            _sitemapService.ReleaseDisplayRouteSettingsCache();
            _services.Notifier.Add(NotifyType.Information, T("Saved Sitemap indexing settings"));
            return RedirectToAction("Indexing");
        }

        public ActionResult RefreshCache() {
            if (!_services.Authorizer.Authorize(Permissions.ManageSitemap, T(Constants.NotAllowedToManageSitemap))) {
                return new HttpUnauthorizedResult();
            }

            _sitemapService.RefreshCache();
            _notifier.Add(NotifyType.Information, T("Sitemap cache cleared"));
            return RedirectToAction("Indexing");
        }
        #endregion
    }
}
