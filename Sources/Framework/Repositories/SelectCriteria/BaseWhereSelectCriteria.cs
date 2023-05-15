namespace Pingvinius.Framework.Repositories.SelectCriteria
{
    using Pingvinius.Framework.Entities;

    /// <summary>
    /// The base select criteria class for where select criteria type.
    /// </summary>
    /// <typeparam name="T">The type of entity. Should be a class which implements IEntity interface.</typeparam>
    public abstract class BaseWhereSelectCriteria<T> : BaseSelectCriteria<T> where T : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseWhereSelectCriteria{T}"/> class.
        /// </summary>
        public BaseWhereSelectCriteria()
            : base(SelectCriteriaType.Where)
        {
        }
    }
}