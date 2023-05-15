namespace ClubbyBook.Data.Models
{
    using System.Linq;
    using Pingvinius.Framework.Entities;

    public partial class User : IEntity, IDeletable
    {
        public Profile Profile
        {
            get
            {
                return this.Profiles.First();
            }
        }
    }
}