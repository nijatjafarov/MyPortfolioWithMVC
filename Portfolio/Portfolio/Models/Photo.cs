//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Portfolio.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    public partial class Photo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
    
        public virtual Project Project { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
    }
}
