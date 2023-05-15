namespace ClubbyBook.Data.Models
{
    using Pingvinius.Framework.Entities;
    using Pingvinius.Framework.Utilities;

    public partial class Book : IEntity, ITrackable, IUrlRewriteable
    {
        #region IUrlRewriteable Members

        public string GenerateUrlRewriteString()
        {
            return this.Confirmed ? LatinizeHelper.Latinize(this.Title) : LatinizeHelper.Latinize(this.Title.GetHashCode().ToString("x"));
        }

        #endregion IUrlRewriteable Members
    }
}