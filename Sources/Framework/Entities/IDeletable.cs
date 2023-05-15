namespace Pingvinius.Framework.Entities
{
    /// <summary>
    /// The interface which should be used for entities which support and use IsDeleted flag for deleting.
    /// </summary>
    public interface IDeletable
    {
        /// <summary>
        /// Gets or sets a value indicating whether this entity is deleted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this entity is deleted; otherwise, <c>false</c>.
        /// </value>
        bool IsDeleted { get; set; }
    }
}