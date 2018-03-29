#region Using
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
#endregion

namespace WebAdvanced.Sitemap.Controllers {
    public class XmlResult : ActionResult {
        public XmlResult(XDocument document) {
            Document = document;
        }

        #region Properties
        public XDocument Document { get; }
        #endregion

        #region Methods
        public override void ExecuteResult(ControllerContext context) {
            // not returning application/rss+xml because of
            // https://bugzilla.mozilla.org/show_bug.cgi?id=256379
            context.HttpContext.Response.ContentType = "text/xml";
            using (var writer = XmlWriter.Create(context.HttpContext.Response.Output)) {
                Document.WriteTo(writer);
            }
        }
        #endregion
    }
}
