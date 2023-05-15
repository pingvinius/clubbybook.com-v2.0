namespace ClubbyBook.Business.Repositories.SelectCriteria
{
    using System;
    using System.Linq;
    using ClubbyBook.Data.Models;
    using Pingvinius.Framework.Repositories.SelectCriteria;

    public static class GenreSelectCriteria
    {
        #region Where Routine

        public sealed class UrlRewrite : BaseUrlRewriteSelectCriteria<Genre>
        {
            public UrlRewrite(string urlRewrite)
                : base(urlRewrite)
            {
            }

            public override IQueryable<Genre> Apply(IQueryable<Genre> query)
            {
                return query.Where(g => g.UrlRewrite.Equals(this.UrlRewrite, StringComparison.InvariantCultureIgnoreCase));
            }
        }

        public sealed class Duplicated : BaseWhereSelectCriteria<Genre>
        {
            public Genre Genre { get; private set; }

            public Duplicated(Genre genre)
            {
                this.Genre = genre;
            }

            public override IQueryable<Genre> Apply(IQueryable<Genre> query)
            {
                return query.Where(e => e.Id != this.Genre.Id && e.UrlRewrite.Equals(this.Genre.UrlRewrite, StringComparison.OrdinalIgnoreCase));
            }
        }

        #endregion Where Routine

        #region OrderBy Routine

        public sealed class NameSort : BaseOrderBySelectCriteria<Genre>
        {
            public override IQueryable<Genre> Apply(IQueryable<Genre> query)
            {
                return query.OrderBy(entity => entity.Name);
            }
        }

        #endregion OrderBy Routine
    }
}