namespace Pingvinius.Framework.Repositories.SelectCriteria
{
    using System.Collections.Generic;
    using System.Linq;
    using Pingvinius.Framework.Entities;

    /// <summary>
    /// The base select criteria class.
    /// All select criteria should inherit this class.
    /// </summary>
    /// <typeparam name="T">The entity type. Should be a class which implements IEntity interface.</typeparam>
    public abstract class BaseSelectCriteria<T> : ISelectCriteria<T> where T : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSelectCriteria{T}"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public BaseSelectCriteria(SelectCriteriaType type)
        {
            this.Type = type;
        }

        /// <summary>
        /// Converts one instance to the list.
        /// </summary>
        /// <returns>The list with one instance.</returns>
        public IList<ISelectCriteria<T>> ToList()
        {
            var list = new List<ISelectCriteria<T>>();
            list.Add(this);
            return list;
        }

        /// <summary>
        /// Gets the no results.
        /// </summary>
        /// <returns>IQueryable instance of no results.</returns>
        public IQueryable<T> GetNoResults()
        {
            return new List<T>().AsQueryable();
        }

        #region ISelectCriteria<T> Members

        /// <summary>
        /// Gets the type of select criteria.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public SelectCriteriaType Type { get; private set; }

        /// <summary>
        /// Applies select criteria in specified query.
        /// Applies default logic. Could be overridden in inner classes.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>The query after applying select criteria.</returns>
        public virtual IQueryable<T> Apply(IQueryable<T> query)
        {
            return query;
        }

        #endregion ISelectCriteria<T> Members
    }
}