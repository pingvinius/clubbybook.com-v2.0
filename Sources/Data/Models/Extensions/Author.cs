namespace ClubbyBook.Data.Models
{
    using Pingvinius.Framework.Entities;
    using Pingvinius.Framework.Utilities;

    public partial class Author : IEntity, ITrackable, IUrlRewriteable
    {
        public AuthorType Type
        {
            get
            {
                return (AuthorType)sbType;
            }
            set
            {
                sbType = (sbyte)value;
            }
        }

        public Gender Gender
        {
            get
            {
                return (Gender)sbGender;
            }
            set
            {
                sbGender = (sbyte)value;
            }
        }

        #region IUrlRewriteable Members

        public string GenerateUrlRewriteString()
        {
            return LatinizeHelper.Latinize(this.FullName);
        }

        #endregion IUrlRewriteable Members
    }
}