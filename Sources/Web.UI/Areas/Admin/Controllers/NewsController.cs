namespace ClubbyBook.Web.UI.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using ClubbyBook.Business.Repositories.Interfaces;
    using ClubbyBook.Business.Repositories.SelectCriteria;
    using ClubbyBook.Data.Models;
    using ClubbyBook.Web.UI.Areas.Admin.Controllers.Base;
    using ClubbyBook.Web.UI.Areas.Admin.Models.News;
    using Pingvinius.Framework.Repositories.SelectCriteria;

    public sealed class NewsController : AdminEntityController<INewsRepository, News, NewsViewModel, NewsViewModelList>
    {
        public NewsController()
        {
            this.CurrentTabName = "tab-news";
        }

        protected override IList<ISelectCriteria<News>> GetIndexSelectCriteria()
        {
            var selectCriteria = base.GetIndexSelectCriteria();
            selectCriteria.Add(new NewsSelectCriteria.DefaultSearch(this.SearchKey));
            selectCriteria.Add(new NewsSelectCriteria.CreatedDateSort());
            return selectCriteria;
        }
    }
}