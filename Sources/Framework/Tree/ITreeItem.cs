namespace Pingvinius.Framework.Tree
{
    using System;

    /// <summary>
    /// The interface which should be used for entities which are tree items.
    /// </summary>
    public interface ITreeItem : IComparable
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        int Id { get; }

        /// <summary>
        /// Gets the parent id.
        /// </summary>
        /// <value>
        /// The parent id.
        /// </value>
        int? ParentId { get; }
    }
}