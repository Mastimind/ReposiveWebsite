using System.Web;
using System.IO;
using System.Web.Mvc;

namespace TaskFollowUp.Internal
{
    /// <summary>
    /// Provides an interface that components can use to resolve paths in a web application
    /// </summary>
    public interface IPathFinder
    {
        string Resolve(string virtualPath);
        string MapToFileSystem(string virtualPath);
        string[] FindFiles(string directory, string filter, SearchOption options);
    }

    public class PathFinder : IPathFinder
    {
        private readonly HttpContextBase _context;
        private readonly UrlHelper _urlHelper;

        public PathFinder(HttpContextBase context, UrlHelper urlHelper)
        {
            _context = context;
            _urlHelper = urlHelper;
        }

        public string Resolve(string virtualPath)
        {
            return _urlHelper.Content(virtualPath);
        }

        public string MapToFileSystem(string virtualPath)
        {
            return _context.Server.MapPath(virtualPath);
        }

        public string[] FindFiles(string directory, string filter, SearchOption options)
        {
            return Directory.GetFiles(directory, filter, options);
        }
    }
}