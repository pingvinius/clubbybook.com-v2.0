namespace Pingvinius.Framework.DbContext
{
    public interface IDbContextStore
    {
        void Add(IDbContext context);

        void Remove();

        bool Contains();

        IDbContext Get();
    }
}