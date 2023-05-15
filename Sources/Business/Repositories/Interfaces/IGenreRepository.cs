namespace ClubbyBook.Business.Repositories.Interfaces
{
    using System.Collections.Generic;
    using ClubbyBook.Data.Models;
    using Pingvinius.Framework.Repositories;
    using Pingvinius.Framework.Tree;

    public interface IGenreRepository : IUrlRewriteEntityRepository<Genre>
    {
        IEnumerable<FlatTreeItem<Genre>> GetFlatTree();
    }
}