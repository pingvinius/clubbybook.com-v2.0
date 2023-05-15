namespace Pingvinius.Framework.Repositories
{
    using Pingvinius.Framework.Entities;

    /// <summary>
    /// The base interface for entity repositories which support URL rewrite getting.
    /// </summary>
    /// <typeparam name="T">The entity type. Should be a class which implement IEntity interface.</typeparam>
    public interface IUrlRewriteEntityRepository<T> : IEntityRepository<T> where T : IEntity
    {
        /// <summary>
        /// Gets entity by specified URL rewrite string.
        /// </summary>
        /// <param name="urlRewrite">The URL rewrite string.</param>
        /// <returns>
        /// The entity.
        /// </returns>
        T Get(string urlRewrite);
    }
}