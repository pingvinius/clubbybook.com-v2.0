namespace Pingvinius.Framework.Repositories.SelectCriteria
{
    using Pingvinius.Framework.Entities;

    /// <summary>
    /// The URL rewrite select criteria class.
    /// </summary>
    /// <typeparam name="T">The type of entity which supports URL rewrite. Should be a class which implements IEntity interface.</typeparam>
    public abstract class BaseUrlRewriteSelectCriteria<T> : BaseWhereSelectCriteria<T> where T : IEntity
    {
        /// <summary>
        /// Gets the URL rewrite.
        /// </summary>
        /// <value>
        /// The URL rewrite.
        /// </value>
        public string UrlRewrite { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseWhereSelectCriteria{T}"/> class.
        /// </summary>
        public BaseUrlRewriteSelectCriteria(string urlRewrite)
        {
            this.UrlRewrite = urlRewrite;
        }
    }
}