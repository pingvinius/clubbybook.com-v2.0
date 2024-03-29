//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClubbyBook.Data.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ConversationNotification
    {
        public int Id { get; set; }
        public int NotificationId { get; set; }
        public bool IsNew { get; set; }
        public sbyte sbDirection { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
    
        public virtual User FromUser { get; set; }
        public virtual Notification Notification { get; set; }
        public virtual User ToUser { get; set; }
    }
}
