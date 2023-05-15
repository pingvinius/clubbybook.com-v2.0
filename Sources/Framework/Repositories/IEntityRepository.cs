namespace Pingvinius.Framework.Repositories
{
    using System.Collections.Generic;
    using Pingvinius.Framework.Entities;
    using Pingvinius.Framework.Repositories.SelectCriteria;

    /// <summary>
    /// The base interface for entity repositories.
    /// All entity repositories should inherit this interface.
    /// </summary>
    /// <typeparam name="T">The entity type. Should be a class which implement IEntity interface.</typeparam>
    public interface IEntityRepository<T> : IRepository where T : IEntity
    {
        /// <summary>
        /// Selects entities by specified criteria (multiple).
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The entity list.</returns>
        IEnumerable<T> Select(IEnumerable<ISelectCriteria<T>> criteria = null);

        /// <summary>
        /// Selects entities by the specified criteria.
        /// </summary>
        /// <param name="criterion">The criterion.</param>
        /// <returns></returns>
        IEnumerable<T> Select(ISelectCriteria<T> criterion);

        /// <summary>
        /// Counts entities by specified criteria.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The entities count.</returns>
        int Count(IEnumerable<ISelectCriteria<T>> criteria = null);

        /// <summary>
        /// Counts entities by specified criterion.
        /// </summary>
        /// <param name="criterion">The criterion.</param>
        /// <returns>The entities count.</returns>
        int Count(ISelectCriteria<T> criterion);

        /// <summary>
        /// Gets entity by specified entity id.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        /// <returns>The entity.</returns>
        T Get(int entityId);

        /// <summary>
        /// Creates the instance of entity.
        /// </summary>
        /// <returns>The created entity instance.</returns>
        T CreateInstance();

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void SaveChanges(T entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);
    }
}