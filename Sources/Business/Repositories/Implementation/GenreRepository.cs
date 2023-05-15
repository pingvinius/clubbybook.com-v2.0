namespace ClubbyBook.Business.Repositories.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using ClubbyBook.Business.Repositories.Interfaces;
    using ClubbyBook.Business.Repositories.SelectCriteria;
    using ClubbyBook.Data.Models;
    using Pingvinius.Framework.Tree;

    internal sealed class GenreRepository : EntityRepository<Genre>, IGenreRepository
    {
        protected override DbSet<Genre> EntityList
        {
            get { return this.Context.Genres; }
        }

        protected override bool IsEntityUnique(Genre entity)
        {
            return this.Count(new GenreSelectCriteria.Duplicated(entity)) == 0;
        }

        protected override Genre CreateInstanceInternal()
        {
            throw new NotSupportedException();
        }

        protected override void SaveChangesInternal(Genre entity)
        {
            throw new NotSupportedException();
        }

        protected override void DeleteInternal(Genre entity)
        {
            throw new NotSupportedException();
        }

        #region IUrlRewriteEntityRepository<Genre> Members

        public Genre Get(string urlRewrite)
        {
            if (string.IsNullOrWhiteSpace(urlRewrite))
            {
                throw new ArgumentNullException("urlRewrite");
            }

            return this.Select(new GenreSelectCriteria.UrlRewrite(urlRewrite)).FirstOrDefault();
        }

        #endregion IUrlRewriteEntityRepository<Genre> Members

        #region IGenreRepository Members

        public IEnumerable<FlatTreeItem<Genre>> GetFlatTree()
        {
            // Get genres
            var genres = this.Select();

            // Create a tree of genres
            var rootTreeItem = TreeHelper.CreateTreeFromList<Genre>(genres);

            // Convert tree to flat model and return
            return TreeHelper.ConvertTreeToFlatModel(rootTreeItem);
        }

        #endregion
    }
}