namespace ClubbyBook.Business.Repositories.Interfaces
{
    using ClubbyBook.Data.Models;
    using Pingvinius.Framework.Repositories;

    public interface INewsRepository : IUrlRewriteEntityRepository<News>
    {
    }
}