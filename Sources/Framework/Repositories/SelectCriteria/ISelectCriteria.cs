namespace Pingvinius.Framework.Repositories.SelectCriteria
{
    using System.Linq;
    using Pingvinius.Framework.Entities;

    /// <summary>
    /// The select criteria to be passed into select or count methods of repository.
    /// </summary>
    /// <typeparam name="T">The type of entity. Should be a class which implements IEntity interface.</typeparam>
    public interface ISelectCriteria<T> where T : IEntity
    {
        /// <summary>
        /// Gets the type of select criteria.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        SelectCriteriaType Type { get; }

        /// <summary>
        /// Applies select criteria in specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>The query after applying select criteria.</returns>
        IQueryable<T> Apply(IQueryable<T> query);
    }
}