namespace ClubbyBook.Web.UI.Mapping
{
    using Pingvinius.Framework.Entities;
    using Pingvinius.Framework.Mapping;
    using Pingvinius.Framework.Mvc.Models;

    public abstract class EntityToViewModelMapping<TEntity, TViewModel> : BaseMapping<TEntity, TViewModel>
        where TEntity : class, IEntity
        where TViewModel : EntityViewModel<TEntity>
    {
        protected override TViewModel MapSourceToTarget(TEntity source, TViewModel targetBase)
        {
            targetBase.Id = source != null ? source.Id : -1;
            targetBase.IsNew = source != null ? false : true;

            return targetBase;
        }
    }
}