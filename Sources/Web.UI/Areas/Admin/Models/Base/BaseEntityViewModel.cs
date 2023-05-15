namespace ClubbyBook.Web.UI.Areas.Admin.Models.Base
{
    using Pingvinius.Framework.Entities;
    using Pingvinius.Framework.Mvc.Models;

    public abstract class BaseEntityViewModel<TEntity> : EntityViewModel<TEntity> where TEntity : class, IEntity
    {
    }
}