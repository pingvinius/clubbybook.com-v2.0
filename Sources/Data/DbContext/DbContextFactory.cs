namespace ClubbyBook.Data.DbContext
{
    using ClubbyBook.Common;
    using ClubbyBook.Data.Models;
    using Pingvinius.Framework.DbContext;

    public sealed class DbContextFactory : IDbContextFactory
    {
        #region IDbContextFactory Members

        IDbContext IDbContextFactory.Create()
        {
            return new ExtendedDbContext(Settings.EntityFrameworkConnectionString);
        }

        #endregion IDbContextFactory Members
    }
}