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
    
    public partial class Country
    {
        public Country()
        {
            this.Cities = new HashSet<City>();
            this.Districts = new HashSet<District>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<District> Districts { get; set; }
    }
}
