using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskFollowUp.Data;

namespace TaskFollowUp.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public System.DateTime StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public int SprintId { get; set; }
        public Nullable<int> DeveloperId { get; set; }

        public  Sprint Sprint { get; set; }
        public  Developer AssignedTo { get; set; }
        public  List<Bug> Bugs { get; set; }
    }
}