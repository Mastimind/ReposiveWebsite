using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskFollowUp.Internal;

namespace TaskFollowUp.Controllers
{
    public class ResourceController : Controller
    {
        private readonly IPathFinder _path;
        

        public ResourceController(IPathFinder path)
        {
            _path = path;
        }

        [ReleaseOutputCache(Duration = 120)]
        public ActionResult JavaScriptBundle()
        {
            string content;
            if (HttpContext.IsDebuggingEnabled)
            {
                content = ReadApplicationScript("~/scripts/main.js");
                return Content(content, "application/javascript");
            }
            content = ReadApplicationScript("~/scripts/main-built.js");
            return Content(content, "application/javascript");
        }

        [ReleaseOutputCache(Duration = 120)]
        public ActionResult CssBundle()
        {
            if (HttpContext.IsDebuggingEnabled)
            {
                return File("~/Content/Application.css", "text/css");
            }
            return File("~/Content/Application-built.css", "text/css");
        }

        public ActionResult DesktopManifest()
        {
            var lastUpdated = DateTime.Now.ToLongTimeString();

            var helper = new CacheManifestBuilder(_path);

            if (lastUpdated != null)
                helper.Timestamp = lastUpdated;

            helper.AddDirectory("~/Images", "*.png", false, SearchOption.AllDirectories);
            helper.AddDirectory("~/img", "*.png", false, SearchOption.AllDirectories);
            helper.AddFile("~/scripts/require.js", false);
            helper.AddFile("~/Content/application-built.css", false);
            helper.AddFile("~/scripts/main-built.js", false);
            helper.AddLine(Url.Content("~/api/sprint/Get"), CacheManifestSection.Cache);
            helper.AddLine("*", CacheManifestSection.Network);

            return Content(helper.ToString(), "text/cache-manifest");
        }

        private string ReadApplicationScript(string file)
        {
            return System.IO.File.ReadAllText(_path.MapToFileSystem(file)).Replace("%ApplicationPath%", Url.Content("~/"));
        }

    }
}
