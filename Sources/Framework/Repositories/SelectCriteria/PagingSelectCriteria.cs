namespace Pingvinius.Framework.Repositories.SelectCriteria
{
    using System.Linq;
    using Pingvinius.Framework.Entities;

    /// <summary>
    /// The paging select criteria.
    /// Should be used in case if it is necessary to use paging for select method of repository.
    /// </summary>
    /// <typeparam name="T">The type of entity. Should be a class which implements IEntity interface.</typeparam>
    public sealed class PagingSelectCriteria<T> : BaseSelectCriteria<T> where T : IEntity
    {
        /// <summary>
        /// Gets or sets the skip count.
        /// </summary>
        /// <value>
        /// The skip count.
        /// </value>
        public int? SkipCount { get; set; }

        /// <summary>
        /// Gets or sets the take count.
        /// </summary>
        /// <value>
        /// The take count.
        /// </value>
        public int? TakeCount { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagingSelectCriteria{T}"/> class.
        /// </summary>
        public PagingSelectCriteria()
            : base(SelectCriteriaType.Paging)
        {
        }

        /// <summary>
        /// Applies paging select criteria in specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>The query after applying select criteria.</returns>
        public override IQueryable<T> Apply(IQueryable<T> query)
        {
            if (this.SkipCount.HasValue)
            {
                query = query.Skip(this.SkipCount.Value);
            }

            if (this.TakeCount.HasValue)
            {
                query = query.Take(this.TakeCount.Value);
            }

            return query;
        }
    }
}