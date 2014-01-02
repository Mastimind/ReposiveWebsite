using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskFollowUp.Data;

namespace TaskFollowUp.Models
{
    public class BugModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Status { get; set; }
        public int TaskId { get; set; }

        public  Task Task { get; set; }
    }
}