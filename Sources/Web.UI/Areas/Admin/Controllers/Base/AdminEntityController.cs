namespace ClubbyBook.Web.UI.Areas.Admin.Controllers.Base
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using ClubbyBook.Common;
    using ClubbyBook.Web.UI.Exceptions;
    using Pingvinius.Framework.Attributes;
    using Pingvinius.Framework.Entities;
    using Pingvinius.Framework.Mapping;
    using Pingvinius.Framework.Mvc.Models;
    using Pingvinius.Framework.Repositories;
    using Pingvinius.Framework.Repositories.SelectCriteria;

    public abstract class AdminEntityController<TRepository, TEntity, TEntityViewModel, TEntityViewModelList> : AdminController
        where TRepository : IEntityRepository<TEntity>
        where TEntity : class, IEntity
        where TEntityViewModel : EntityViewModel<TEntity>, new()
        where TEntityViewModelList : EntityViewModelList<TEntityViewModel, TEntity>, new()
    {
        protected int ItemCount { get; set; }

        protected TRepository Repository
        {
            get
            {
                return RepositoryFactory.Get<TRepository>();
            }
        }

        protected AdminEntityController()
        {
            // Default count for displaying list.
            this.ItemCount = 10;
        }

        #region Actions Routine

        [HttpGet]
        public virtual ActionResult Index()
        {
            // Validate input parameters
            if (this.PageNumber < 0)
            {
                throw new ArgumentException("this.PageNumber < 0");
            }

            // Get default select criteria
            var selectCriteria = this.GetIndexSelectCriteria();

            // Get entity list (could be with paging)
            var entityList = this.Repository.Select(selectCriteria);

            // Get total entity list count
            int entityCount = this.Repository.Count(selectCriteria);
            var viewModelList = this.Convert(entityList, entityCount);

            // Execute custom logic before show index page
            this.ExecuteCustomLogicOnIndex(viewModelList);

            // Show index page
            return this.View(viewModelList);
        }

        [HttpGet]
        public virtual ActionResult Details(int id)
        {
            // Validate input parameters
            if (id < 0)
            {
                throw new ArgumentException("id < 0");
            }

            // Get entity
            var entity = this.Repository.Get(id);
            if (entity == null)
            {
                throw new EntityIsNotFoundException(id);
            }

            // Convert entity to view model
            TEntityViewModel viewModel = this.Convert(entity);

            // Execute custom logic before show details page
            this.ExecuteCustomLogicOnDetails(viewModel);

            // Show details page
            return this.View(viewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int? id = null)
        {
            // Validate input parameters
            if (id.HasValue && id.Value < 0)
            {
                throw new ArgumentException("id.HasValue && id.Value < 0");
            }

            // Get entity if there is one
            TEntity entity = null;
            if (id.HasValue)
            {
                entity = this.Repository.Get(id.Value);
                if (entity == null)
                {
                    throw new EntityIsNotFoundException(id.Value);
                }
            }

            // Create view model based on entity
            TEntityViewModel viewModel = this.Convert(entity);

            // Execute custom logic before show edit page
            this.ExecuteCustomLogicOnEdit(viewModel);

            // Show edit page
            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult Edit(TEntityViewModel viewModel)
        {
            // Validate input parameters
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            // Validate model
            if (!this.ModelState.IsValid)
            {
                return View(viewModel);
            }

            // Get entity to modify
            TEntity entity = null;
            if (viewModel.IsNew)
            {
                entity = this.Repository.CreateInstance();
            }
            else
            {
                entity = this.Repository.Get(viewModel.Id);
                if (entity == null)
                {
                    throw new EntityIsNotFoundException(viewModel.Id);
                }
            }

            // Convert view model changes to entity changes
            entity = this.Convert(viewModel, entity);

            // Save changes
            this.Repository.SaveChanges(entity);

            // Process custom logic after saving
            this.ExecuteCustomLogicOnPostEdit(viewModel, entity);

            // Redirect to details page for this entity.
            return this.RedirectToAction("Details", new { id = entity.Id });
        }

        [HttpPost]
        [AccessPermission(RoleNames.Admin)]
        public virtual ActionResult Delete(int id)
        {
            // Try get entity
            var entity = this.Repository.Get(id);
            if (entity == null)
            {
                throw new EntityIsNotFoundException(id);
            }

            // Execute custom logic before delete
            this.ExecuteCustomLogicBeforeDelete(entity);

            // Process deleting
            this.Repository.Delete(entity);
            this.Repository.SaveChanges(entity);

            // Execute customLogic after delete
            this.ExecuteCustomLogicAfterDelete(entity);

            // Return success
            return this.CreateAjaxSuccessResponse();
        }

        #endregion Actions Routine

        protected virtual IList<ISelectCriteria<TEntity>> GetIndexSelectCriteria()
        {
            var selectCriteria = new List<ISelectCriteria<TEntity>>();
            selectCriteria.Add(new PagingSelectCriteria<TEntity>()
            {
                TakeCount = this.ItemCount,
                SkipCount = this.PageNumber * this.ItemCount
            });
            return selectCriteria;
        }

        #region Converters Routine

        protected virtual TEntityViewModelList Convert(IEnumerable<TEntity> entities, int entityCount)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }

            TEntityViewModelList resultList = new TEntityViewModelList();
            resultList.SearchKey = this.SearchKey;
            resultList.TotalItemCount = entityCount;
            resultList.PagingInfo.Index = this.PageNumber;
            resultList.PagingInfo.Count = entityCount / this.ItemCount + (entityCount % this.ItemCount == 0 ? 0 : 1);
            resultList.PagingInfo.Capacity = this.ItemCount;

            foreach (var entity in entities)
            {
                resultList.Items.Add(this.Convert(entity));
            }

            return resultList;
        }

        protected virtual TEntityViewModel Convert(TEntity entity, TEntityViewModel viewModel = null)
        {
            // The variable 'entity' could be null in case if it is creating

            if (viewModel == null)
            {
                viewModel = new TEntityViewModel();
            }

            viewModel = Mapper.Map<TEntity, TEntityViewModel>(entity, viewModel);

            return viewModel;
        }

        protected virtual TEntity Convert(TEntityViewModel viewModel, TEntity entity)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException("viewModel");
            }

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            return Mapper.Map<TEntityViewModel, TEntity>(viewModel, entity);
        }

        #endregion Converters Routine

        #region Custom Logic Virtual Methods Routine

        protected virtual void ExecuteCustomLogicOnIndex(TEntityViewModelList viewModelList)
        {
        }

        protected virtual void ExecuteCustomLogicOnDetails(TEntityViewModel viewModel)
        {
        }

        protected virtual void ExecuteCustomLogicOnEdit(TEntityViewModel viewModel)
        {
        }

        protected virtual void ExecuteCustomLogicOnPostEdit(TEntityViewModel viewModel, TEntity entity)
        {
        }

        protected virtual void ExecuteCustomLogicBeforeDelete(TEntity entity)
        {
        }

        protected virtual void ExecuteCustomLogicAfterDelete(TEntity entity)
        {
        }

        #endregion Custom Logic Virtual Methods Routine
    }
}