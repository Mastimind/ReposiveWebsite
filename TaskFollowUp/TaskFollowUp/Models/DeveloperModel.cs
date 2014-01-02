using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskFollowUp.Data;

namespace TaskFollowUp.Models
{
    public class DeveloperModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

         public virtual List<Task> Tasks { get; set; }

    }
}