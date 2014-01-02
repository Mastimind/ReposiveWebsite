using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskFollowUp.Models
{
    public class GenericModel<T>
    {
        public string Title { get; set; }

        public T[] Items { get; set; }
    }
}