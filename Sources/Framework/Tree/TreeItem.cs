namespace Pingvinius.Framework.Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Pingvinius.Framework.Entities;


    /// <summary>
    /// Class represents tree item.
    /// </summary>
    /// <typeparam name="T">The item type. The item should implement ITreeItem interface.</typeparam>
    public sealed class TreeItem<T> where T : class, ITreeItem
    {
        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        public T Item { get; private set; }

        /// <summary>
        /// Gets the level.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        public int Level { get; private set; }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public TreeItem<T> Parent { get; private set; }

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        public IList<TreeItem<T>> Children { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this tree item is root.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this tree item is root; otherwise, <c>false</c>.
        /// </value>
        public bool IsRoot
        {
            get
            {
                return this.Parent == null;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeItem{T}"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="parent">The parent.</param>
        /// <param name="level">The level.</param>
        public TreeItem(T item, TreeItem<T> parent, int level)
        {
            this.Item = item;
            this.Level = level;
            this.Parent = parent;
            this.Children = new List<TreeItem<T>>();
        }

        /// <summary>
        /// Sorts the children.
        /// </summary>
        public void SortChildren()
        {
            ((List<TreeItem<T>>)this.Children).Sort((x, y) => x.Item.CompareTo(y.Item));
        }
    }
}
