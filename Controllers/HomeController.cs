#region Using
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Orchard.DisplayManagement;
using Orchard.Mvc;
using Orchard.Themes;
using WebAdvanced.Sitemap.Models;
using WebAdvanced.Sitemap.Services;
#endregion

namespace WebAdvanced.Sitemap.Controllers {
    public class HomeController : Controller {
        private readonly IAdvancedSitemapService _sitemapService;

        public HomeController(IAdvancedSitemapService sitemapService, IShapeFactory shapeFactory) {
            _sitemapService = sitemapService;
            Shape = shapeFactory;
        }

        #region Properties
        public dynamic Shape { get; set; }
        #endregion

        #region Methods
        [Themed]
        public ActionResult Index() {
            var root = _sitemapService.GetSitemapRoot();
            // Get Routes grouped by DisplayColumn
            var groupedSettings = _sitemapService.GetRoutes()
                .Where(route => route.Active && route.Slug != null && root.Children.ContainsKey(route.Slug))
                .GroupBy(route => route.DisplayColumn)
                .OrderBy(gs => gs.Key);
            var nextColumn = 1;
            var columnShapes = new List<dynamic>();
            foreach (var grouping in groupedSettings) {
                var thisController = this;
                // Add any empty columns
                for (var empty = nextColumn; empty < grouping.Key; empty++) {
                    columnShapes.Add(Shape.Sitemap_Column(Groups: Enumerable.Empty<dynamic>()));
                }
                var groupShapes = grouping.OrderBy(gs => gs.Weight)
                    .Select(r => thisController.BuildGroupShape(root.Children[r.Slug]));
                columnShapes.Add(Shape.Sitemap_Column(Groups: groupShapes));
                nextColumn = grouping.Key + 1;
            }
            return new ShapeResult(this, Shape.Sitemap_Index(Columns: columnShapes));
        }

        public ActionResult Xml() {
            return new XmlResult(_sitemapService.GetSitemapDocument());
        }

        private dynamic BuildGroupShape(SitemapNode node) {
            var childShapes = node.Children.Values.Select(BuildNodeShape)
                .ToList();
            return Shape.Sitemap_Group(Title: node.Title, Url: node.Url, Children: childShapes);
        }

        private dynamic BuildNodeShape(SitemapNode node) {
            var childShapes = node.Children.Values.Select(BuildNodeShape)
                .ToList();
            return Shape.Sitemap_Node(Title: node.Title, Url: node.Url, Children: childShapes);
        }
        #endregion
    }
}
