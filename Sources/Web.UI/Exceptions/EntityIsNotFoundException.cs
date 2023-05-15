namespace ClubbyBook.Web.UI.Exceptions
{
    using System;

    /// <summary>
    /// The exception which is raised when the entity is not found but expected finding.
    /// </summary>
    [Serializable]
    public sealed class EntityIsNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityIsNotFoundException"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        public EntityIsNotFoundException(int id)
            : base(string.Format("The entity with ID '{0}' is not found.", id))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityIsNotFoundException"/> class.
        /// </summary>
        /// <param name="urlRewrite">The URL rewrite.</param>
        public EntityIsNotFoundException(string urlRewrite)
            : base(string.Format("The entity with this '{0}' url rewrite string is not found.", urlRewrite))
        {
        }
    }
}