namespace Pingvinius.Framework.Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Pingvinius.Framework.Entities;

    /// <summary>
    /// The helper to work with trees.
    /// Creating trees from item list.
    /// Converting trees into flat model.
    /// </summary>
    public static class TreeHelper
    {
        /// <summary>
        /// Creates the tree from list.
        /// The list items will be sorted.
        /// </summary>
        /// <typeparam name="T">The item type. The item should implement ITreeItem interface.</typeparam>
        /// <param name="items">The items.</param>
        /// <returns>The root element of tree. The item there is null.</returns>
        /// <exception cref="System.ArgumentNullException">items</exception>
        public static TreeItem<T> CreateTreeFromList<T>(IEnumerable<T> items) where T : class, ITreeItem
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            // Create root tree item
            var rootTreeItem = new TreeItem<T>(null, null, 0);

            // Create children recursively
            foreach (var topLevelItem in items.Where(i => !i.ParentId.HasValue).ToList())
            {
                rootTreeItem.Children.Add(TreeHelper.CreateTreeItem<T>(items, topLevelItem, rootTreeItem, 1));
            }

            // Sort children
            rootTreeItem.SortChildren();

            // Return created root tree item
            return rootTreeItem;
        }

        /// <summary>
        /// Converts the tree to flat model.
        /// </summary>
        /// <typeparam name="T">The item type. The item should implement ITreeItem interface.</typeparam>
        /// <param name="rootTreeItem">The root tree item.</param>
        /// <returns>The list of flat tree items.</returns>
        /// <exception cref="System.ArgumentNullException">rootTreeItem</exception>
        public static IEnumerable<FlatTreeItem<T>> ConvertTreeToFlatModel<T>(TreeItem<T> rootTreeItem) where T : class, ITreeItem
        {
            if (rootTreeItem == null)
            {
                throw new ArgumentNullException("rootTreeItem");
            }

            // Create flat list
            List<FlatTreeItem<T>> flatTreeItemList = new List<FlatTreeItem<T>>();

            // Fill in flat model list
            foreach (var childTreeItem in rootTreeItem.Children)
            {
                TreeHelper.FillFlatTreeItemList(flatTreeItemList, childTreeItem);
            }

            // Return flat model list of tree items
            return flatTreeItemList;
        }

        /// <summary>
        /// Creates the tree item.
        /// </summary>
        /// <typeparam name="T">The item type. The item should implement ITreeItem interface.</typeparam>
        /// <param name="items">The items.</param>
        /// <param name="item">The item.</param>
        /// <param name="parentTreeItem">The parent tree item.</param>
        /// <param name="level">The level.</param>
        /// <returns>Created TreeItem with children.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// items
        /// or
        /// item
        /// or
        /// parentTreeItem
        /// </exception>
        private static TreeItem<T> CreateTreeItem<T>(IEnumerable<T> items, T item, TreeItem<T> parentTreeItem, int level) where T : class, ITreeItem
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            if (parentTreeItem == null)
            {
                throw new ArgumentNullException("parentTreeItem");
            }

            // Create current level tree item
            var treeItem = new TreeItem<T>(item, parentTreeItem, level);

            // Fill in children
            foreach (var childItem in items.Where(i => i.ParentId.HasValue && i.ParentId.Value == item.Id).ToList())
            {
                treeItem.Children.Add(TreeHelper.CreateTreeItem(items, childItem, treeItem, level + 1));
            }

            // Sort children
            treeItem.SortChildren();

            // Return created item
            return treeItem;
        }

        /// <summary>
        /// Fills the flat tree item list.
        /// </summary>
        /// <typeparam name="T">The item type. The item should implement ITreeItem interface.</typeparam>
        /// <param name="flatTreeItemList">The flat tree item list.</param>
        /// <param name="treeItem">The tree item.</param>
        /// <exception cref="System.ArgumentNullException">
        /// flatTreeItemList
        /// or
        /// treeItem
        /// </exception>
        private static void FillFlatTreeItemList<T>(IList<FlatTreeItem<T>> flatTreeItemList, TreeItem<T> treeItem) where T : class, ITreeItem
        {
            if (flatTreeItemList == null)
            {
                throw new ArgumentNullException("flatTreeItemList");
            }

            if (treeItem == null)
            {
                throw new ArgumentNullException("treeItem");
            }

            // Add current tree item to flat model
            flatTreeItemList.Add(new FlatTreeItem<T>(treeItem.Item, treeItem.Level));

            // Add children of tree item after current tree item recursively
            foreach (var childTreeItem in treeItem.Children)
            {
                TreeHelper.FillFlatTreeItemList(flatTreeItemList, childTreeItem);
            }
        }
    }
}
