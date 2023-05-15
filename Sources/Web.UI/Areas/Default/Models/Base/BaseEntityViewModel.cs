namespace ClubbyBook.Web.UI.Areas.Default.Models.Base
{
    using Pingvinius.Framework.Entities;
    using Pingvinius.Framework.Mvc.Models;

    public abstract class BaseEntityViewModel<TEntity> : EntityViewModel<TEntity> where TEntity : class, IEntity
    {
    }
}