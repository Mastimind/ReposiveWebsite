// ReSharper disable RedundantUsingDirective

using System;
using System.Web.Mvc;

// ReSharper restore RedundantUsingDirective

namespace TaskFollowUp.Internal
{
#if !DEBUG
    public class ReleaseOutputCache : OutputCacheAttribute
    {
         
    }
#else
    public class ReleaseOutputCache : Attribute
    { //in debug, no output cache :)
        public int Duration { get; set; }
    }
#endif
}