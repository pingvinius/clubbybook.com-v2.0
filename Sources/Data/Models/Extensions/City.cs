namespace ClubbyBook.Data.Models
{
    using System;
    using Pingvinius.Framework.Entities;

    public partial class City : IEntity, IUrlRewriteable
    {
        #region IUrlRewriteable Members

        public string GenerateUrlRewriteString()
        {
            throw new NotSupportedException();
        }

        #endregion IUrlRewriteable Members
    }
}