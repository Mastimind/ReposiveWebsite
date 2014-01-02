using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace TaskFollowUp.Internal
{
    public enum CacheManifestSection
    {
        /// <summary>
        /// Explicitly cached resources
        /// </summary>
        Cache,
        /// <summary>
        /// Specify offline substitutes
        /// </summary>
        Fallback,
        /// <summary>
        /// Resources that are always online
        /// </summary>
        Network
    }

    public class CacheManifestBuilder
    {
        private readonly IPathFinder _pathFinder;
        private readonly string[] _fileBlacklist = new[] { "Thumbs.db" };

        public string Timestamp { get; set; }

        public CacheManifestBuilder(IPathFinder pathFinder)
        {
            _pathFinder = pathFinder;
            Paths = new Dictionary<CacheManifestSection, List<string>>();
        }

        public Dictionary<CacheManifestSection, List<string>> Paths { get; private set; } 

        public void AddFile(string path, bool version = true)
        {
            var resolvedPath = path.StartsWith("~") ? _pathFinder.Resolve(path) : path;
            if(!resolvedPath.Contains("v=") && version)
            {
                resolvedPath += string.Format("{0}v={1}", resolvedPath.Contains("?") ? "&" : "?", CacheVersionString);
            }
            AddLine(resolvedPath, CacheManifestSection.Cache);
        }

        public void AddDirectory(string directory, string filter = "*.*", bool version = true, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            var path = _pathFinder.MapToFileSystem(directory);
            var basePath = _pathFinder.MapToFileSystem("~/");
            foreach(var file in _pathFinder.FindFiles(path, filter, searchOption))
            {
                if(_fileBlacklist.Any(x => file.Contains(x)))
                    continue;
                var virtualDir = file.Replace(basePath, "").Replace("\\", "/");
                AddFile(string.Format("~/{0}", virtualDir), version);
            }
        }

        public void AddLine(string line, CacheManifestSection section)
        {
            List<string> list;
            if(Paths.ContainsKey(section))
                list = Paths[section];
            else
            {
                list = Paths[section] = new List<string>();
            }
            list.Add(line);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine("CACHE MANIFEST");

            //changes the cache manifest on a new build
            builder.AppendFormat("## Version: {0} {1}", WebApiApplication.LastModified.ToString("yyMMddhhmmss"), Timestamp);
            
            if(Paths.ContainsKey(CacheManifestSection.Cache))
            {
                builder.AppendLine();
                builder.AppendLine("CACHE:");
                builder.Append(string.Join(Environment.NewLine, Paths[CacheManifestSection.Cache]));
            }
            if(Paths.ContainsKey(CacheManifestSection.Network))
            {
                builder.AppendLine();
                builder.AppendLine("NETWORK:");
                builder.Append(string.Join(Environment.NewLine, Paths[CacheManifestSection.Network]));
            }
            if(Paths.ContainsKey(CacheManifestSection.Fallback))
            {
                builder.AppendLine();
                builder.AppendLine("FALLBACK:");
                builder.Append(string.Join(Environment.NewLine, Paths[CacheManifestSection.Fallback]));
            }
            return builder.ToString();
        }

        private static string CacheVersionString
        {
            get { return WebApiApplication.LastModified.ToString("yyMMddhh"); }
        }
    }
}