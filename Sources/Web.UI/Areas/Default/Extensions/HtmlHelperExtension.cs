namespace ClubbyBook.Web.UI.Areas.Default.Extensions
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using ClubbyBook.Web.UI.Areas.Default.Models.Common;
    using Pingvinius.Framework.Entities;
    using Pingvinius.Framework.Mvc.Models;

    public static class HtmlHelperExtension
    {
        #region Header Builders Routine

        public static Header GetHeaderBlock(this HtmlHelper htmlHelper, string title, bool useSmallTitle = false, params Header.RightLink[] rightLinks)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException("htmlHelper");
            }

            return new Header()
            {
                Title = title,
                UseSmallTitle = useSmallTitle,
                RightLinks = rightLinks != null ? rightLinks.ToList() : null
            };
        }

        #endregion Header Builders Routine

        #region Search Builders Routine

        public static Search GetSearchBlockForIndex<TEntityViewModelList, TEntityViewModel, TEntity>(this HtmlHelper htmlHelper, TEntityViewModelList viewModelList)
            where TEntityViewModelList : EntityViewModelList<TEntityViewModel, TEntity>
            where TEntityViewModel : EntityViewModel<TEntity>
            where TEntity : class, IEntity
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException("htmlHelper");
            }

            return new Search()
            {
                SearchKey = viewModelList.SearchKey,
                TotalItemCount = viewModelList.TotalItemCount
            };
        }

        #endregion Search Builders Routine
    }
}