#region Using
using Orchard.Localization;
using Orchard.UI.Navigation;
#endregion

namespace WebAdvanced.Sitemap {
    public class AdminMenu : INavigationProvider {
        public AdminMenu() {
            T = NullLocalizer.Instance;
        }

        #region INavigationProvider Members
        public void GetNavigation(NavigationBuilder builder) {
            builder.Add(T("Sitemap"),
                "2.0.0",
                navigationItemBuilder => navigationItemBuilder.Permission(Permissions.ManageSitemap)
                    .LinkToFirstChild(true)
                    .Add(T("Indexing"), "1", item => Describe(item, "Indexing", Constants.ControllerName, Constants.AreaName))
                    .Add(T("Display"), "2", item => Describe(item, "DisplaySettings", Constants.ControllerName, Constants.AreaName))
                    .Add(T("Cache"), "3", item => Describe(item, "Cache", Constants.ControllerName, Constants.AreaName)),
                new[] {"bi-menu-section"});
        }

        public string MenuName => "admin";
        #endregion

        #region Properties
        public Localizer T { get; set; }
        #endregion

        #region Methods
        private static void Describe(NavigationItemBuilder item, string actionName, string controllerName, string areaName) {
            item.Action(actionName,
                    controllerName,
                    new {
                        area = areaName
                    })
                .LocalNav()
                .Permission(Permissions.ManageSitemap);
        }
        #endregion
    }
}
