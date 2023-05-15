namespace ClubbyBook.Web.UI.Mapping
{
    using Pingvinius.Framework.Entities;
    using Pingvinius.Framework.Mapping;
    using Pingvinius.Framework.Mvc.Models;

    public abstract class ViewModelToEntityMapping<TViewModel, TEntity> : BaseMapping<TViewModel, TEntity>
        where TViewModel : EntityViewModel<TEntity>
        where TEntity : class, IEntity
    {
        protected override TEntity MapSourceToTarget(TViewModel source, TEntity targetBase)
        {
            return targetBase;
        }
    }
}