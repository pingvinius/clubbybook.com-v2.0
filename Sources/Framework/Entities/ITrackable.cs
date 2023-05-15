namespace Pingvinius.Framework.Entities
{
    using System;

    /// <summary>
    /// The track able mark for entities.
    /// </summary>
    public interface ITrackable
    {
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the last modified date.
        /// </summary>
        /// <value>
        /// The last modified date.
        /// </value>
        DateTime LastModifiedDate { get; set; }
    }
}