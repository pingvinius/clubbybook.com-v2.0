namespace Pingvinius.Framework.Attributes
{
    using System;

    /// <summary>
    /// The class used as attribute to Enum values to specify that some value has URL rewrite text.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class UrlRewriteAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the URL rewrite.
        /// </summary>
        /// <value>
        /// The URL rewrite.
        /// </value>
        public string UrlRewrite { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlRewriteAttribute"/> class.
        /// </summary>
        /// <param name="urlRewrite">The URL rewrite.</param>
        public UrlRewriteAttribute(string urlRewrite)
        {
            this.UrlRewrite = urlRewrite;
        }
    }
}