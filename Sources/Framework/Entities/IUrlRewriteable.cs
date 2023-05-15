namespace Pingvinius.Framework.Entities
{
    /// <summary>
    /// The URL rewrite mark for entities.
    /// </summary>
    public interface IUrlRewriteable
    {
        /// <summary>
        /// Gets or sets the URL rewrite.
        /// </summary>
        /// <value>
        /// The URL rewrite.
        /// </value>
        string UrlRewrite { get; set; }

        /// <summary>
        /// Generates the URL rewrite string.
        /// </summary>
        /// <returns></returns>
        string GenerateUrlRewriteString();
    }
}