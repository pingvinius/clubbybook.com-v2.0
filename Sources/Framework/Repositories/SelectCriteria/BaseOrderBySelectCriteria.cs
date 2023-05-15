namespace Pingvinius.Framework.Repositories.SelectCriteria
{
    using Pingvinius.Framework.Entities;

    /// <summary>
    /// The base select criteria class for order by select criteria.
    /// </summary>
    /// <typeparam name="T">The type of entity. Should be a class which implements IEntity interface.</typeparam>
    public abstract class BaseOrderBySelectCriteria<T> : BaseSelectCriteria<T> where T : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseOrderBySelectCriteria{T}"/> class.
        /// </summary>
        public BaseOrderBySelectCriteria()
            : base(SelectCriteriaType.OrderBy)
        {
        }
    }
}