//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TaskFollowUp.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Task
    {
        public Task()
        {
            this.Bugs = new HashSet<Bug>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public System.DateTime StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public int SprintId { get; set; }
        public Nullable<int> DeveloperId { get; set; }
    
        public virtual Developer AssignedDeveloper { get; set; }
        public virtual Sprint Sprint { get; set; }
        public virtual ICollection<Bug> Bugs { get; set; }
    }
}
