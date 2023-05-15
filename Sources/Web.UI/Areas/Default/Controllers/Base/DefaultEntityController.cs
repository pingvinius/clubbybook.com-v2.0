namespace ClubbyBook.Web.UI.Areas.Default.Controllers.Base
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using ClubbyBook.Web.UI.Exceptions;
    using Pingvinius.Framework.Entities;
    using Pingvinius.Framework.Mapping;
    using Pingvinius.Framework.Mvc.Models;
    using Pingvinius.Framework.Repositories;
    using Pingvinius.Framework.Repositories.SelectCriteria;

    public abstract class DefaultEntityController<TRepository, TEntity, TEntityViewModel, TEntityViewModelList> : DefaultController
        where TRepository : IUrlRewriteEntityRepository<TEntity>
        where TEntity : class, IEntity, IUrlRewriteable
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

        protected DefaultEntityController()
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
        public virtual ActionResult Details(string urlRewrite)
        {
            // Validate input parameters
            if (string.IsNullOrWhiteSpace(urlRewrite))
            {
                throw new ArgumentException("The URL rewrite string is not specified.");
            }

            // Get entity
            var entity = this.Repository.Get(urlRewrite);
            if (entity == null)
            {
                throw new EntityIsNotFoundException(urlRewrite);
            }

            // Convert entity to view model
            TEntityViewModel viewModel = this.Convert(entity);

            // Execute custom logic before show details page
            this.ExecuteCustomLogicOnGetDetails(viewModel);

            // Show details page
            return this.View(viewModel);
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
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            if (viewModel == null)
            {
                viewModel = new TEntityViewModel();
            }

            viewModel = Mapper.Map<TEntity, TEntityViewModel>(entity, viewModel);

            return viewModel;
        }

        #endregion Converters Routine

        #region Custom Logic Virtual Methods Routine

        protected virtual void ExecuteCustomLogicOnIndex(TEntityViewModelList viewModelList)
        {
        }

        protected virtual void ExecuteCustomLogicOnGetDetails(TEntityViewModel viewModel)
        {
        }

        #endregion Custom Logic Virtual Methods Routine
    }
}