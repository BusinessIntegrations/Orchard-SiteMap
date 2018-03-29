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
            builder.Add(T("BI Sitemap"),
                "4.0",
                menu => menu /*.LinkToFirstChild(false)*/.Permission(Permissions.ManageSitemap)
                    .Add(T("Indexing"),
                        "1",
                        item => item.Action("Indexing",
                                "Admin",
                                new {
                                    area = "WebAdvanced.Sitemap"
                                })
                            .LocalNav()
                            .Permission(Permissions.ManageSitemap))
                    .Add(T("Display"),
                        "2",
                        item => item.Action("DisplaySettings",
                                "Admin",
                                new {
                                    area = "WebAdvanced.Sitemap"
                                })
                            .LocalNav()
                            .Permission(Permissions.ManageSitemap))
                    .Add(T("Cache"),
                        "3",
                        item => item.Action("Cache",
                                "Admin",
                                new {
                                    area = "WebAdvanced.Sitemap"
                                })
                            .LocalNav()
                            .Permission(Permissions.ManageSitemap)));
        }

        public string MenuName => "admin";
        #endregion

        #region Properties
        public Localizer T { get; set; }
        #endregion
    }
}
