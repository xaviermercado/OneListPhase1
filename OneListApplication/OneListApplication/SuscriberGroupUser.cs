//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OneListApplication
{
    using System;
    using System.Collections.Generic;
    
    public partial class SuscriberGroupUser
    {
        public int SuscriberGroupUserID { get; set; }
        public int SuscriberGroupID { get; set; }
        public string UserID { get; set; }
        public int UserTypeID { get; set; }
        public string ListUserStatus { get; set; }
        public string SuscriptionDate { get; set; }
    
        public virtual SuscriberGroup SuscriberGroup { get; set; }
        public virtual User User { get; set; }
    }
}