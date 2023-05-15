namespace ClubbyBook.Data.Models
{
    using System;
    using Pingvinius.Framework.Entities;
    using Pingvinius.Framework.Tree;

    public partial class Genre : IEntity, IUrlRewriteable, ITreeItem
    {
        public string FullName
        {
            get
            {
                string fullName = Name;
                Genre parent = this.Parent;

                while (parent != null)
                {
                    fullName = string.Format("{0} - {1}", parent.Name, fullName);
                    parent = parent.Parent;
                }

                return fullName;
            }
        }

        #region IUrlRewriteable Members

        public string GenerateUrlRewriteString()
        {
            throw new NotSupportedException();
        }

        #endregion IUrlRewriteable Members

        #region ITreeItem Members

        int ITreeItem.Id
        {
            get { return this.Id; }
        }

        int? ITreeItem.ParentId
        {
            get { return this.ParentId; }
        }

        #endregion

        #region IComparable Members

        public int CompareTo(object obj)
        {
            // If passed item is null, current instance is bigger
            if (obj == null)
            {
                return 1;
            }

            // Don't allow to use this method with not Genre type
            var otherGenre = obj as Genre;
            if (otherGenre == null)
            {
                throw new ArgumentException(string.Format("The specified object has type {0} but should be Genre.", obj.GetType().Name));
            }

            // If orders are different, compare genres based on them
            int orderCompareResult = this.Order.CompareTo(otherGenre.Order);
            if (orderCompareResult != 0)
            {
                return orderCompareResult;
            }

            // If orders equal compare genres based on names
            if (string.IsNullOrWhiteSpace(this.Name))
            {
                return -1;
            }
            if (string.IsNullOrWhiteSpace(otherGenre.Name))
            {
                return 1;
            }
            return string.Compare(this.Name, otherGenre.Name, StringComparison.OrdinalIgnoreCase);
        }

        #endregion
    }
}