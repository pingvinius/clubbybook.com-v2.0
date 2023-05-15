namespace Pingvinius.Framework.Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    /// <summary>
    /// The class represents flat tree item.
    /// </summary>
    /// <typeparam name="T">The item type. The item should implement ITreeItem interface.</typeparam>
    public sealed class FlatTreeItem<T> where T : class, ITreeItem
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
        /// Initializes a new instance of the <see cref="FlatTreeItem{T}"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="level">The level.</param>
        /// <exception cref="System.ArgumentNullException">item</exception>
        public FlatTreeItem(T item, int level)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            this.Item = item;
            this.Level = level;
        }
    }
}
