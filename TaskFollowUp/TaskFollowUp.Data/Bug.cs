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
    
    public partial class Bug
    {
        public int Id { get; set; }
        public string Tilte { get; set; }
        public bool Status { get; set; }
        public int TaskId { get; set; }
    
        public virtual Task Task { get; set; }
    }
}
