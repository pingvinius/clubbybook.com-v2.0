namespace Pingvinius.Framework.Mvc.Models
{
    using Pingvinius.Framework.Entities;

    public abstract class EntityViewModelList<TEntityViewModel, TEntity> : ViewModelList<TEntityViewModel>
        where TEntityViewModel : EntityViewModel<TEntity>
        where TEntity : class, IEntity
    {
    }
}