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
    
    public partial class User
    {
        public User()
        {
            this.Profiles = new HashSet<Profile>();
            this.UserBooks = new HashSet<UserBook>();
            this.UserRoles = new HashSet<UserRole>();
        }
    
        public int Id { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime LastAccessDate { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual ICollection<Profile> Profiles { get; set; }
        public virtual ICollection<UserBook> UserBooks { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}