//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TicketWebappAireLogic.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class commentTable
    {
        public int commentID { get; set; }
        public string commentContents { get; set; }
        public string commentTime { get; set; }
        public Nullable<int> ticketID { get; set; }
        public Nullable<int> userID { get; set; }
    
        public  ticketTable ticketTable { get; set; }
        public  userTable userTable { get; set; }
    }
}
