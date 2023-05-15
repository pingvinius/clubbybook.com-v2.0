namespace Pingvinius.Framework.Repositories
{
    using System;
    using System.Collections.Generic;
    using Pingvinius.Framework.Entities;
    using Pingvinius.Framework.Repositories.SelectCriteria;

    /// <summary>
    /// The base implementation of entity repository.
    /// All entity repositories should inherit this base class.
    /// </summary>
    /// <typeparam name="T">The entity type. Should be a class which implements IEntity interface and has empty constructor.</typeparam>
    public abstract class BaseEntityRepository<T> : BaseRepository, IEntityRepository<T> where T : IEntity, new()
    {
        #region Abstract Methods Routine

        /// <summary>
        /// Selects entities by specified criteria.
        /// Should be implemented in inner classes to implement the logic.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The entity list.</returns>
        protected abstract IEnumerable<T> SelectInternal(IEnumerable<ISelectCriteria<T>> criteria);

        /// <summary>
        /// Counts entities by specified criteria.
        /// Should be implemented in inner classes to implement the logic.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The entities count.</returns>
        protected abstract int CountInternal(IEnumerable<ISelectCriteria<T>> criteria);

        /// <summary>
        /// Gets entity by specified entity id.
        /// Should be implemented in inner classes to implement the logic.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        /// <returns>The entity.</returns>
        protected abstract T GetInternal(int entityId);

        /// <summary>
        /// Creates the instance of entity.
        /// Should be implemented in inner classes to implement the logic.
        /// </summary>
        /// <returns>The created entity instance.</returns>
        protected abstract T CreateInstanceInternal();

        /// <summary>
        /// Saves the changes.
        /// Should be implemented in inner classes to implement the logic.
        /// </summary>
        /// <param name="entity">The entity.</param>
        protected abstract void SaveChangesInternal(T entity);

        /// <summary>
        /// Deletes the specified entity.
        /// Should be implemented in inner classes to implement the logic.
        /// </summary>
        /// <param name="entity">The entity.</param>
        protected abstract void DeleteInternal(T entity);

        #endregion Abstract Methods Routine

        #region IEntityRepository<T> Members

        /// <summary>
        /// Selects entities by specified criteria.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The entity list.</returns>
        public IEnumerable<T> Select(IEnumerable<ISelectCriteria<T>> criteria = null)
        {
            return this.SelectInternal(criteria);
        }

        /// <summary>
        /// Selects entities by specified criterion.
        /// </summary>
        /// <param name="criterion">The criterion.</param>
        /// <returns>The entity list.</returns>
        public IEnumerable<T> Select(ISelectCriteria<T> criterion)
        {
            List<ISelectCriteria<T>> criteria = new List<ISelectCriteria<T>>();
            if (criterion != null)
            {
                criteria.Add(criterion);
            }

            return this.Select(criteria);
        }

        /// <summary>
        /// Counts entities by specified criteria.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The entities count.</returns>
        public int Count(IEnumerable<ISelectCriteria<T>> criteria = null)
        {
            return this.CountInternal(criteria);
        }

        /// <summary>
        /// Counts entities by specified criterion.
        /// </summary>
        /// <param name="criterion">The criterion.</param>
        /// <returns>The entities count.</returns>
        public int Count(ISelectCriteria<T> criterion)
        {
            List<ISelectCriteria<T>> criteria = new List<ISelectCriteria<T>>();
            if (criterion != null)
            {
                criteria.Add(criterion);
            }

            return this.Count(criteria);
        }

        /// <summary>
        /// Gets entity by specified entity id.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        /// <returns>The entity.</returns>
        public T Get(int entityId)
        {
            if (entityId < 0)
            {
                throw new ArgumentException("entityId < 0");
            }
            return this.GetInternal(entityId);
        }

        /// <summary>
        /// Creates the instance of entity.
        /// </summary>
        /// <returns>The created entity instance.</returns>
        public T CreateInstance()
        {
            return this.CreateInstanceInternal();
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void SaveChanges(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.SaveChangesInternal(entity);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.DeleteInternal(entity);
        }

        #endregion IEntityRepository<T> Members
    }
}