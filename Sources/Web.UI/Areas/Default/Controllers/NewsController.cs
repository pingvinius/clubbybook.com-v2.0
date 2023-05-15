namespace ClubbyBook.Web.UI.Areas.Default.Controllers
{
    using System.Collections.Generic;
    using ClubbyBook.Business.Repositories.Interfaces;
    using ClubbyBook.Business.Repositories.SelectCriteria;
    using ClubbyBook.Data.Models;
    using ClubbyBook.Web.UI.Areas.Default.Controllers.Base;
    using ClubbyBook.Web.UI.Areas.Default.Models.News;
    using Pingvinius.Framework.Repositories.SelectCriteria;

    public sealed class NewsController : DefaultEntityController<INewsRepository, News, NewsViewModel, NewsViewModelList>
    {
        protected override IList<ISelectCriteria<News>> GetIndexSelectCriteria()
        {
            var selectCriteria = base.GetIndexSelectCriteria();
            selectCriteria.Add(new NewsSelectCriteria.DefaultSearch(this.SearchKey));
            selectCriteria.Add(new NewsSelectCriteria.CreatedDateSort());
            return selectCriteria;
        }
    }
}