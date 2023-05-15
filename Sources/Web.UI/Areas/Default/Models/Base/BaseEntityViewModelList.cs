namespace ClubbyBook.Web.UI.Areas.Default.Models.Base
{
    using Pingvinius.Framework.Entities;
    using Pingvinius.Framework.Mvc.Models;

    public abstract class BaseEntityViewModelList<TEntityViewModel, TEntity> : EntityViewModelList<TEntityViewModel, TEntity>
        where TEntityViewModel : EntityViewModel<TEntity>
        where TEntity : class, IEntity
    {
    }
}