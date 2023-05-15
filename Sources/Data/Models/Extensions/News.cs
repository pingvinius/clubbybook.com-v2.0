namespace ClubbyBook.Data.Models
{
    using Pingvinius.Framework.Entities;
    using Pingvinius.Framework.Utilities;

    public partial class News : IEntity, ITrackable, IUrlRewriteable
    {
        #region IUrlRewriteable Members

        public string GenerateUrlRewriteString()
        {
            return LatinizeHelper.Latinize(this.Title);
        }

        #endregion IUrlRewriteable Members
    }
}