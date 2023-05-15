namespace Pingvinius.Framework.DbContext
{
    public interface IDbContextFactory
    {
        IDbContext Create();
    }
}