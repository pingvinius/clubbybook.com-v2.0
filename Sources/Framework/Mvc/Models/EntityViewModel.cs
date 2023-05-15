namespace Pingvinius.Framework.Mvc.Models
{
    using System.ComponentModel.DataAnnotations;
    using Pingvinius.Framework.Entities;

    public abstract class EntityViewModel<TEntity> : ViewModel where TEntity : class, IEntity
    {
        [Key]
        public int Id { get; set; }

        public bool IsNew { get; set; }

        public EntityViewModel()
        {
            this.Id = -1;
            this.IsNew = true;
        }
    }
}