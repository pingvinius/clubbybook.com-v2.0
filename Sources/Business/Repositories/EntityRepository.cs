namespace ClubbyBook.Business.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using ClubbyBook.Data.Models;
    using Pingvinius.Framework.DbContext;
    using Pingvinius.Framework.Entities;
    using Pingvinius.Framework.Repositories;
    using Pingvinius.Framework.Repositories.SelectCriteria;
    using Pingvinius.Framework.Utilities;

    internal abstract class EntityRepository<T> : BaseEntityRepository<T> where T : class, IEntity, new()
    {
        #region Properties

        protected abstract DbSet<T> EntityList { get; }

        protected ExtendedDbContext Context
        {
            get { return (ExtendedDbContext)DbContextManager.GetCurrentContext(); }
        }

        #endregion Properties

        #region Default Select Criteria Routine

        protected virtual IList<ISelectCriteria<T>> GetDefaultSelectCriteriaForSelect()
        {
            return new List<ISelectCriteria<T>>();
        }

        protected virtual IList<ISelectCriteria<T>> GetDefaultSelectCriteriaForCount()
        {
            return new List<ISelectCriteria<T>>();
        }

        protected virtual bool IsEntityUnique(T entity)
        {
            throw new NotSupportedException();
        }

        #endregion Default Select Criteria Routine

        #region Query Configuration Routine

        protected virtual DbQuery<T> ConfigureQueryForSelect(DbQuery<T> query)
        {
            return query;
        }

        protected virtual DbQuery<T> ConfigureQueryForGet(DbQuery<T> query)
        {
            return query;
        }

        #endregion Query Configuration Routine

        #region BaseEntityRepository Memeber Overrides Routine

        protected override IEnumerable<T> SelectInternal(IEnumerable<ISelectCriteria<T>> selectCriteria)
        {
            // Apply default select criteria
            if (selectCriteria == null)
            {
                selectCriteria = this.GetDefaultSelectCriteriaForSelect();
            }

            // Configure query
            var configuredQuery = this.ConfigureQueryForSelect(this.EntityList);

            // Prepare query to use in conditionals
            var query = configuredQuery.AsQueryable<T>();

            // Apply Where statement
            foreach (var criterion in selectCriteria.Where(sc => sc.Type == SelectCriteriaType.Where))
            {
                query = criterion.Apply(query);
            }

            // Apply OrderBy statement
            foreach (var criterion in selectCriteria.Where(sc => sc.Type == SelectCriteriaType.OrderBy))
            {
                query = criterion.Apply(query);
            }

            // Apply Paging statement
            foreach (var criterion in selectCriteria.Where(sc => sc.Type == SelectCriteriaType.Paging))
            {
                query = criterion.Apply(query);
            }

            // Run query and return result list
            return query.ToList();
        }

        protected override int CountInternal(IEnumerable<ISelectCriteria<T>> selectCriteria)
        {
            // Apply default select criteria
            if (selectCriteria == null)
            {
                selectCriteria = this.GetDefaultSelectCriteriaForCount();
            }

            // Prepare query to use in conditionals
            var query = this.EntityList.AsQueryable<T>();

            // Apply Where statement
            foreach (var criterion in selectCriteria.Where(sc => sc.Type == SelectCriteriaType.Where))
            {
                query = criterion.Apply(query);
            }

            // Run query and return item count
            return query.Count();
        }

        protected override T GetInternal(int entityId)
        {
            // Configure query
            var configuredQuery = this.ConfigureQueryForGet(this.EntityList);

            // Find and return item by Id
            return configuredQuery.Where(e => e.Id == entityId).FirstOrDefault();
        }

        protected override T CreateInstanceInternal()
        {
            // Create new entity
            T entity = new T();

            // Add entity to list
            this.EntityList.Add(entity);

            // Return created entity
            return entity;
        }

        protected override void SaveChangesInternal(T entity)
        {
            var entityState = this.Context.Entry(entity).State;

            // Do not save changes if there is no them
            if (entityState == EntityState.Unchanged)
            {
                return;
            }

            if (entityState != EntityState.Deleted)
            {
                // Update track able fields
                this.UpdateTrackableFields(entity);

                // Update URL rewrite field
                this.UpdateUrlRewriteField(entity);
            }

            // Save changes
            int changeCount = this.Context.SaveChanges();

            // Log save operation
            this.Logger.Debug("The {0} changes were saved to database.", changeCount);
        }

        protected override void DeleteInternal(T entity)
        {
            var deletableEntity = entity as IDeletable;
            if (deletableEntity != null)
            {
                deletableEntity.IsDeleted = true;
            }
            else
            {
                this.EntityList.Remove(entity);
            }
        }

        #endregion BaseEntityRepository Memeber Overrides Routine

        protected void UpdateTrackableFields(T entity)
        {
            ITrackable trackableEntity = entity as ITrackable;
            if (trackableEntity != null)
            {
                if (this.Context.Entry(entity).State == EntityState.Added)
                {
                    trackableEntity.CreatedDate = DateTimeHelper.Now;
                }

                trackableEntity.LastModifiedDate = DateTimeHelper.Now;
            }
        }

        protected void UpdateUrlRewriteField(T entity)
        {
            IUrlRewriteable rewriteableEntity = entity as IUrlRewriteable;
            if (rewriteableEntity != null)
            {
                int correctionIndex = 0;
                bool isEntityUnique = true;
                do
                {
                    string generatedUrlRewrite = rewriteableEntity.GenerateUrlRewriteString().ToLower();
                    string newUrlRewriteValue = correctionIndex == 0 ? generatedUrlRewrite : string.Format("{0}-{1}", generatedUrlRewrite, correctionIndex);

                    if (rewriteableEntity.UrlRewrite != newUrlRewriteValue)
                    {
                        rewriteableEntity.UrlRewrite = newUrlRewriteValue;

                        isEntityUnique = this.IsEntityUnique(entity);
                        if (!isEntityUnique)
                        {
                            correctionIndex++;
                        }
                    }
                }
                while (!isEntityUnique);
            }
        }
    }
}